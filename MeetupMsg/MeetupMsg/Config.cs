using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetupMsg
    {
    /// <summary>
    /// Static class holding OAuth 2.0 configuration constants.
    /// </summary>
    internal static class OAuthConfig
        {
        /// <summary>
        /// The OAuth2.0 Client ID of your project.
        /// </summary>
        /// 
        public static readonly string CLIENT_ID = "<your client id>";
      

        /// <summary>
        /// The OAuth2.0 Client secret of your project.
        /// </summary>
        /// 
        public static readonly string CLIENT_SECRET = "<your client secret>";
       
        /// <summary>
        /// The OAuth2.0 scopes required by your project.
        /// </summary>
        public static readonly string[] SCOPES = new String[]
        {
            "https://www.googleapis.com/auth/glass.timeline",
            "https://www.googleapis.com/auth/glass.location",
            "https://www.googleapis.com/auth/userinfo.profile"
        };

        /// <summary>
        /// The Redirect URI of your project.
        /// </summary>
        public static readonly string REDIRECT_URI = "<your redirect url>";

        }

    /// <summary>
    /// Static class holding app wide configuration constants.
    /// </summary>

    internal static class Config
    {
        
    }
    }