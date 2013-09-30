using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using MeetupMsg.Models;

namespace MeetupMsg
    {
        public class Utils
        {
        /// <summary>
        /// Retrieve an IAuthenticator instance using the provided state.
        /// </summary>
        /// <param name="credentials">OAuth 2.0 credentials to use.</param>
        /// <returns>Authenticator using the provided OAuth 2.0 credentials</returns>
        public static IAuthenticator GetAuthenticatorFromState(IAuthorizationState credentials)
            {
            var provider =
                new StoredStateClient(
                    GoogleAuthenticationServer.Description, OAuthConfig.CLIENT_ID, OAuthConfig.CLIENT_SECRET,
                    credentials);
            var auth =
                new OAuth2Authenticator<StoredStateClient>(provider, StoredStateClient.GetState);
            auth.LoadAccessToken();
            return auth;
            }


        /// <summary>
        /// Extends the NativeApplicationClient class to allow setting of a custom
        /// IAuthorizationState.
        /// </summary>
        public class StoredStateClient : NativeApplicationClient
            {
            /// <summary>
            /// Initializes a new instance of the <see cref="StoredStateClient"/> class.
            /// </summary>
            /// <param name="authorizationServer">The token issuer.</param>
            /// <param name="clientIdentifier">The client identifier.</param>
            /// <param name="clientSecret">The client secret.</param>
            public StoredStateClient(AuthorizationServerDescription authorizationServer,
                String clientIdentifier,
                String clientSecret,
                IAuthorizationState state)
                : base(authorizationServer, clientIdentifier, clientSecret)
                {
                this.State = state;
                }

            public IAuthorizationState State { get; private set; }

            /// <summary>
            /// Returns the IAuthorizationState stored in the StoredStateClient instance.
            /// </summary>
            /// <param name="provider">OAuth2 client.</param>
            /// <returns>The stored authorization state.</returns>
            static public IAuthorizationState GetState(StoredStateClient provider)
                {
                provider.RefreshToken(provider.State);
                return provider.State;
                }
            }


            /// <summary>
            /// Retrieved stored credentials for the provided user ID.
            /// </summary>
            /// <param name="userId">User's ID.</param>
            /// <returns>Stored GoogleAccessProtectedResource if found, null otherwise.</returns>
            public static IAuthorizationState GetStoredCredentials(string userId)
            {
            Context db = new Context();
            StoredCredential sc = db.StoredCredentials.FirstOrDefault(x => x.UserId == userId);
            if (sc != null)
                {
                return new AuthorizationState()
                {
                    AccessToken = sc.AccessToken,
                    RefreshToken = sc.RefreshToken,
                    AccessTokenExpirationUtc = sc.TokenExpirationTime,
                    AccessTokenIssueDateUtc = sc.TokenIssuedTime
                };
                }
            return null;
            }

            /// <summary>
            /// Store OAuth 2.0 credentials in the application's database.
            /// </summary>
            /// <param name="userId">User's ID.</param>
            /// <param name="credentials">The OAuth 2.0 credentials to store.</param>
            public static void StoreCredentials(String userId, IAuthorizationState credentials)
                {
                Context db = new Context();
                StoredCredential sc = db.StoredCredentials.FirstOrDefault(x => x.UserId == userId);
                if (sc != null)
                    {
                    sc.AccessToken = credentials.AccessToken;
                    sc.RefreshToken = credentials.RefreshToken;
                    sc.TokenIssuedTime = credentials.AccessTokenIssueDateUtc;
                    sc.TokenExpirationTime = credentials.AccessTokenExpirationUtc;
                    }
                else
                    {
                    db.StoredCredentials.Add(new StoredCredential
                    {
                        UserId = userId,
                        AccessToken = credentials.AccessToken,
                        RefreshToken = credentials.RefreshToken,
                        TokenIssuedTime = credentials.AccessTokenIssueDateUtc,
                        TokenExpirationTime = credentials.AccessTokenExpirationUtc
                    });
                    }
                db.SaveChanges();
                }
        }
    }