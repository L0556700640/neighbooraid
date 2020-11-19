using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;

namespace API.Controllers
{
    [EnableCors("*","*","*")]
    [RoutePrefix("API/Doctor")]
    public class DoctorController : ApiController
    {
        [Route("AddDoctor")]
        [HttpPost]
        public IHttpActionResult AddDoctor()
        {
           HttpPostedFile file = HttpContext.Current.Request.Files[0];
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            DTO.DoctorsDetailsDTO doctor = JsonConvert.DeserializeObject<DTO.DoctorsDetailsDTO>(HttpContext.Current.Request["doctor"]);


            int doctorId= BL.DoctorBL.AddDoctor(doctor,file);
            return Ok(doctorId);
        }
        [Route("ConfirmDoctor/{doctorId}/{isConfirmed}")]
        [HttpPost]
        public IHttpActionResult ConfirmDoctor(int doctorId,bool isConfirmed)//todo להצפין קוד רופא
        {
           BL.DoctorBL.ConfirmDoctor(doctorId, isConfirmed);
            return Ok("הפעולה בוצעה בהצלחה");
        }
    }
}
