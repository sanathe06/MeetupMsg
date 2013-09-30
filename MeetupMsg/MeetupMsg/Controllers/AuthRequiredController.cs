
using System;
using System.Web.Mvc;
using Google.Apis.Mirror.v1;
using Google.Apis.Services;
namespace MeetupMsg.Controllers
    {
    public abstract class AuthRequiredController : Controller
        {
        protected MirrorService Service { get; set; }
        protected String UserId { get; set; }

        /// <summary>
        /// Initializes this Controller with the current user ID and an authorized Mirror service
        /// instance.
        /// </summary>
        /// <returns>Whether or not initialization succeeded.</returns>
        protected Boolean Initialize()
            {
            UserId = Session["userId"] as String;
            if (String.IsNullOrEmpty(UserId))
                {
                return false;
                }

            var state = Utils.GetStoredCredentials(UserId);
            if (state == null)
                {
                return false;
                }

            Service = new MirrorService(new BaseClientService.Initializer()
            {
                Authenticator = Utils.GetAuthenticatorFromState(state)
            });


            return true;
            }
        }
    }