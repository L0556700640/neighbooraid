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
using System.Text.Json;
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
                            satisfaction = 75
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
            DAL.Doctor doctor;
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                    doctor = db.Doctors.FirstOrDefault(d => d.doctorId.Equals(doctorId));
                    db.Doctors.FirstOrDefault(d => d.doctorId.Equals(doctorId))
                        .isConfirmed = isConfirmed;
                    db.SaveChanges();
            }
            SendMailToDoctorAfterConfirm(doctor);

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
        public static List<DTO.CasesToDoctor> GetCasesToDoctor(string id)
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

        public static bool UpdateDoctorDetailsBL(DTO.Doctor doctorDetails)
        {

                try
                {
                    using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                    {
                        DAL.Doctor newDoctor = Convertors.DoctorConvertor.ConvertDoctorToDAL(doctorDetails);
                        db.Doctors.FirstOrDefault(d=>d.doctorId==newDoctor.doctorId).address=newDoctor.address;
                        db.Doctors.FirstOrDefault(d=>d.doctorId==newDoctor.doctorId).certificateNumber=newDoctor.certificateNumber;
                        db.Doctors.FirstOrDefault(d=>d.doctorId==newDoctor.doctorId).certificateValidity=newDoctor.certificateValidity;
                        db.Doctors.FirstOrDefault(d=>d.doctorId==newDoctor.doctorId).doctorPhone=newDoctor.doctorPhone;
                        db.Doctors.FirstOrDefault(d=>d.doctorId==newDoctor.doctorId).firstName=newDoctor.firstName;
                        db.Doctors.FirstOrDefault(d=>d.doctorId==newDoctor.doctorId).isConfirmed=newDoctor.isConfirmed;
                        db.Doctors.FirstOrDefault(d=>d.doctorId==newDoctor.doctorId).lastName=newDoctor.lastName;
                        db.Doctors.FirstOrDefault(d=>d.doctorId==newDoctor.doctorId).mail=newDoctor.mail;
                        db.Doctors.FirstOrDefault(d=>d.doctorId==newDoctor.doctorId).pictureDiploma=newDoctor.pictureDiploma;
                        db.SaveChanges();
                        SendMailToDoctorAfterConfirm(newDoctor);

                    return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                return false;
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
        public static ReturnedDoctorsToCase GetDoctorsToCase(int helpCallId, int caseId, string contactsListUrl)
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
                        db.Cases.FirstOrDefault(c => c.caseId == caseId)
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
                return new ReturnedDoctorsToCase(
                        GetContactsDoctorsFromGoogleAccount(helpCallId, doctorsToCorrentCase, contactsListUrl),
                        GetDoctorsByDistance(helpCallId, doctorsToCorrentCase)
                        );

                //todo: from google people
                //todo: by calc the distance

                //string doctorPoint = "";
                //string helpCallPoint = "";
                //int a = DistanceFromPatientAndDoctorInMinutes(helpCallPoint, doctorPoint);

                //return theMostSuitableDoctors;
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

                string duration = xmlDoc.DocumentElement.SelectSingleNode("//DistanceMatrixResponse/row/element/duration/value").InnerText;
                return Convert.ToInt32(duration) / 60;
            }
            catch (Exception)
            {
                return -1;
            }
            //return TravelingTime
        }
        public static List<DTO.CloseDoctor> GetDoctorsByDistance(int helpCallId, List<DTO.Doctor> relatedDoctors)
        {
            List<CloseDoctor> closeDoctor = new List<CloseDoctor>();
            string patientPoint = HelpCallBL.ConvertPointsToAddress(helpCallId);
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                foreach (var doctor in relatedDoctors)
                {
                    closeDoctor.Add(
                        new CloseDoctor(
                            doctor,
                    DistanceFromPatientAndDoctorInMinutes(patientPoint, doctor.address),
                    db.CasesToDoctors
                    .FirstOrDefault(myCase => myCase.doctorId == doctor.doctorId
                    &&
                    myCase.caseId == db.HelpCalls.FirstOrDefault(hc => hc.callId == helpCallId).caseId)
                    .satisfaction
                     ));
                }
            }
            return closeDoctor.OrderBy(doctor => doctor.Satisfaction).ToList();

        }
        public static List<DTO.ContactsDoctor> GetContactsDoctorsFromGoogleAccount(int helpCallId, List<DTO.Doctor> relatedDoctors, string contactsListUrl)
        {
             List<ContactsDoctor> contactsDoctorsToThisCase = new List<ContactsDoctor>();
            List<DTO.Contact> contacts = new List<DTO.Contact>();
            //todo: fill the function
            var json="";
            using (WebClient wc = new WebClient())
            {
                 json = wc.DownloadString(contactsListUrl);
            }
                GoogleContacts googleContacts = new GoogleContacts();
                googleContacts = JsonSerializer.Deserialize<GoogleContacts>(json);
                foreach (var contact in googleContacts.feed.entry)
                {
                    if (contact.gdphoneNumber.Length > 0) {
                        contacts.Add(new Contact
                        {
                            Name = contact.gdname.gdfullName.t,
                            Phone = contact.gdphoneNumber[0].t
                        });
                     }
                }

            int s;
            foreach (var c in contacts)
            {
                foreach (var doctor in relatedDoctors)
                {
                    if (c.Phone.Equals(doctor.doctorPhone))
                    {
                        using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                        {
                            s = db.CasesToDoctors
                                .FirstOrDefault(ctd => ctd.doctorId == doctor.doctorId
                            && ctd.Case.caseId ==
                            (db.HelpCalls
                            .FirstOrDefault(h => h.callId == helpCallId).caseId))
                                .satisfaction;
                        }
                        contactsDoctorsToThisCase.Add(new ContactsDoctor(doctor,c.Name,s));
                    }
                }
            }
            return contactsDoctorsToThisCase;
        }
        public static List<DTO.Doctor> GetAllDoctors()
        {
            List<DTO.Doctor> doctors= new List<DTO.Doctor>();

            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                foreach (var d in db.Doctors)
                {
                    doctors.Add(Convertors.DoctorConvertor.ConvertDoctorToDTO(d));
                }
            }
            return doctors;
        }
        public static bool AddStatisficationRateToDoctorByCase(int helpCallID, int statisfication)
        {
            string correntDoctor;
            int correntCase, correntStatisfication, amountOfCallsToThisCase;
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                correntDoctor = db.HelpCalls.FirstOrDefault(h => h.callId == helpCallID).doctorId;
                correntCase = (int)db.HelpCalls.FirstOrDefault(h => h.callId == helpCallID).caseId;
                correntStatisfication = db.CasesToDoctors
                   .FirstOrDefault(ctd => ctd.doctorId == correntDoctor
               && ctd.Case.caseId == correntCase).satisfaction;
                amountOfCallsToThisCase = db.HelpCalls.Where((h => h.caseId == correntCase && h.doctorId.Equals(correntDoctor))).Count();

                db.CasesToDoctors
                        .FirstOrDefault(ctd => ctd.doctorId == correntDoctor
                    && ctd.Case.caseId == correntCase).satisfaction =
                    (correntStatisfication * (amountOfCallsToThisCase - 1) + statisfication) / amountOfCallsToThisCase;
            }
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
        public static List<ContactsDoctor> GetContactsFromDoctors(DTO.Cases choosedCase, List<DTO.ContactsDoctor> contacts)
        {
            List<ContactsDoctor> contactsDoctorsToThisCase = new List<ContactsDoctor>();
            List<DTO.Doctor> doctorToCorrentCase = DoctorsToCase(choosedCase);

            foreach (var c in contacts)
            {
                foreach (var doctor in doctorToCorrentCase)
                {
                    if (c.Doctor.doctorPhone.Equals(doctor.doctorPhone))
                    {
                        contactsDoctorsToThisCase.Add(c);
                    }
                }
            }

            return contactsDoctorsToThisCase;
        }
        public static void SendMailToDoctorAfterConfirm(DAL.Doctor doctor)
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
                msg.To.Add(new MailAddress(doctor.mail));
                msg.Subject = "אישור הרשמה לNeighborAid עבור דר'  " + doctor.lastName;

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
                               <img src='cid:{2}' alt='Alternate Text' style='height: 100px;' />
                               <h1>שלום!</h1>
                               <h3>הרשמתך לNeighborAid הסתימה בהצלחה.</h3>
                               <h3> הידע שברשותך מועיל לאנשים רבים הזקוקים לו</h3>
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
                               <h6>כדי להתחבר לאפליקציה עליך להזין את הפרטים הבאים</h6>
                               <label> שם פרטי: {0}</label>
                               <br />
                               <label> תעודת זהות: {1}</label>
                               <br />
                           </div>
                  <h3>אנחנו מודים לך על נכונותך להציל חיים!</h3>
                 </div>"
                             , doctor.firstName,
                            doctor.doctorId,
                             res.ContentId);
                #endregion
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBodyString, null, MediaTypeNames.Text.Html);
                alternateView.LinkedResources.Add(res);
                msg.AlternateViews.Add(alternateView);
                msg.IsBodyHtml = true;
           //     msg.Attachments.Add(new Attachment(doctor.pictureDiploma));
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

    }
}
