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
        private const string secret = "85#m8G[<C*M?cwNyn8Q'$Ie/@`=qF5tOh}-mBdRF*~Y9kJ6@BITbK>S}8 LO,!/Q";
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
                _error.Description = "Internal Error11!";
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
                    _token.TokenAccess = this.GenerateNewTokenForUser(email);
                    return JsonConvert.SerializeObject(_token);
                }
            }
            _error.StatusError = 500;
            _error.Description = "Internal Error22!";
            return JsonConvert.SerializeObject(_error);
        }

        private string GenerateNewTokenForUser(string email)
        {
            var payload = new Dictionary<string, object>
            {
                { "email", email }
            };
            JWT.IJwtAlgorithm      algorithm = new HMACSHA256Algorithm();
            JWT.IJsonSerializer   serializer = new JsonNetSerializer();
            JWT.IBase64UrlEncoder urlEncoder = new JWT.JwtBase64UrlEncoder();
            JWT.IJwtEncoder          encoder = new JWT.JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            return token;
        }
        [HttpGet]
        public string verifyUserToken ()
        {
            string token = Request.Headers["token"];
            try
            {
                JWT.IJsonSerializer serializer = new JsonNetSerializer();
                JWT.IDateTimeProvider provider = new JWT.UtcDateTimeProvider();
                JWT.IJwtValidator validator = new JWT.JwtValidator(serializer, provider);
                JWT.IBase64UrlEncoder urlEncoder = new JWT.JwtBase64UrlEncoder();
                JWT.IJwtDecoder decoder = new JWT.JwtDecoder(serializer, validator, urlEncoder);
                
                var json = decoder.Decode(token, secret, verify: true);
                return "ok";
            }
            catch (JWT.TokenExpiredException)
            {
                return "Token has expired";
            }
            catch (JWT.SignatureVerificationException)
            {
                return "Token has invalid signature";
            }
        }
    }
}