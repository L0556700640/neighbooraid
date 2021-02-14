using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DTO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace BL
{
    public class CasesBL
    {
        public static bool AddCase(DTO.Cases newCase)
        {
            //todo: fill the function
            return true;
        }
        public static List<DTO.Cases> getAllCases()
        {
            try
            {
                List<DAL.Case> dalCases = new List<Case>();
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    dalCases = db.Cases.ToList();
                }
                List<DTO.Cases> dtoCases = new List<DTO.Cases>();
                foreach (var c in dalCases)
                {
                    dtoCases.Add(
                        Convertors.CaseConvertor.ConvertCaseToDTO(c)
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

        public static string getCaseName(string id)
        {

            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                var case1=(from c in db.Cases
                               where c.caseId.Equals(id)
                               select c).ToList();
                return case1.Select(c => Convertors.CaseConvertor.ConvertCaseToDTO(c).caseName).FirstOrDefault();
                // db.SaveChanges();
            }

        }
        public static List<DTO.Cases> GetTheCasesRelatedByTheSearch(int helpCallID, string sentence)
        {
            string translateSentence = TranslateBL.Translate(sentence);
            List<string> importantWords = new List<string>();
            importantWords = TranslateBL.Analysis(translateSentence);

            List<DTO.Cases> relatedCases = new List<DTO.Cases>();
            relatedCases = KeywordBL.GetRelatedCasesByKeywords(helpCallID, importantWords);

            return relatedCases;
        }

        public static bool UpdateCasesToDoctorBL(string doctorID, List<Cases> newCasesList)
        {
            //todo: end the function
            List<DAL.CasesToDoctor> allCasesToThisDoctor= new List<DAL.CasesToDoctor>();
            List<DAL.CasesToDoctor> casesToConfirm= new List<DAL.CasesToDoctor>();
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                allCasesToThisDoctor= db.CasesToDoctors.Where(doc => doc.doctorId.Equals(doctorID)).ToList();
            }
            foreach (var oneCase in newCasesList)
            {
               UpdateCaseToDoctor(doctorID,oneCase.caseId,0);
            }
            casesToConfirm = allCasesToThisDoctor.Where(c => c.satisfaction < 50).ToList();
            sendMailToUpdateCases(casesToConfirm);
            return true;
        }

        private static void sendMailToUpdateCases(List<DAL.CasesToDoctor> casesToConfirm)
            {
                try
                {
                    DAL.Doctor doctor = casesToConfirm[0].Doctor;
                    string email = "neighbooraid@gmail.com";
                    string password = "VSRkhrz123";

                    var loginInfo = new NetworkCredential(email, password);
                    var msg = new MailMessage();
                    var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                    msg.From = new MailAddress(email);
                    msg.To.Add(new MailAddress("l0556700640@gmail.com"));
                    msg.To.Add(new MailAddress("hadarotmi123@gmail.com"));

                    msg.Subject = "עדכון התמחויות לרופא " + doctor.doctorId;

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
                               <h3>הרופא }0{ }1{ עדכן את פרטיו, וכעת הוא מחכה לאישור שלך.</h3>
                           </div>
                           "
                                 ,doctor.firstName,
                                 doctor.lastName,
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
                                         רשימת ההתמחויות החדשה: </b > <br />

                ");


                    foreach (var item in casesToConfirm)
                    {
                        htmlBodyString += string.Format(@"<label> {0}- {3} נקודות כרגע</ label> <br />
                        <form action ='https://localhost:44314/API/Cases/ConfirmCase/{1}/{2}/50' method='post' style='display:inline-block'>
                                 <button type='submit'
                                       style='display: inline-block !important; text-align: center !important; border: 1px solid transparent !important; border-radius: 5px !important; padding: 10px !important; margin: 5% !important; color: #fff !important; background-color: #28a745 !important; border-color: #28a745 !important; margin-bottom: 5px !important; font-family: Amerald !important; font-size: medium !important;'>   
                                    מאשר
                           </button>
                        </form>
                        <form action = 'https://localhost:44314/API/Doctor/ConfirmDoctor/{1}/{2}/50' method = 'post' style = 'display:inline-block' >
                             <button type='submit'
                                   style='display: inline-block !important; text-align: center !important; border: 1px solid transparent !important; border-radius: 5px !important; padding: 10px !important; margin: 5% !important; color: #fff !important; background-color: #dc3545 !important; border-color: #dc3545 !important; margin-bottom: 5px !important; font-family: Amerald !important; font-size: medium !important;'>
                                לא מאשר
                             </button>

                        </form>
                        ", item.Case.caseName, item.caseId,item.doctorId, item.satisfaction);
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
                <h4> לאחר אישור מקרה, המערכת תציג את הרופא ככשיר לתת שירות בנושא שצוין.</h4>
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

        public static bool UpdateCaseToDoctor(string doctorId, int caseId, int statisfication)
        {
            try
            {

                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    if (db.CasesToDoctors.ToList().Exists
                        (c => c.caseId == caseId && c.doctorId == doctorId) == false)
                    {
                        db.CasesToDoctors.Add(new DAL.CasesToDoctor
                        {
                            caseId = caseId,
                            doctorId = doctorId,
                            satisfaction = statisfication
                        });
                    }
                    else
                    {
                        db.CasesToDoctors
                            .FirstOrDefault(ctd => ctd.caseId == caseId && ctd.doctorId == doctorId)
                            .satisfaction = statisfication;
                    }
                    db.SaveChanges();

                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public static ReturnedDoctorsToCase CaseChose(int helpCallID, int choosedCase, string contactsListUrl)
        {
            try
            {
                UpdateCaseToHelpCall(helpCallID, choosedCase);
              KeywordBL.SaveTheCorrentCaseToKeywords(helpCallID, choosedCase);
                return DoctorBL.GetDoctorsToCase(helpCallID,choosedCase,contactsListUrl);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        private static void UpdateCaseToHelpCall(int helpCallID, int choosedCase)
        {
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                db.HelpCalls.FirstOrDefault(h => h.callId == helpCallID).caseId = choosedCase;
                db.SaveChanges();
            }
        }
    }
}
