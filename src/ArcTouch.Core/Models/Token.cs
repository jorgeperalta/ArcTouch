using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ArcTouch.Core.Models
{
    public class Token
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
        [JsonProperty(PropertyName = "expires_at")]
        public string ExpiresAt { get; set; }
        [JsonProperty(PropertyName = "request_token")]
        public string RequestToken { get; set; }
    }
}
