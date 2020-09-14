using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Contacts
    {
        public int contactId { get; set; }
        public int patientId { get; set; }
        public string contactPhone { get; set; }
        public string contactName { get; set; }
    }
}
