using CLN.Application.Models;
using Microsoft.AspNetCore.Http;

namespace CLN.Application.Helpers
{
    public static class SessionManager
    {
        private static long _loggedInUserId;
        public static long LoggedInUserId { set { _loggedInUserId = value; } get { return _loggedInUserId; } }

        static SessionManager()
        {
            UsersLoginSessionData? sessionData = (UsersLoginSessionData?)new HttpContextAccessor().HttpContext.Items["SessionData"]!;
            LoggedInUserId = sessionData.UserId ?? 0;
        }

        //public static void InitializesSessionData()
        //{ 

        //}
    }
}
