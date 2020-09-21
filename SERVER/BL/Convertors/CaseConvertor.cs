using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Convertors
{
    public static class CaseConvertor
    {
        public static DAL.Case ConvertCaseToDAL(DTO.Cases myCase)
        {
            var c = new DAL.Case();
            c.caseId = myCase.caseId;
            c.caseName = myCase.caseName;
            return c;
        }

        public static DTO.Cases ConvertCaseToDTO(DAL.Case myCase)
        {
            return new DTO.Cases { caseId = myCase.caseId, caseName = myCase.caseName };
        }
    }
}
