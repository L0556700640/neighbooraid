using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls;

namespace BL
{
    public class ManagerBL
    {
        public static void SendMailToConfirmDoctor(DAL.Doctor doctor)
        {
            #region try old error gmail.com not valid
            //string from = "neighbooraid/@gmail/.com‏";
            //bool isValid = ValidEmail.IsValidEmail(from);
            //string to = "l0556700640/@gmail/.com";
            //var fromAddress = new MailAddress(from, "NeiboorAid");
            //var toAddress = new MailAddress(to, "Liraz");
            //const string fromPassword = "VSRkhrz123";
            //const string subject = "אישור רופא";
            //string body = String.Format(@"<a href=""https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/true"">Confirm</a><br/> <a href=""https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/true"">Confirm</a>", id);

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp/.gmail/.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            //};

            //try
            //{
            //    using (var message = new MailMessage(fromAddress, toAddress)
            //    {
            //        Subject = subject,
            //        Body = body,
            //        IsBodyHtml = true
            //    })
            //    {
            //        //     smtp.Send("neighbooraid@gmail.com‏", "l0556700640@gmail.com", "confirm", "try sent a mail");

            //        smtp.Send(message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Type type = ex.GetType();
            //}
            #endregion
            #region ניסוי לא מוצלח (לא שינה כלום)ך
            //try
            //{
            //    MailMessage mail = new MailMessage();
            //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            //    mail.From = new MailAddress("neighbooraid@gmail.com‏");
            //    mail.To.Add("l0556700640@gmail.com");
            //    mail.To.Add("hadarotmi123@gmail.com");

            //    mail.Subject = "Test Mail - 1";

            //    mail.IsBodyHtml = true;
            //    string htmlBody;

            //    htmlBody = String.Format(@"<a href=""https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/true"">Confirm</a><br/> <a href=""https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/true"">Confirm</a>", id);

            //    mail.Body = htmlBody;

            //    SmtpServer.Port = 587;
            //    SmtpServer.Credentials = new System.Net.NetworkCredential("Neighboor Aid", "VSRkhrz123");
            //    SmtpServer.EnableSsl = true;

            //    SmtpServer.Send(mail);
            // MessageBox.Show("mail Send");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}

            #endregion

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
    }
}
