using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BL
{
    public class HelpCallBL
    {
        public static int helpCallId = 1;
        public static int SaveHelpCallInDB(DTO.HelpCall helpCall)
        {

            try
            {
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    DAL.HelpCall newhelpcall = Convertors.HelpCallConvertor.ConvertHelpCallToDAL(helpCall);
                    DAL.HelpCall myhelpcall = new HelpCall();
                        //string diplomaDocumentPath = "//";//todo: send the file path and save in db
                    newhelpcall.caseId = 0;
                    newhelpcall.doctorId = "";
                    newhelpcall.callId = db.HelpCalls.Count()+1;
                  myhelpcall=  db.HelpCalls.Add(newhelpcall);
                    db.SaveChanges();

                    return myhelpcall.callId;
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
            string latLng;
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                 latLng =
                    db.HelpCalls.FirstOrDefault(h => h.callId == helpCallId).addressX +
                    "," +
                    db.HelpCalls.FirstOrDefault(h => h.callId == helpCallId).addressY;
            }
            string key = "AIzaSyBrXhPtMorEH1jvdOptRJsshnym-Ut5bw0";
            string uri = "https://maps.google.com/maps/api/geocode/xml?key=" + key + "&latlng=" + latLng + "&sensor=false";
            WebClient wc = new WebClient();
            try
            {
                string geoCodeInfo = wc.DownloadString(uri);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(geoCodeInfo);

                string formattedAddress = xmlDoc.DocumentElement.SelectSingleNode("/GeocodeResponse/result/formatted_address").InnerText;
                return formattedAddress;
            }
            catch (Exception)
            {
                return "";
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
