using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Keyword
    {
        public int keywordId { get; set; }
        public string keyWord1 { get; set; }
        public List<DTO.Cases> Cases { get; set; } = new List<Cases>();

    }
}
