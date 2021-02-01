using DAL;
using DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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
                    string pathToGetExtension = string.Format(@"c:\" + file.FileName);
                    string diplomaDocumentNewPath = DTO.StartPoint.Liraz+"DAL\\Files\\"+doctor.ToString()+Path.GetExtension(pathToGetExtension);
                    file.SaveAs(diplomaDocumentNewPath);
                    newDoctor.pictureDiploma = diplomaDocumentNewPath;

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
        public static void ConfirmDoctor(string doctorId, bool isConfirmed)
        {
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                (from d in db.Doctors
                 where d.doctorId.Equals(doctorId)==true
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
                //todo: from google people
                //todo: by calc the distance
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
        public static void GetDoctorCoordinates(string doctorAddress)
        {
            string uri = "https://maps.googleapis.com/maps/api/geocode/json?address="+doctorAddress + "&key=AIzaSyA5L81_-5d2Hy7hHsNVhodk1zS90Qu-aP8";
            WebClient wc = new WebClient();
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
            //todo: end! fast!
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
            try
            {
                string email = "neighbooraid@gmail.com";
                string password = "VSRkhrz123";
                /*
                LinkedResource inline = new LinkedResource(DTO.StartPoint.Liraz + "DAL\\Files\\icon.jpg", MediaTypeNames.Image.Jpeg);
                inline.ContentId = Guid.NewGuid().ToString();
                avHtml.LinkedResources.Add(inline);
                */
                var loginInfo = new NetworkCredential(email, password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                msg.From = new MailAddress(email);
                msg.To.Add(new MailAddress("l0556700640@gmail.com"));
                msg.To.Add(new MailAddress("hadarotmi123@gmail.com"));

                msg.Subject = "אישור רופא "+doctor.doctorId;

                LinkedResource res = new LinkedResource(DTO.StartPoint.Liraz + "DAL\\Files\\icon.png");
                res.ContentId = Guid.NewGuid().ToString();



                #region buildHtmlMessageBody
                string htmlBodyString = string.Format(
                      @"
                       <div style='  direction: rtl;
                                     background-color: #EE8989;
                                     font-family: Amerald;
                                     font-size:medium; '>
                           <div style='text-align:center'>
                               <img src='cid:{5}' alt='Alternate Text' style='height: 100px;' />
                               <h1>שלום!</h1>
                               <h3>נרשם לאפליקציה רופא שמחכה לאישור שלך</h3>
                           </div>
                           <div style='  position: relative;
                                         padding: 0.75rem 1.25rem;
                                         margin-bottom: 1rem;
                                         margin-left: 7%;
                                         margin-right: 7%;
                                         border: 1px solid #5c060e;
                                         border-radius: 0.25rem;
                                         color: #5c060e;
                                         width: 75%;
                                         background-color: #f5c9c9;
                                         border-radius: 5px; '>
                               <label> תעודת זהות: {0}</label>
                               <br />
                               <label> שם פרטי: {1}</label>
                               <br />
                               <label> שם משפחה: {2}</label>
                               <br />
                               <label> מספר טלפון: {3}</label>
                               <br />
                               <label> כתובת: {4}</label>
                               <br />
                               <br />
                           </div>"

                           , doctor.doctorId,
                             doctor.firstName,
                             doctor.lastName,
                             doctor.doctorPhone,
                             doctor.address,
                             res.ContentId);
                htmlBodyString += string.Format(@"
                              <div style='position: relative;
                                           padding: 0.75rem 1.25rem;
                                           margin-bottom: 1rem;
                                           margin-left: 7%;
                                           margin-right: 7%;
                                           border: 1px solid #5c060e;
                                           border-radius: 0.25rem;
                                           color: #5c060e;
                                           width: 75%;
                                           background-color: #f5c9c9;
                                           border-radius: 5px; '>
                                         <b>
                                          רשימת ההתמחויות: </b > <br />

                ");

                List<DAL.Case> casesToThisDoctor = new List<Case>();
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    foreach (var oneCase in doctor.CasesToDoctors)
                    {
                        casesToThisDoctor.Add(db.Cases.Find(oneCase.caseId));
                    }
                }
                foreach (var item in casesToThisDoctor)
                {
                    htmlBodyString += string.Format(@"<label> {0} </ label> <br />", item.caseName);
                }
                htmlBodyString += string.Format(@"
         </div >
         <div style='position: relative;
                        padding: 0.75rem 1.25rem;
                        margin-bottom: 1rem;
                        margin-left: 7%;
                        margin-right: 7%;
                        border: 1px solid #5c060e;
                        border-radius: 0.25rem;
                        color: #5c060e;
                        width: 75%;
                        background-color: #f5c9c9;
                        border-radius: 5px;
                        text-align:center;'>
                <h5>
                    לתשומת לבך, מצורף מסמך המעיד על התמחויות המתמחה <br />
                    יש לעיין בו לפני אישור הרופא
                </h5>
         </div>
         <div style ='margin-right:25%;'>
                  <form action ='https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/true' method='post' style='display:inline-block'>
                                 <button type='submit'
                                       style='display: inline-block !important; text-align: center !important; border: 1px solid transparent !important; border-radius: 5px !important; padding: 10px !important; margin: 5% !important; color: #fff !important; background-color: #28a745 !important; border-color: #28a745 !important; margin-bottom: 5px !important; font-family: Amerald !important; font-size: medium !important;'>
                                                  מאשר את התווספות המתמחה למערכת
                           </button>
                          </form>

                <form action = 'https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/false' method = 'post' style = 'display:inline-block' >
                             <button type='submit'
                                   style='display: inline-block !important; text-align: center !important; border: 1px solid transparent !important; border-radius: 5px !important; padding: 10px !important; margin: 5% !important; color: #fff !important; background-color: #dc3545 !important; border-color: #dc3545 !important; margin-bottom: 5px !important; font-family: Amerald !important; font-size: medium !important;'>
                                איני מאשר את התווספות המתמחה למערכת
                        </button>

                </form>
        </div>
    </div>
"
 , doctor.doctorId);
                #endregion
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBodyString, null, MediaTypeNames.Text.Html);
                alternateView.LinkedResources.Add(res);
                msg.AlternateViews.Add(alternateView);
                msg.IsBodyHtml = true;
                msg.Attachments.Add(new Attachment(doctor.pictureDiploma));
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
