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
            

            string doctorId = BL.DoctorBL.AddDoctor(doctor,file);
            return Ok(doctorId);
        }
        [Route("ConfirmDoctor/{doctorId}/{isConfirmed}")]
        [HttpPost]
        public IHttpActionResult ConfirmDoctor(int doctorId,bool isConfirmed)//todo להצפין קוד רופא
        {
           BL.DoctorBL.ConfirmDoctor(doctorId, isConfirmed);
            return Ok("הפעולה בוצעה בהצלחה");
        }

        [Route("CheckUser")]
        [HttpPost]
        public IHttpActionResult CheckDoctor()//todo להצפין קוד רופא
        {
            string doctorName = JsonConvert.DeserializeObject<string>(HttpContext.Current.Request["firstName"]);
            string doctorId= JsonConvert.DeserializeObject<string>(HttpContext.Current.Request["id"]);

            //BL.DoctorBL.CheckUser(doctorName, doctorId);
            return Ok(BL.DoctorBL.CheckUser(doctorName, doctorId));
        }

        [Route("User/{doctorId}")]
        [HttpGet]
        public IHttpActionResult User(string doctorId)//todo להצפין קוד רופא
        {


           // BL.DoctorBL.CheckUser(doctorName, doctorId);
            return  Ok(BL.DoctorBL.User(doctorId));
        }

        [Route("DeleteDoctor")]
        [HttpPost]
        public IHttpActionResult DeleteDoctor()//todo להצפין קוד רופא
        {

            string doctorId = JsonConvert.DeserializeObject<string>(HttpContext.Current.Request["id"]);

            //BL.DoctorBL.CheckUser(doctorName, doctorId);
            return Ok(BL.DoctorBL.DeleteDoctor(doctorId));
        }
    }
}
