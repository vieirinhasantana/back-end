using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using CDO.Models;

namespace CDO.Controllers
{

    public class BugController : Controller
    {
        public BugRepository _bugRepository = new BugRepository();

        [Route("api/bug")]
        [HttpGet]
        public List<Bug> GetAll()
        {
            return _bugRepository.GetAll();
        }

        [Route("api/bug/one")]
        [HttpGet]
        public List<Bug> GetOne()
        {
            string objectId = Request.Headers["idBug"];
            return _bugRepository.GetOne(objectId);
        }

        [Route("api/bug")]
        [HttpPost]
        public string Post([FromBody] Bug bug)
        {
            string title   = bug.Title;
            string severity = bug.Severity;
            //string product = bug.Product;
            string description = bug.Description;
            string email = bug.Email;
            string status = bug.Status;
            string image  = bug.Image;
            return _bugRepository.InsertOne(title, severity, description, email, status, image);
        }

        [Route("api/bug")]
        [HttpPut]
        public string Put()
        {
            string objectId = Request.Headers["idBug"];
            return _bugRepository.Update(objectId);
        }
    }
}