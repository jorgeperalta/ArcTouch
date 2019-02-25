using System;
using Newtonsoft.Json;

namespace ArcTouch.Core.Models
{
    public class Session
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
        [JsonProperty(PropertyName = "session_id")]
        public string SessionId { get; set; }
    }
}
