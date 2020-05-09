using Microsoft.AspNetCore.Http;
using ShopHub.Models.Models;
using ShopHub.Services.Interface;
using ShopHub.Services.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopHub.Services.Services
{
    public class SessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }
        public int GetUserId()
        {
            return Convert.ToInt32(_session.GetString(SessionDetails.UserId));
        }

        public string GetUserName()
        {
            return _session.GetString(SessionDetails.UserName);
        }

        public int GetUserTypeId()
        {
            return Convert.ToInt32(_session.GetString(SessionDetails.UserTypeId));
        }

        public void SessionClear()
        {
            _session.Clear();
        }

        public void SetUserId(int userId)
        {
            _session.SetString(SessionDetails.UserId, userId.ToString());
        }

        public void SetUserName(string userName)
        {
            _session.SetString(SessionDetails.UserName, userName);
        }

        public void SetUserTypeId(int userTypeId)
        {
            _session.SetString(SessionDetails.UserTypeId, userTypeId.ToString());
        }
    }
}
