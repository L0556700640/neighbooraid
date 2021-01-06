using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace BL
{
    public class DoctorBL
    {
        public static string AddDoctor(DTO.DoctorsDetailsDTO doctor, HttpPostedFile file)
        {
            try
            {
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    DAL.Doctor newDoctor = Convertors.DoctorConvertor.ConvertDoctorToDAL(doctor.Doctor);
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        newDoctor.pictureDiploma = binaryReader.ReadBytes(file.ContentLength);
                    }
                    db.Doctors.Add(newDoctor);
                    db.SaveChanges();
                    //לבדוק אם נכון להעביר בצורה כזו למסד הנתונים (בלי המרה) ב
                    foreach (var c in doctor.Cases)
                    {
                        db.CasesToDoctors.Add(
                        new DAL.CasesToDoctor
                        {
                            doctorId = newDoctor.doctorId,
                            caseId = c.caseId,
                            satisfaction = 0
                        }
                        );
                    }
                    db.SaveChanges();
                    ManagerBL.SendMailToConfirmDoctor(newDoctor);

                    return newDoctor.doctorId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (-1).ToString();
            }
        }
        public static void ConfirmDoctor(int doctorId, bool isConfirmed)
        {
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                (from d in db.Doctors
                 where d.doctorId.Equals(doctorId)
                 select d).ToList().ForEach(d => d.isConfirmed = isConfirmed);
                db.SaveChanges();
            }

        }
        public static bool CheckUser(string firstName, string id)
        {
            List<DAL.Doctor> dd;
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                dd = (from d in db.Doctors
                      where d.doctorId.Equals(id) && d.firstName.Equals(firstName) && d.isConfirmed == true
                      select d).ToList();
                // db.SaveChanges();
            }
            if (dd.Count > 0)
                return true;
            return false;
        }
        public static DTO.Doctor User(string id)
        {

            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                var doctors = (from d in db.Doctors
                               where d.doctorId.Equals(id)
                               select d).ToList();
                return doctors.Select(d => Convertors.DoctorConvertor.ConvertDoctorToDTO(d)).FirstOrDefault();
                // db.SaveChanges();
            }

        }
        public static List<DTO.CasesToDoctor> casesToDoctor(string id)
        {

            try
            {
                List<DAL.CasesToDoctor> dalCases = new List<DAL.CasesToDoctor>();
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    dalCases = db.CasesToDoctors.ToList();
                }
                List<DTO.CasesToDoctor> dtoCases = new List<DTO.CasesToDoctor>();
                foreach (var c in dalCases)
                {
                    if (c.doctorId == id)
                        dtoCases.Add(
                            Convertors.CaseToDoctorConvertor.ConvertCasesToDoctorToDTO(c)
                            );
                }

                return dtoCases;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        public static bool DeleteDoctor(string id)
        {
            try
            {
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    var doctors = (from d in db.Doctors
                                   where d.doctorId.Equals(id)
                                   select d).ToList();

                    db.Doctors.Remove(doctors.FirstOrDefault());
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }


        }
        public static List<DTO.ReturnedDoctorsToCase> GetDoctorsToCase(DTO.Cases correntCase)
        {
            //this function get the doctors to the corrent case from the db:
            //it find the doctors who note that them specializes in this case,
            //check that them confirm, and that the patient who get from them help
            //in the past, doesn't report that the treatment was bad
            List<DTO.Doctor> doctorsToCorrentCase = new List<DTO.Doctor>();
            List<DAL.Doctor> doctorsOfCorrentCaseFromDB = new List<DAL.Doctor>();
            int satisfactorySatisfaction = 50;
            try
            {
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    doctorsOfCorrentCaseFromDB =
                        db.Cases.FirstOrDefault(c => c.caseId == correntCase.caseId)
                        .CasesToDoctors
                        .Where(c=>c.Doctor.isConfirmed==true
                        && c.satisfaction>= satisfactorySatisfaction)
                        .Select(c => c.Doctor)
                        .ToList();
                }
                foreach (var doctor in doctorsOfCorrentCaseFromDB)
                {
                    doctorsToCorrentCase.Add(
                        Convertors.DoctorConvertor.ConvertDoctorToDTO(doctor));
                }
                //now the func check which doctor from the list live close to the help call
                //and also find the doctors from google contacts.
                List<ReturnedDoctorsToCase> theMostSuitableDoctors = new List<ReturnedDoctorsToCase>();
                //from google people
                //by calc the distance
                foreach (var doctor1 in doctorsToCorrentCase)
                {

                }
                string doctorPoint = "";
                string helpCallPoint = "";
                int a = DistanceFromPatientAndDoctorInMinutes(helpCallPoint,doctorPoint);



                return theMostSuitableDoctors;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static int DistanceFromPatientAndDoctorInMinutes(string patientPoint, string doctorPoint)
        {
            string uri = "https://maps.googleapis.com/maps/api/distancematrix/xml?key=AIzaSyA5L81_-5d2Hy7hHsNVhodk1zS90Qu-aP8&origins="
                          + patientPoint + "&destinations=" + doctorPoint + "&mode=driving&units=imperial&sensor=false";
            WebClient wc = new WebClient();
            try
            {
                string geoCodeInfo = wc.DownloadString(uri);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(geoCodeInfo);

                string duration = xmlDoc.DocumentElement.SelectSingleNode("/DistanceMatrixResponse/row/element/duration/value").InnerText;
                return Convert.ToInt32(duration) / 60;
            }
            catch (Exception)
            {
                return -1;
            }
            //return TravelingTime
        }
    }
}
