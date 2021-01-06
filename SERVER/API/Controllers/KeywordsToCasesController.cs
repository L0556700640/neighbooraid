using DTO;
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
    public class KeywordsToCasesController : ApiController
    {
        //todo: לשנות בכחול שהפונקציה הזאת תשלח 2 משתנים ולא 1.
        //יש גם את המזהה של הפציעה שנוסף. איפפה הוא נשמר אצל הלקוח?- לבדוק
        [Route("saveKeywordsToCase/{helpCallId},{selectedCase}")]
        [HttpGet]
        public IHttpActionResult SaveKeywordsToCase(int helpCallId, DTO.Cases selectedCase)
        {
            return Ok(BL.KeywordBL.AddCaseToKeywords(helpCallId, selectedCase));
        }

        private object AddCaseToKeywords(Cases selectedCase)
        {
            throw new NotImplementedException();
        }
    }
}