using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BL
{
    public class HelpCallBL
    {
        public static bool writeHelpCallTpXML(int helpCallID, List<DTO.Keyword> keywordsList)
        {
            try
            {
                XDocument helpCallDocument = XDocument.Load("../../CorrentHelpCall.xml");

                XElement newHelpCall = new XElement("helpCall");
                newHelpCall.Add(new XAttribute("id", helpCallID));
                XElement keywordsRoot = new XElement("keyword");
                XElement keyword = null;
                foreach (var word in keywordsList)
                {
                    keyword = new XElement("keyword", word);
                    keywordsRoot.Add(keyword);
                }
                newHelpCall.Add(keywordsRoot);
                helpCallDocument.Root.Add(newHelpCall);

                helpCallDocument.Save("../../XMLS/RuthFilleByXDocument.xml");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public static bool SaveHelpCallInDB(DTO.HelpCall helpCall)
        {
            try
            {
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    DAL.HelpCall newHelpCall = Convertors.HelpCallConvertor.ConvertHelpCallToDAL(helpCall);
                    //string diplomaDocumentPath = "//";//todo: send the file path and save in db
                
                    db.HelpCalls.Add(newHelpCall);
                    db.SaveChanges();
                    //לבדוק אם נכון להעביר בצורה כזו למסד הנתונים (בלי המרה) ב
                   
                   
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            //todo: delete the raw
            //  helpCall = helpCall;
            //todo: fill the function
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
