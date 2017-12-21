using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Security;
using Microsoft.AspNetCore.Mvc;

using CDO.Models;

namespace CDO.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        public UserRepository _userRepository = new UserRepository();

        [HttpPost]
        public string Auth()
        {
            string authorization = Request.Headers["Authorization"];
            return this.Decrypt(authorization);
        }

        private string Decrypt(string authorization)
        {
            if (authorization != null && authorization.StartsWith("Basic")){
                authorization = authorization.Replace("Basic ", "");
                byte[] data = Convert.FromBase64String(authorization);
                string decodeAuthorization = Encoding.UTF8.GetString(data);
                string[] exploded = decodeAuthorization.Split(':');
                return exploded[0];
            } else {
                return "Error";
            }
        }
    }
}