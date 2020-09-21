using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Doctor
    {
        public int doctorId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string doctorPhone { get; set; }
        public string address { get; set; }
        public byte[] pictureDiploma { get; set; }
        public bool isConfirmed { get; set; }
    }
}
