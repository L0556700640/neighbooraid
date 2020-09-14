using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Convertors
{
    public static class ContactsConvertor
    {
        public static DTO.Contacts ConvertContactsToDTO(DAL.Contact contacts)
        {
            return new DTO.Contacts
            {
                contactId = contacts.contactId,
                contactName = contacts.contactName,
                contactPhone = contacts.contactPhone,
                patientId = contacts.patientId
            };
        }
        public static DAL.Contact ConvertContactsToDAL(DTO.Contacts contacts)
        {
            return new DAL.Contact
            {
                contactId = contacts.contactId,
                contactName = contacts.contactName,
                contactPhone = contacts.contactPhone,
                patientId = contacts.patientId
            };
        }
    }
}
