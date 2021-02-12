using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DTO;
using System.Xml.Linq;

namespace BL
{
    public class KeywordBL
    {
        public static DTO.Keyword AddKeyword(DTO.Keyword keyword)

        {
            try
            {
             int keywordId= GetKeywordIdByKeyword(keyword.keyWord1);
                if (keywordId < 0)
                {
                    using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                    {
                        db.Keywords.Add(Convertors.KeywordConvertors.ConvertKeywordsToDAL(new DTO.Keyword(keyword.keyWord1)));
                        db.SaveChanges();

                    }
                }
                return new DTO.Keyword
                {
                    keywordId = GetKeywordIdByKeyword(keyword.keyWord1),
                    keyWord1 = keyword.keyWord1
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null);
            }
        }

        public static bool SaveTheCorrentCaseToKeywords( int helpCallID, int correntCase)

        {
            try
            {
                List<DTO.Keyword> keywordsInThisSearch = new List<DTO.Keyword>();
                keywordsInThisSearch = ReadHelpCallTpXML(helpCallID);
                DAL.Case choosedCase = new Case();
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    foreach (var keyword in keywordsInThisSearch)
                    {
                        if (db.KeywordsToCases.Any(
                            ktc => ktc.Keyword.keyWord1.Equals(keyword.keyWord1)
                            && ktc.caseId == correntCase
                            ))
                            db.KeywordsToCases.First(ktc => ktc.Keyword.keyWord1.Equals(keyword.keyWord1)
                       && ktc.caseId == correntCase).numOfUsingThisRelation++;
                        else
                            db.KeywordsToCases.Add(
                                Convertors.KeywordToCaseConvertor.ConvertKeywordsToCaseToDAL
                                (new DTO.KeywordsToCase(correntCase, keyword.keywordId)));
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (false);
            }
        }

        public static List<Cases> GetRelatedCasesByKeywords(int helpCallID, List<string> words)
        {
            //this function get the keywords and "understand" by last searchs which cases can be suitable to this help call
            //all the keywords saved: we want to know them and the case it related to
            // in order to use them in the next searchs.
            List<DTO.Keyword> keywordsInThisSearch = new List<DTO.Keyword>();
            //take all the old related information from the database about relationship between the words
            List<DAL.KeywordsToCase> dalKeywordsToCases = new List<DAL.KeywordsToCase>();
            List<DAL.Keyword> dalKeywords = new List<DAL.Keyword>();
            List<DAL.Case> dalCases = new List<DAL.Case>();
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {

                dalKeywords=db.Keywords.ToList();
                dalCases=db.Cases.ToList();
                dalKeywordsToCases = db.KeywordsToCases.ToList();
            }
            //check the keywords:
            //if it new keyword- we have to save it in the database.
            //if this keyword used in the past- we have to see in which cases it used.
            //in short- to see if this keyword tell us somthing about the case
            List<DTO.Keyword> keywordsToAdd = new List<DTO.Keyword>();
            List<DTO.RelatedCase> relatedCases = new List<DTO.RelatedCase>();
            bool isUsed = false;
            foreach (var word in words)
            {
                foreach (var keyword in dalKeywordsToCases)
                {
                    if (word.Equals(keyword.Keyword.keyWord1))
                    {
                        isUsed = true;
                        keywordsInThisSearch.Add(
                            Convertors.KeywordConvertors.ConvertKeywordsToDTO
                            (keyword.Keyword));
                        if (relatedCases.Any(someCase => someCase.relatedCase.caseId == keyword.caseId))
                            relatedCases.First(someCase => someCase.relatedCase.caseId == keyword.caseId).sumOfNumOfUsingThisSearch +=
                                keyword.numOfUsingThisRelation;
                        else
                        {
                            relatedCases.Add(new RelatedCase
                                (
                                Convertors.CaseConvertor.ConvertCaseToDTO(
                                    dalCases.Find(c => c.caseId == keyword.caseId)),
                                keyword.numOfUsingThisRelation
                                ));
                        }
                    }
                }
                if (!isUsed)
                {
                    //there is few actions in one sentence:
                    //1. create new DTO.keyword();
                    //2. add the keyword to the db
                    //3. get the keyword+keywordID from the db
                    //4. save the keyword as one of the keyword in this search, in order to add the related case.
                    keywordsInThisSearch.Add(AddKeyword(new DTO.Keyword(word)));
                }
            }
            bool saveTheKeywordsInXML= WriteHelpCallTpXML(helpCallID, keywordsInThisSearch);
            //now we have the keywords of this search save
            //until the patient choose which case from the related cases
            //or all the cases is the correct case for this keywords.
            //when he choose the correct case we save in the db
            //that the keywords related to the choosed case

            return relatedCases
                .OrderByDescending(someCase => someCase.sumOfNumOfUsingThisSearch)
                .Select(someCase => someCase.relatedCase)
                .ToList();
        }

        public static bool WriteHelpCallTpXML(int helpCallID, List<DTO.Keyword> keywordsList)
        {
            try
            {
                //  string xmlFileFullPath = DTO.StartPoint.LirazHadar+"BL\\HelpCallXMLS\\CorrentHelpCall.xml";
                string xmlFileFullPath = DTO.StartPoint.Liraz + "BL\\HelpCallXMLS\\CorrentHelpCall.xml";
                XDocument helpCallDocument = XDocument.Load(xmlFileFullPath);

                XElement newHelpCall = new XElement("helpCall");
                newHelpCall.Add(new XAttribute("id", helpCallID));
                XElement keywordsRoot = new XElement("keywords");
                XElement keyword = null;
                foreach (var word in keywordsList)
                {
                    keyword = new XElement("keyword", word.keyWord1);
                    keyword.Add(new XAttribute("keywordId", word.keywordId));
                    keywordsRoot.Add(keyword);
                }
                newHelpCall.Add(keywordsRoot);
                helpCallDocument.Root.Add(newHelpCall);

                helpCallDocument.Save(xmlFileFullPath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return false;
            }
        }
        public static List<DTO.Keyword> ReadHelpCallTpXML(int helpCallID)
        {
            try
            {
                XDocument helpCallDocument = XDocument.Load("HelpCallXMLS/CorrentHelpCall.xml");
                var helpCalls = helpCallDocument.Descendants("helpCall");
                //todo: check- this is problem (cast)?
                List<XElement> keywordsInCorrentCall = (List<XElement>)
                                            (from hc in helpCalls.Elements()
                                            where hc.Name == "helpCall" &&
                                            int.Parse(hc.Attribute("id").Value) == helpCallID
                                            select hc.Element("keywords").Elements("keyword").ToList());
                List<DTO.Keyword> keywordsInThisSearch = new List<DTO.Keyword>();
                foreach (var keywordElement in keywordsInCorrentCall)
                {
                    keywordsInThisSearch.Add(
                        new DTO.Keyword
                    {
                        keywordId = int.Parse(keywordElement.Attribute("keywordId").Value),
                        keyWord1 = keywordElement.Value
                    });
                }
                return keywordsInThisSearch;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public static int GetKeywordIdByKeyword(string keyword)
        {
            List<DAL.Keyword> keywords = new List<DAL.Keyword>();

            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                keywords = db.Keywords.ToList();
            }
            if (keywords.Where(kw => kw.keyWord1.Equals(keyword)).Count() > 0)
            {
                return keywords.FirstOrDefault(k => k.keyWord1.Equals(keyword)).keywordId;
            }
            else
                return -1;
        }


    }
}
