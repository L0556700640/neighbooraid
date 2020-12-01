using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HelpCall
    {
        public int callId { get; set; }
        public int caseId { get; set; }
        public string doctorId { get; set; }
        public double addressX { get; set; }
        public double addressY { get; set; }
        public System.DateTime date { get; set; }


    }
}
