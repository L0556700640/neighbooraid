using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Convertors
{
    public class KeywordConvertors
    {
        public static DTO.Keyword ConvertKeywordsToDTO(DAL.Keyword keyword)
        {
            return new DTO.Keyword
            {
                keyWord1 = keyword.keyWord1,
                keywordId=keyword.keywordId
            };
        }
        public static DAL.Keyword ConvertKeywordsToDAL(DTO.Keyword keyword)
        {
            return new DAL.Keyword
            {
                keyWord1 = keyword.keyWord1,
                keywordId = keyword.keywordId,
            };
        }
    }
}
//Cases = keyword.Cases.ToList().Select(c => Convertors.CaseConvertor.ConvertCaseToDAL(c)).ToList()

