using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Persistence.Models
{
    public class ViewModel
    {
        public int Id { get; set; }
        [JsonPropertyName("UserModelId")]
        public int UserId { get; set; }
        public string Controller { get; set; }
        public string View { get; set; }
        public UserModel User { get; set; }
    }
}
