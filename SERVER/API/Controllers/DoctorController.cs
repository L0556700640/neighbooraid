using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors("*","*","*")]
    [RoutePrefix("API/Doctor")]
    public class DoctorController : ApiController
    {
        [Route("AddDoctor")]
        [HttpPost]
        public IHttpActionResult AddDoctor(DTO.DoctorsDetailsDTO doctor)
        {
            int doctorId= BL.DoctorBL.AddDoctor(doctor);
            return Ok(doctorId);
        }
    }
}
