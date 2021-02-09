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

        public CasesToDoctor(string doctorId, int caseId, int satisfaction)
        {
            this.doctorId = doctorId;
            this.caseId = caseId;
            this.satisfaction = satisfaction;
        }

        public CasesToDoctor()
        {
        }
    }
}
