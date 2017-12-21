using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CDO.Models;

namespace CDO.Controllers
{
    [Route("api/bug")]
    public class BugController : Controller
    {
        public BugRepository _bugRepository = new BugRepository();

        [HttpGet]
        public List<Bug> GetAll()//IEnumerable<Bug> GetList()
        {
            return _bugRepository.GetAll();
        }
    }
}