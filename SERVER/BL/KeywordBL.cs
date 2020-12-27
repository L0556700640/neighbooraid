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
        public static int AddKeyword(DTO.Keyword keyword)
        {
            try
            {
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    DAL.Keyword newKeyword = new DAL.Keyword();
                    newKeyword= Convertors.KeywordConvertors.ConvertKeywordsToDAL(keyword);

                    db.Keywords.Add(newKeyword);
                    db.SaveChanges();


                    return newKeyword.keywordId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (-1);
            }
        }

        public static bool AddCasesToKeyword(DTO.Keyword keyword, List<DTO.Cases> casesToKeyword)
        {
            try
            {
                using (neighboorAidDBEntities db = new neighboorAidDBEntities())
                {
                    DAL.Keyword myKeyword = Convertors.KeywordConvertors.ConvertKeywordsToDAL(keyword);
                    foreach (var c in casesToKeyword)
                    {
                        DAL.Case c1 = Convertors.CaseConvertor.ConvertCaseToDAL(c);
                        db.Keywords.Find(myKeyword).Cases.Add(c1);
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
            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                dalKeyWords = db.Keywords.Include(k=>k.Cases).ToList();
            }
            List<DTO.Keyword> dtoKeyWords = new List<DTO.Keyword>();
            foreach (var w in dalKeyWords)
            {
                dtoKeyWords.Add(
                    Convertors.KeywordConvertors.ConvertKeywordsToDTO(w)
                    );
            }
            //dtokeywords.keyword1;
            //words
            List<DTO.Keyword> keywordsToAdd = new List<DTO.Keyword>();
            List<DTO.Cases> relatedCases = new List<DTO.Cases>();
            bool flag = false;
            foreach (var word in words)
            {
                foreach (var keyword in dtoKeyWords)
                {
                    if (word.Equals(keyword))
                    {
                        flag = true;
                        foreach (var c in keyword.Cases)
                        {
                            relatedCases.Add(c);
                        }
                    }
                }
                if (!flag)
                {
                    keywordsToAdd.Add(new DTO.Keyword(word));
                }
            }
            foreach (var newKeyword in keywordsToAdd)
            {
                int a=AddKeyword(newKeyword);
            }
            //todo: return the correct list
            //todo: save the searchWords- in the db: now or after the patient choose?
            return relatedCases;
        }
    }
}
