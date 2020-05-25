using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.Jwt.AccessToken;

namespace Video.Conferencing.Controllers
{
    [Route("token")]
    [ApiController]
    public class TwilioController : ControllerBase
    {
        private readonly AppSettings appSettings;

        public TwilioController(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        [HttpGet]
        public ActionResult Get(string identity, string roomName)
        {
            // Create an Chat grant for this token
            var grant = new VideoGrant
            {
                Room = roomName
            };
            var grants = new HashSet<IGrant> { { grant } };

            // Create an Access Token generator
            var token = new Token(
               appSettings.TwilioAccountSid,
               appSettings.TwilioApiKeySid,
               appSettings.TwilioApiKeySecret,
                identity,
                grants: grants);

            return Ok(token.ToJwt());
        }
    }
}
