using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Mirror.v1;
using Google.Apis.Mirror.v1.Data;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;

namespace MeetupMsg.Controllers
    {
    public class AuthController:AuthRequiredController
        {
        private static readonly String OAUTH2_REVOKE_ENDPOINT =
           "https://accounts.google.com/o/oauth2/revoke";

        // GET: /auth

        public ActionResult StartAuth()
            {
            UriBuilder builder =
                new UriBuilder(GetProvider().RequestUserAuthorization(GetAuthorizationState()));
            NameValueCollection queryParameters = HttpUtility.ParseQueryString(builder.Query);

            queryParameters.Set("access_type", "offline");
            queryParameters.Set("approval_prompt", "force");

            builder.Query = queryParameters.ToString();

            return Redirect(builder.Uri.ToString());
            }


        /// <summary>
        /// Returns an OAuth 2.0 provider for the authorization flow.
        /// </summary>
        /// <returns>OAuth 2.0 provider.</returns>
        private NativeApplicationClient GetProvider()
            {
            var provider =
                new NativeApplicationClient(
                    GoogleAuthenticationServer.Description, OAuthConfig.CLIENT_ID,
                    OAuthConfig.CLIENT_SECRET);
            return provider;
            }

        /// <summary>
        /// Returns a pre-filled OAuth 2.0 state for the authorization flow.
        /// </summary>
        /// <returns>OAuth 2.0 state.</returns>
        private IAuthorizationState GetAuthorizationState()
            {
            var authorizationState = new AuthorizationState(OAuthConfig.SCOPES);
            authorizationState.Callback = new Uri(OAuthConfig.REDIRECT_URI);
            return authorizationState;
            }

        //
        // GET: /oauth2callback

        public ActionResult OAuth2Callback(String code)
            {
            var provider = GetProvider();
            try
                {
                var state = provider.ProcessUserAuthorization(code, GetAuthorizationState());
                var initializer = new BaseClientService.Initializer()
                {
                    Authenticator = Utils.GetAuthenticatorFromState(state)
                };

                Oauth2Service userService = new Oauth2Service(initializer);
                String userId = userService.Userinfo.Get().Fetch().Id;

                Utils.StoreCredentials(userId, state);
                Session["userId"] = userId;

                //BootstrapUser(new MirrorService(initializer), userId);
                }
            catch (ProtocolException)
                {
                // TODO(alainv): handle error.
                }
            return Redirect(Url.Action("Index", "Meetup"));
            }

        /// <summary>
        /// Bootstrap the user with a welcome message; if not running locally, this method also
        /// adds a sharing contact and subscribes to timeline notifications.
        /// </summary>
        /// <param name="mirrorService">Authorized Mirror service object</param>
        /// <param name="userId">ID of the user being bootstrapped.</param>
        private void BootstrapUser(MirrorService mirrorService, string userId)
            {
            if(Request.IsSecureConnection && !Request.IsLocal)
            {
                //create a Subscription
                Subscription subscription = new Subscription()
                                                {
                                                    Collection = "timeline",
                                                    UserToken = userId,
                                                    CallbackUrl = Url.Action("notify",null,null,Request.Url.Scheme)
                                                };
                Log.D("Insert Subscription");
                //insert a Subscription
                mirrorService.Subscriptions.Insert(subscription).Fetch();


             
                UriBuilder builder=new UriBuilder(Request.Url);
                builder.Path = Url.Content("~/Content/images/meetuplogo.png");
                //create a sharing contact
                Contact contact = new Contact()
                                      {
                                          Id = "MEETUP_MSG",
                                          DisplayName = "Meetup Updates",
                                          ImageUrls = new List<String> {builder.ToString()}
                                      };

                //insert sharing contact
                mirrorService.Contacts.Insert(contact).Fetch();
                Log.D("insert sharing contact");
            }else
            {
                Console.WriteLine("");
            }

            //create timeline item
            TimelineItem item = new TimelineItem()
                                    {
                                        Text = "Welcome to the Colombo Agile Meetup",
                                        Notification = new NotificationConfig()
                                                           {
                                                               Level = "DEFAULT"
                                                           }
                                    };
            //insert time line item
            mirrorService.Timeline.Insert(item);
            Log.D("insert time line item");

            }
        }
    }