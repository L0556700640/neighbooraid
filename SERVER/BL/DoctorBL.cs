using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BL
{
    public class DoctorBL
    {
        public static string AddDoctor(DTO.DoctorsDetailsDTO doctor,HttpPostedFile file)
        {
            try
            {
                using (neighboorAidDBEntities db=new neighboorAidDBEntities())
                {
                    DAL.Doctor newDoctor = Convertors.DoctorConvertor.ConvertDoctorToDAL(doctor.Doctor);
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                     newDoctor.pictureDiploma= binaryReader.ReadBytes(file.ContentLength);
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
                        ) ;
                    }
                    db.SaveChanges();
                    ManagerBL.SendMailToConfirmDoctor(newDoctor);

                    return newDoctor.doctorId;}
            }
            catch(Exception ex)
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
        public static bool CheckUser(string firstName,string id)
        {
            List<DAL.Doctor> dd;
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                dd = (from d in db.Doctors
                        where d.doctorId.Equals(id) && d.firstName.Equals(firstName)&& d.isConfirmed==true select d).ToList();
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
                var doctors =(from d in db.Doctors
                             where d.doctorId.Equals(id)
                             select d).ToList();
               return doctors.Select(d=> Convertors.DoctorConvertor.ConvertDoctorToDTO(d)).FirstOrDefault();
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
                    if(c.doctorId==id)
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
        public static List<DTO.Doctor> GetDoctorsToCase(DTO.Cases correntCase)
        {
            List<DTO.Doctor> doctorsToCorrentCase = new List<DTO.Doctor>();
            List<DAL.Doctor> doctorsOfCorrentCaseFromDB = new List<DAL.Doctor>();
            try
            {
            using(neighboorAidDBEntities db=new neighboorAidDBEntities())
            {
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return new List<DTO.Doctor>();
        }
    }
}
