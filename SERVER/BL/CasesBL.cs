using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BL
{
    public class CasesBL
    {
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
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public static List<DTO.Cases> GetTheCasesRelatedByTheSearch(string sentence)
        {
            string translateSentence = TranslateBL.Translate(sentence);
            List<string> importantWords = new List<string>();
            importantWords= TranslateBL.Analysis(translateSentence);

            List<DTO.Cases> relatedCases = new List<DTO.Cases>();
            relatedCases = KeywordBL.GetRelatedCasesByKeywords(importantWords);
            #region function in KeywordBL
            // List<DAL.Keyword> dalKeyWords = new List<Keyword>();
            //using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            // {
            //dalKeyWords = db.Keywords.Include(k => k.Cases).ToList();
            ////}
            ////List<DTO.Keyword> dtoKeyWords = new List<DTO.Keyword>();
            ////foreach (var w in dalKeyWords)
            ////{
            ////    dtoKeyWords.Add(
            ////        Convertors.KeywordConvertors.ConvertKeywordsToDTO(w)
            ////        );
            ////}
            //////dtokeywords.keyword1;
            //////words
            ////List<DTO.Keyword> keywordsToAdd = new List<DTO.Keyword>();
            ////List<DTO.Cases> relatedCases = new List<DTO.Cases>();
            ////bool flag = false;
            ////foreach (var word in words)
            ////{
            ////    foreach (var keyword in dtoKeyWords)
            ////    {
            ////        if (word.Equals(keyword))
            ////        {
            ////            flag = true;
            ////            foreach (var c in keyword.Cases)
            ////            {
            ////                relatedCases.Add(c);
            ////            }
            ////        }
            ////    }
            ////    if (!flag)
            ////    {
            ////        keywordsToAdd.Add(new DTO.Keyword(word));
            ////    }
            ////}
            ////List<DAL.Keyword> addToDBKeywords = new List<DAL.Keyword>();
            ////foreach (var w in keywordsToAdd)
            ////{
            ////    addToDBKeywords.Add(
            ////        Convertors.KeywordConvertors.ConvertKeywordsToDAL(w)
            ////        );
            ////}


            //todo: extra- to return the corrected list


            //foreach (var searchWord in words)
            //{
            //    foreach (var keyword in dtoKeyWords)
            //    {
            //        if (keyword.keyWord1.Equals(searchWord))
            //        {
            //            foreach (var c in keyword.Cases)
            //            {
            //                relatedCases.Add(c);
            //            }
            //        }
            //    }
            //}
            #endregion
            return relatedCases;
        }
    }
}
