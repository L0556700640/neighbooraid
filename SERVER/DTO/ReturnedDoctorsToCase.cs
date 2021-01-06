using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ReturnedDoctorsToCase
    {
        public List<DTO.ContactsDoctor> contactsDoctor { get; set; } = new List<ContactsDoctor>();
        public List<DTO.CloseDoctor> closeDoctor { get; set; } = new List<CloseDoctor>();
        public ReturnedDoctorsToCase()
        {

        }

        public ReturnedDoctorsToCase(List<ContactsDoctor> contactsDoctor, List<CloseDoctor> closeDoctor)
        {
            this.contactsDoctor = contactsDoctor;
            this.closeDoctor = closeDoctor;
        }
    }
}
