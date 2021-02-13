using BL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]

    [RoutePrefix("API/Cases")]
    public class CaseController : ApiController
    {
        [Route("GetAllCases")]
        [HttpGet]
        public IHttpActionResult GetAllCases()
        {
            //string path = HttpContext.Current.Server.MapPath("card.json");
            //string newPath = "";
            //var ss = path.Split('\\');
            //for (int i = 0; i <ss.Length-3; i++)
            //{
            //    newPath += ss[i] + "\\";
            //}
            // string s=  BL.TranslateBL.Translate("שלום עולם");
            return Ok(CasesBL.getAllCases());
        }
        [Route("getCaseName/{caseId}")]
        [HttpGet]
        public IHttpActionResult getCaseName(string caseId)
        {
            return Ok(CasesBL.getCaseName(caseId));
        }

        [Route("GetRelatedCases/{helpCallID}/{searchSentence}")]
        [HttpGet]
        public IHttpActionResult GetRelatedCases(int helpCallID, string searchSentence)
        {
            return Ok(CasesBL.GetTheCasesRelatedByTheSearch(helpCallID,searchSentence));
        }

        [Route("CaseChose")]
        [HttpPost]
        //todo: get the information from client dto object in the c#
        public IHttpActionResult CaseChose([FromBody]int helpCallID, [FromBody] int choosedCase, [FromBody] string contactsListUrl)
        {
            return Ok(CasesBL.CaseChose(helpCallID, choosedCase, "https://www.google.com/m8/feeds/contacts/default/thin?alt=json_access_token=ya29.A0AfH6SMAp22-TJXYMU2t2b5kn6r3Tpf9aVPQJcpv0BQDcNpMtyIhFFQplo_MeZ59oDtFkVzY2BfhyXS-0LwyzqDTed8OeVi3vh0s-OOGmDr_3kOB2bZOYU23xdFRxhVRvLjASNm_028EFfkj2FM1A353PNFZHIw&max-results=500&v=3.0"));

        }
        [Route("UpdateCasesToDoctor/{doctorID}/{newCasesList}")]
        [HttpPost]
        public IHttpActionResult UpdateListOfCasesToDoctor(string doctorID, List<DTO.Cases> newCasesList)
        {
            //todo: get the file, save and add to the mail.
            return Ok(CasesBL.UpdateCasesToDoctorBL(doctorID, newCasesList));

        }

        [Route("ConfirmCase/{caseId}/{doctorID}/{satisfaction}")]
        [HttpPost]
        public IHttpActionResult ConfirmCase(int caseId, string doctorID, int satisfaction)
        {
            return Ok(CasesBL.UpdateCaseToDoctor( doctorID, caseId, satisfaction));

        }

    }
}
