using System;
using Newtonsoft.Json;

namespace ArcTouch.Core.Models
{
    public class SessionRequest
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "request_token")]
        public string RequestToken { get; set; }
    }
}
