using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using CDO.Models;

namespace CDO.Controllers
{
    [Route("api/bug")]
    public class BugController : Controller
    {
        public BugRepository _bugRepository = new BugRepository();

        [HttpGet]
        public List<Bug> GetAll()
        {
            return _bugRepository.GetAll();
        }

        [HttpPost]
        public string Post([FromBody] Bug bug){;
            string title   = bug.Title;
            string severity = bug.Severity;
            //string product = "dasdsad";
            string description = bug.Description;
            string email = bug.Email;
            string status = bug.Status;
            string image  = bug.Image;
            return _bugRepository.InsertOne(title, severity, description, email, status, image);
        }
    }
}