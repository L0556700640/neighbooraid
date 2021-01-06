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
                return false;
            }
        }
    }
}
