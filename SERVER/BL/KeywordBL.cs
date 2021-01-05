using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DTO;

namespace BL
{
    public class KeywordBL
    {
        public static List<DTO.Keyword> keywordsInThisSearch=null;


        public static DTO.Keyword AddKeyword(DTO.Keyword keyword)
        {
            try
            {
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    DAL.Keyword newKeyword = new DAL.Keyword();
                    newKeyword= Convertors.KeywordConvertors.ConvertKeywordsToDAL(keyword);

                    db.Keywords.Add(newKeyword);
                    db.SaveChanges();


                    return Convertors.KeywordConvertors.ConvertKeywordsToDTO(newKeyword);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (null);
            }
        }
        //todo: end the func
        public static bool AddCaseToKeywords(DTO.Cases theChoosedCase)
        {
            try
            {
                DAL.Case choosedCase = new Case();
                choosedCase = Convertors.CaseConvertor.ConvertCaseToDAL(theChoosedCase);
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    foreach (var k in keywordsInThisSearch)
                    {
                        foreach (var item in db.Keywords)
                        {
                            if (k.keyWord1 == item.keyWord1)
                            {
                          //todo: add relate cases to this keyword, or add relation to execute keyword to case
                                //if(db.);
                            }
                        }
                        //{
                        //    if(k.Cases.Contains(choosedCase))
                        //}

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


        public static List<Cases> GetRelatedCasesByKeywords(List<string> words)
        {
            List<DAL.Keyword> dalKeyWords = new List<DAL.Keyword>();
            List<DAL.KeywordsToCase> dalKeywordsToCases = new List<DAL.KeywordsToCase>();
            List<DAL.Case> dalCases = new List<DAL.Case>();
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                dalKeyWords = db.Keywords.ToList();
                dalKeywordsToCases = db.KeywordsToCases.ToList();
                dalCases = db.Cases.ToList();
            }
            List<DTO.Keyword> dtoKeyWords = new List<DTO.Keyword>();
            foreach (var w in dalKeyWords)
            {
                dtoKeyWords.Add(
                    Convertors.KeywordConvertors.ConvertKeywordsToDTO(w)
                    );
            }
            List<DTO.Keyword> keywordsToAdd = new List<DTO.Keyword>();
            List<DTO.Cases> relatedCases = new List<DTO.Cases>();
            //todo: find way to get the relatedCases orderby keywordToCase.numberOfUseThisRelation
            bool flag = false;
            foreach (var word in words)
            {
                foreach (var keyword in dtoKeyWords)
                {
               if (word.Equals(keyword.keyWord1))
                    {
                        flag = true;
                        keywordsInThisSearch.Add(keyword);
                        foreach (var kc in dalKeywordsToCases)
                        {
                            if(kc.keywordId==keyword.keywordId)
                            {
                              relatedCases.Add(
                                  Convertors.CaseConvertor.ConvertCaseToDTO(
                                  (Case)
                                  dalCases.Select(c => c)
                                  .Where(c => c.caseId == kc.caseId))
                                  );
                            }
                        }
                        break;
                    }
                }
                if (!flag)
                {
                    keywordsToAdd.Add(new DTO.Keyword(word));
                }
            }
            foreach (var newKeyword in keywordsToAdd)
            {
              BL.KeywordBL.keywordsInThisSearch.Add(AddKeyword(newKeyword));
            }
            //todo: keyword to case- save the searchWords- in the db: now or after the patient choose?
            return relatedCases;
        }


        public static bool SaveTheCorrentCaseToKeywords(Cases correntCase, List<DTO.Keyword> keywordsInThisSearchFromXMLFile)
        {
            try
            {
                DAL.KeywordsToCase ktc = null;
                DAL.KeywordsToCase c = null;


                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    foreach (var keywordToCase in keywordsInThisSearch)
                    {
                        DAL.KeywordsToCase kwc  = db.KeywordsToCases.FirstOrDefault(w => w.Keyword.keyWord1 == keywordToCase.keyWord1 && w.caseId == correntCase.caseId);
                        if (kwc!=null)
                        {
                            //c = (DAL.KeywordsToCase)db.KeywordsToCases.Select(k => k.keywordId == ktc.keywordId && k.caseId == ktc.caseId);
                            kwc.numOfUsingThisRelation++;
                        }

                        else
                        {
                            db.KeywordsToCases.Add(new DAL.KeywordsToCase {
                                caseId=correntCase.caseId,
                                keywordId=keywordToCase.keywordId,
                                numOfUsingThisRelation=1

                            });
                        }
                    }
                    db.SaveChanges();
                }
                    return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
