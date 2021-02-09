using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public Contact()
        {

        }

        public Contact(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }
}
