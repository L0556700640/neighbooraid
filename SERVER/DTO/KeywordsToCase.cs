using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KeywordsToCase
    {
        public int caseId { get; set; }
        public int keywordId { get; set; }
        public int numOfUsingThisRelation { get; set; }

        public KeywordsToCase(int caseId, int keywordId)
        {
            this.caseId = caseId;
            this.keywordId = keywordId;
            this.numOfUsingThisRelation = 1;
        }

        public KeywordsToCase()
        {
        }

        public KeywordsToCase(int caseId, int keywordId, int numOfUsingThisRelation) : this(caseId, keywordId)
        {
            this.numOfUsingThisRelation = numOfUsingThisRelation;
        }
    }
}
