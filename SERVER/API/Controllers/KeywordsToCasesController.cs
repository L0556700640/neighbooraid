using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]

    [RoutePrefix("API/KeywordsToCases")]
    public class KeywordsToCasesController: ApiController
    {
        [Route("saveKeywordsToCase/{selectedCase}")]
        [HttpGet]
        public IHttpActionResult SaveKeywordsToCase()
        {
            return Ok();
        }
    }
}