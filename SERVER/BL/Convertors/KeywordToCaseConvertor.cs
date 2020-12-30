using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Convertors
{
    public class KeywordToCaseConvertor
    {
        public static DAL.KeywordsToCase ConvertKeywordsToCaseToDAL(DTO.KeywordsToCase keyword)
        {
            return new DAL.KeywordsToCase
            {
                caseId = keyword.caseId,
                keywordId = keyword.keywordId,
                numOfUsingThisRelation = keyword.numOfUsingThisRelation
            };
        }

        public static DTO.KeywordsToCase ConvertKeywordsToCaseToDTO(DAL.KeywordsToCase keyword)
        {
            return new DTO.KeywordsToCase
            {
                caseId = keyword.caseId,
                keywordId = keyword.keywordId,
                numOfUsingThisRelation = keyword.numOfUsingThisRelation
            };
        }
    }
}
