using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CasesToDoctor
    {
        public string doctorId { get; set; }
        public int caseId { get; set; }
        public int satisfaction { get; set; }
    }
}
