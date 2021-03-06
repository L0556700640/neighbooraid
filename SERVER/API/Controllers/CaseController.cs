﻿using BL;
using DTO;
using Newtonsoft.Json;
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
        public IHttpActionResult CaseChose()
        {
            int helpcallid = JsonConvert.DeserializeObject<int>(HttpContext.Current.Request["helpCallID"]);
            int caseid = JsonConvert.DeserializeObject<int>(HttpContext.Current.Request["choosedCase"]);
            var url = JsonConvert.DeserializeObject<string>(HttpContext.Current.Request["contactsListUrl"]);
            return Ok(CasesBL.CaseChose(helpcallid,caseid,url));
        }
        

        [Route("ConfirmCase/{caseId}/{doctorID}/{satisfaction}")]
        [HttpPost]
        public IHttpActionResult ConfirmCase(int caseId, string doctorID, int satisfaction)
        {
            return Ok(CasesBL.UpdateCaseToDoctor( doctorID, caseId, satisfaction));

        }

    }
}
