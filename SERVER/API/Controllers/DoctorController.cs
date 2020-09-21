using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("API/Cases")]
    public class DoctorController : ApiController
    {
        [Route("AddDoctor")]
        [HttpPost]
        public IHttpActionResult AddDoctor(DTO.Doctor doctor)
        {
            bool successAddDoctor= BL.DoctorBL.AddDoctor(doctor);
            return Ok(successAddDoctor);
        }
    }
}
