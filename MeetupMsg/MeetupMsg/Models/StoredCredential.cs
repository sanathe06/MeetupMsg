using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeetupMsg.Models
    {
    public class StoredCredential
        {
        [Key]
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? TokenIssuedTime { get; set; }
        public DateTime? TokenExpirationTime { get; set; }
        }
    }