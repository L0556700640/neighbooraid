using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
