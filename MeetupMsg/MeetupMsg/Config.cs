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
        /// exile
        public static readonly string CLIENT_ID = "879201691161.apps.googleusercontent.com";
        //local
        //public static readonly string CLIENT_ID = "283632518816.apps.googleusercontent.com";

        /// <summary>
        /// The OAuth2.0 Client secret of your project.
        /// </summary>
        /// 
        public static readonly string CLIENT_SECRET = "zdYKr8xzbhHcEgjFXZI3U_JT";
        //local
        //public static readonly string CLIENT_SECRET = "xYh_u7K1jzyO7HVm4m_GVhXh";

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
        public static readonly string REDIRECT_URI = "https://glassware.exilesoft.com/oauth2callback";


        //public static readonly string REDIRECT_URI = "http://localhost:62693/oauth2callback";
        //public static readonly string REDIRECT_URI = "https://localhost:443/oauth2callback";
        }

    /// <summary>
    /// Static class holding app wide configuration constants.
    /// </summary>

    internal static class Config
    {
        
    }
    }