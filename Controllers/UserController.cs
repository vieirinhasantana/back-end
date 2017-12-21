using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CDO.Models;

namespace CDO.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        public UserRepository _userRepository = new UserRepository();

        [HttpGet]
        public String GetAll()
        {
            return "sadassddsa";
        }
    }
}