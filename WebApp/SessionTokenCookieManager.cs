using System;
using System.Web;

namespace WebApp
{
    public class SessionTokenCookieManager
    {
        private const string SessionTokenCookieName = "sessionToken";

        public void SetSessionTokenCookie(HttpCookieCollection cookieCollection, string sessionToken)
        {
            var sessionTokenCookie = new HttpCookie(SessionTokenCookieName)
                {
                    Value = sessionToken
                };
            cookieCollection.Add(sessionTokenCookie);
        }

        public void UnsetSessionTokenCookie(HttpCookieCollection cookieCollection)
        {
            var sessionTokenCookie = new HttpCookie(SessionTokenCookieName)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
            cookieCollection.Add(sessionTokenCookie);
        }

        public string GetSessionToken(HttpRequestBase request)
        {
            var sessionTokenCookie = request.Cookies[SessionTokenCookieName];
            if (sessionTokenCookie == null)
            {
                return null;
            }

            return sessionTokenCookie.Value;
        }

        public bool HasSessionTokenCookie(HttpRequestBase request)
        {
            return !string.IsNullOrEmpty(GetSessionToken(request));
        }
    }
}