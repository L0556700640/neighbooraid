using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
namespace BL
{
    public class ManagerBL
    {

        public static void SendMailToConfirmDoctor(int id)
        {
            var fromAddress = new MailAddress("neighbooraid@gmail.com‏", "NeiboorAid");
            var toAddress =  new MailAddress("l0556700640@gmail.com", "Liraz") ;
            const string fromPassword = "VSRkhrz123";
            const string subject = "אישור רופא";
            string body = String.Format( @"<a href=""https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/true"">Confirm</a><br/> <a href=""https://localhost:44314/API/Doctor/ConfirmDoctor/{0}/true"">Confirm</a>",id);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml=true
            })
            {
                smtp.Send(message);
            }
        }
    }
}
