using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Doctor
    {
        public string doctorId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string doctorPhone { get; set; }
        public string address { get; set; }
        public bool isConfirmed { get; set; }
        public string mail { get; set; }
        public string certificateNumber { get; set; }
        public DateTime certificateValidity { get; set; }
    }
}
