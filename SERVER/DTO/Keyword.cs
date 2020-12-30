using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Keyword
    {
        private static int keywordsCounter = 0;
        public int keywordId { get; set; }
        public string keyWord1 { get; set; }

        public Keyword(string keyWord1)
        {
            this.keywordId = keywordsCounter++;
            this.keyWord1 = keyWord1;
        }

        public Keyword()
        {
            keywordsCounter++;
        }

        public Keyword(int keywordId, string keyWord1)
        {
            this.keywordId = keywordId;
            this.keyWord1 = keyWord1;
            keywordsCounter++;
        }
    }
}
