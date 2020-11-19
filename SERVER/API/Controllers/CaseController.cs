using BL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
           string s=  BL.TranslateBL.Translate("שלום עולם").GetAwaiter().GetResult();
            return Ok(CasesBL.getAllCases());
        }



        [Route("GetRelatedCases/{searchSentence}")]
        [HttpGet]
        public IHttpActionResult GetRelatedCases(string searchSentence)
         {
            return Ok(CasesBL.getTheCasesRelatedByTheSearch(searchSentence));
        }
    }
}
