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
        public static List<DTO.Cases> getTheCasesRelatedByTheSearch(string sentence)
        {
            List<string> words =new List<string>();
            words=sentence.Split(' ').ToList();
            //todo: למחוק את מילות הקישור מהמערך
            bool remove = true;
            List<string> linkWords = new List<string>
            {
                "את",
                "כל",
                "על",
                "בתוך",
                "מעל",
                "כואב",
                "לי",
                "לו",
                "להם",
                "להן",
                "אני",
            };
            foreach (var w in linkWords)
            {
                while (remove != false)
                {
                    remove = words.Remove(w);
                }
                remove = true;
            }
            //get the keywords table from db.
            List<DAL.Keyword> dalKeyWords = new List<Keyword>();
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
            //
            List<DTO.Cases> relatedCases = new List<DTO.Cases>();
            foreach (var searchWord in words)
            {
                foreach (var keyword in dtoKeyWords)
                {
                    if (keyword.keyWord1.Equals(searchWord))
                    {
                        foreach (var c in keyword.Cases)
                        {
                            relatedCases.Add(c);
                        }
                    }
                }
            }
                return relatedCases;//todo: return the correct list
        }
    }
}
