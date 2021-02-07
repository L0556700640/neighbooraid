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
    public class CasesBL
    {
        public static bool AddCase(DTO.Cases newCase)
        {
            //todo: fill the function
            return true;
        }
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static string getCaseName(string id)
        {

            using (neighboorAidDBEntities db = new neighboorAidDBEntities())
            {
                var case1=(from c in db.Cases
                               where c.caseId.Equals(id)
                               select c).ToList();
                return case1.Select(c => Convertors.CaseConvertor.ConvertCaseToDTO(c).caseName).FirstOrDefault();
                // db.SaveChanges();
            }

        }
        public static List<DTO.Cases> GetTheCasesRelatedByTheSearch(int helpCallID, string sentence)
        {
            string translateSentence = TranslateBL.Translate(sentence);
            List<string> importantWords = new List<string>();
            importantWords = TranslateBL.Analysis(translateSentence);

            List<DTO.Cases> relatedCases = new List<DTO.Cases>();
            relatedCases = KeywordBL.GetRelatedCasesByKeywords(helpCallID, importantWords);

            return relatedCases;
        }

        public static ReturnedDoctorsToCase CaseChose(int helpCallID, Cases choosedCase)
        {
            try
            {
                KeywordBL.SaveTheCorrentCaseToKeywords(helpCallID, choosedCase);
                return DoctorBL.GetDoctorsToCase(helpCallID,choosedCase);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        private static bool SaveSearchKeywordsToTheChoosedCase(int helpCallID, Cases choosedCase)
        {
            try
            {

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

    }
}
