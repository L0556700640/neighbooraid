using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BL
{
    public class HelpCallBL
    {

        //public static int helpCallId = 1;
        public static int SaveHelpCallInDB(DTO.HelpCall helpCall)
        {

            try
            {
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    DAL.HelpCall newhelpcall = Convertors.HelpCallConvertor.ConvertHelpCallToDAL(helpCall);
                    //string diplomaDocumentPath = "//";//todo: send the file path and save in db
                   // newhelpcall.caseId = null;
                   // newhelpcall.doctorId = "";
                   // newhelpcall.callId = db.HelpCalls.Count()+1;
                    db.HelpCalls.Add(newhelpcall);
                    db.SaveChanges();

                    return newhelpcall.callId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return -1;
            }

            //todo: delete the raw
            //  helpCall = helpCall;
            //todo: fill the function
        }
        public static string ConvertPointsToAddress(int helpCallId)
        {

            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                string latLng =
                    db.HelpCalls.FirstOrDefault(h => h.callId == helpCallId).addressX +
                    "," +
                    db.HelpCalls.FirstOrDefault(h => h.callId == helpCallId).addressY;
                //string key ="AIzaSyBrXhPtMorEH1jvdOptRJsshnym-Ut5bw0";
               // string key = "";
                string uri = "https://maps.google.com/maps/api/geocode/xml?key=" + "AIzaSyD6s4nF_IyVnzwthb1kIIje709yY7Tl1nY" + "&latlng=" + latLng + "&sensor=false";
               // string uri2 = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latLng + "&key=" + key;

                try
                {
                    WebClient wc = new WebClient();
                    string geoCodeInfo = wc.DownloadString(uri);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(geoCodeInfo);
                    //string formattedAddress = "";
                    string formattedAddress = xmlDoc.DocumentElement.SelectSingleNode("/GeocodeResponse/result/formatted_address").InnerText;
                    return formattedAddress;
                }
                catch (Exception)
                {
                    return "";
                }

            }
        }
        public static DTO.HelpCall GetHelpCallByID(int helpCallID)
        {
            //todo: delete the raw
            helpCallID = helpCallID++;
            //todo: fill the function
            return new DTO.HelpCall();
        }


    }
}
