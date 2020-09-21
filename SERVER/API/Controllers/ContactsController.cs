using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("API/Contacts")]
    public class ContactsController : ApiController
    {
        [Route("getPhoneNumbers")]
        public List<string> getPhoneNumbers()
        {
            return new List<string> { "0556700640", "0504173502" };
        }
        [Route("getContactsNames")]
        public List<string> getContactsNames()
        {
            return new List<string> { "Liraz", "Peretz" };
        }
    }
}
