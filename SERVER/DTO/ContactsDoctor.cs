using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ContactsDoctor
    {

        public DTO.Doctor Doctor { get; set; }
        public string Name { get; set; }
        public int Satisfaction { get; set; }


        public ContactsDoctor(Doctor doctor, string name, int satisfaction)
        {
            Doctor = doctor;
            Name = name;
            Satisfaction = satisfaction;
        }

        public ContactsDoctor()
        {
        }
    }
}
