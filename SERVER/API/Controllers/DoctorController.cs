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
    [EnableCors("*", "*", "*")]
    [RoutePrefix("API/Doctor")]
    public class DoctorController : ApiController
    {
        [Route("AddDoctor")]
        [HttpPost]
        public IHttpActionResult AddDoctor()
        {
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            var c = HttpContext.Current.Request["doctor"];
            DTO.DoctorsDetailsDTO doctor = JsonConvert.DeserializeObject<DTO.DoctorsDetailsDTO>(HttpContext.Current.Request["doctor"]);


            string doctorId = BL.DoctorBL.AddDoctor(doctor, file);
            return Ok(doctorId);
        }

        [Route("ConfirmDoctor/{doctorId}/{isConfirmed}")]
        [HttpGet]
        public IHttpActionResult ConfirmDoctor(int doctorId, bool isConfirmed)
        {
            //todo להצפין קוד רופא
            BL.DoctorBL.ConfirmDoctor(doctorId, isConfirmed);
            return Ok("הפעולה בוצעה בהצלחה");
        }

        [Route("CheckUser")]
        [HttpPost]
        public IHttpActionResult CheckDoctor()
        {
            //todo להצפין קוד רופא
            string doctorName = JsonConvert.DeserializeObject<string>(HttpContext.Current.Request["firstName"]);
            string doctorId = JsonConvert.DeserializeObject<string>(HttpContext.Current.Request["id"]);

            //BL.DoctorBL.CheckUser(doctorName, doctorId);
            return Ok(BL.DoctorBL.CheckUser(doctorName, doctorId));
        }

        [Route("User/{doctorId}")]
        [HttpGet]
        public IHttpActionResult User(string doctorId)//todo להצפין קוד רופא
        {


            // BL.DoctorBL.CheckUser(doctorName, doctorId);
            return Ok(BL.DoctorBL.User(doctorId));
        }

        [Route("casesToDoctor/{doctorId}")]
        [HttpGet]
        public IHttpActionResult casesToDoctor(string doctorId)
        {
            return Ok(BL.DoctorBL.casesToDoctor(doctorId));
        }
        [Route("DeleteDoctor")]
        [HttpPost]
        public IHttpActionResult DeleteDoctor()
        {

            string doctorId = JsonConvert.DeserializeObject<string>(HttpContext.Current.Request["id"]);

            //BL.DoctorBL.CheckUser(doctorName, doctorId);
            return Ok(BL.DoctorBL.DeleteDoctor(doctorId));
        }


        [Route("getDoctorToCases/{selectedCase}")]
        [HttpGet]
        public IHttpActionResult GetDoctorToCases(DTO.Cases selectedCase)
        {
            //todo להצפין קוד רופא
            return Ok(BL.DoctorBL.GetDoctorsToCase(selectedCase));
        }

        [Route("DoctorToCases/{selectedCase}")]
        [HttpGet]
        public IHttpActionResult DoctorToCases(DTO.Cases selectedCase)
        {
            //todo להצפין קוד רופא
            return Ok(BL.DoctorBL.DoctorsToCase(selectedCase));
        }

        [Route("AddHelpCall")]
        [HttpPost]
        public IHttpActionResult AddHelpCall()
        {
            //HttpPostedFile file = HttpContext.Current.Request.Files[0];
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            // var c = HttpContext.Current.Request["helpcall"];
            DTO.HelpCall helpcall = JsonConvert.DeserializeObject<DTO.HelpCall>(HttpContext.Current.Request["helpcall"]);
            return Ok(BL.HelpCallBL.SaveHelpCallInDB(helpcall));
        }


    }

}