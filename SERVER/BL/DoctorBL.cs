using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                    //string diplomaDocumentPath = "//";//todo: send the file path and save in db
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
                    SendMailToConfirmDoctor(newDoctor);

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
                        .Where(c => c.Doctor.isConfirmed == true
                        && c.satisfaction >= satisfactorySatisfaction)
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
                int a = DistanceFromPatientAndDoctorInMinutes(helpCallPoint, doctorPoint);



                return theMostSuitableDoctors;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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

        public static List<DTO.ContactsDoctor> GetContactsDoctorsFromGoogleAccount()
        {
            //todo: fill the function
            //link: https://developers.google.com/api-client-library/dotnet/guide/aaa_oauth
            /*
                public static void getContactsPhonesFromGoogleAcount()
        {
            PeopleResource.ConnectionsResource.ListRequest peopleRequest =
            peopleService.People.Connections.List("people/me");
            peopleRequest.PersonFields = "names,emailAddresses";
            ListConnectionsResponse connectionsResponse = peopleRequest.Execute();
            IList<Person> connections = connectionsResponse.Connections;
        }
             */
            return new List<DTO.ContactsDoctor>();
        }
        public static List<DTO.Doctor> GetAllDoctors()
        {
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {

            }
            return new List<DTO.Doctor>();
        }
        public static bool AddStatisficationRateToDoctorByCase(int helpCallID)
        {
            //todo: fill the function
            return true;
        }
        public static void SendMailToConfirmDoctor(DAL.Doctor doctor)
        {
            string email = "neighbooraid@gmail.com";
            string password = "VSRkhrz123";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress("l0556700640@gmail.com"));
            msg.To.Add(new MailAddress("hadarotmi123@gmail.com"));

            //msg.To.Add(new MailAddress("hadarotmi123@gmail.com"));
            msg.Subject = "אישור רופא";
            var base64 = Convert.ToBase64String(doctor.pictureDiploma);
            msg.Body = string.Format(
            #region htmlMessageBody
                //todo: to put the string in html file and read it with IOStream as string.
                @"
<div dir=' ltr'>
                    <h1>Hello!</h1>
                    <h3>There is a doctor who need your confirm:</h3>
                    <div style='
    position: relative;
    padding: 0.75rem 1.25rem;
    margin-bottom: 1rem;
    border: 1px solid transparent;
    border-radius: 0.25rem;
    color: #0c5460;
    width: 50%;
    background-color: #d1ecf1;
    border-color: #bee5eb;'
>
<label> Id: {0}</label>
                    <br />
                    <label> First Name: {1}</ label>
                    <br />
                    <label> Last Name: {2}</label>
                    <br />
                    <label> Phone Number: {3}</label>
                    <br />
                    <label> Address: {4}</label>
                    <br />
                    <br />
</div>
<div>
<div style='display: inline-block;'>
 <form action='https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/true' method='post' >
                        <button type='submit'
                           style='display: inline-block;
                           font-weight: 400;
                           text-align: center;
                           vertical-align: middle;
                           -webkit-user-select: none;
                           -moz-user-select: none;
                           -ms-user-select: none;
                           user-select: none;
                           border: 1px solid transparent;
                           padding: 0.375rem 0.75rem;
                           font-size: 1rem;
                           line-height: 1.5;
                           border-radius: 0.25rem;
                           transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
                           color: #fff;
                           background-color: #28a745;
                           border-color: #28a745;'>
                        Confirm
                          </button>
                        </form>
</div>
                     <form action='https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/false' method='post'>
                        <button type='submit'
                           style='display: inline-block;
                           font-weight: 400;
                           text-align: center;
                           vertical-align: middle;
                           -webkit-user-select: none;
                           -moz-user-select: none;
                           -ms-user-select: none;
                           user-select: none;
                           border: 1px solid transparent;
                           padding: 0.375rem 0.75rem;
                           font-size: 1rem;
                           line-height: 1.5;
                           border-radius: 0.25rem;
                           transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
                           color: #fff;
                           background-color: #dc3545;
                           border-color: #dc3545;'>
                        UnConfirm
                    </button>
                 </form>
                 </div>
</div>
"
                      , doctor.doctorId,
                       doctor.firstName,
                       doctor.lastName,
                       doctor.doctorPhone,
                       doctor.address
            #endregion
                       );
            msg.IsBodyHtml = true;
            //todo: delete;
            var pathOfTheFile = "";
            msg.Attachments.Add(new Attachment(pathOfTheFile));
            //todo: to save the files in folder of all the files and put there the path
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }
        public static void CallToTheDoctor(int doctorId)
        {
            //todo: endTheFunc
        }

        public static List<DTO.Doctor> DoctorsToCase(DTO.Cases correntCase)
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
                        .Where(c => c.Doctor.isConfirmed == true
                        && c.satisfaction >= satisfactorySatisfaction)
                        .Select(c => c.Doctor)
                        .ToList();
                }
                foreach (var doctor in doctorsOfCorrentCaseFromDB)
                {
                    doctorsToCorrentCase.Add(
                        Convertors.DoctorConvertor.ConvertDoctorToDTO(doctor));
                }
            }
            catch { }
            return doctorsToCorrentCase;
        }
    }
}
