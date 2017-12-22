using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

using BCrypt.Net;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;

using CDO.Models;

namespace CDO.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        public UserRepository _userRepository = new UserRepository();
        public Token          _token          = new Token();
        public Error          _error          = new Error();

        [HttpPost]
        public JsonResult Auth()
        {
            string authorization = Request.Headers["Authorization"];
            string authorizationDecode = this.DecryptAuthorizationBase64(authorization);
            string[] credentials = authorizationDecode.Split(':');
            var result = this.ValidateExistenceOfUserCredentials(credentials[0], credentials[1]);
            var decondeJson = JsonConvert.DeserializeObject(result);
            return Json(decondeJson);
        }

        private string DecryptAuthorizationBase64(string authorization)
        {
            if (authorization != null && authorization.StartsWith("Basic")){
                authorization = authorization.Replace("Basic ", "");
                byte[] data = Convert.FromBase64String(authorization);
                string decodeAuthorization = Encoding.UTF8.GetString(data);
                return decodeAuthorization;
            } else {
                _error.StatusError = 500;
                _error.Description = "Internal Error!";
                return JsonConvert.SerializeObject(_error);
            }
        }

        private string ValidateExistenceOfUserCredentials(string email, string password)
        {
            var users = _userRepository.GetUser(email);
            if(users.Count != 0 ) {
                string emailData    = users[0].Email;
                string passwordData = users[0].Password;
                if (BCrypt.Net.BCrypt.Verify(password, passwordData)) {
                    _token.Email = email;
                    _token.TokenAccess = this.GenerateNewTokenForUser();
                    return JsonConvert.SerializeObject(_token);
                }
            }
            _error.StatusError = 500;
            _error.Description = "Internal Error!";
            return JsonConvert.SerializeObject(_error);
        }

        private string GenerateNewTokenForUser()
        {
            const string secret = "85#m8G[<C*M?cwNyn8Q'$Ie/@`=qF5tOh}-mBdRF*~Y9kJ6@BITbK>S}8 LO,!/Q";
            var payload = new Dictionary<string, object>
            {
                { "claim1", 0 },
                { "claim2", "claim2-value" }
            };
            JWT.IJwtAlgorithm      algorithm = new HMACSHA256Algorithm();
            JWT.IJsonSerializer   serializer = new JsonNetSerializer();
            JWT.IBase64UrlEncoder urlEncoder = new JWT.JwtBase64UrlEncoder();
            JWT.IJwtEncoder          encoder = new JWT.JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            return token;
        }
    }
}