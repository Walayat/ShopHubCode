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
        //IHttpContextAccessor give us access to the browser sessions

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        //This method is use to get login UserId
        public int GetUserId()
        {
            return Convert.ToInt32(_session.GetString(SessionDetails.UserId));
        }

        //This method is use to get login UserName
        public string GetUserName()
        {
            return _session.GetString(SessionDetails.UserName);
        }

        //This method is use to get login UserTypeId
        public int GetUserTypeId()
        {
            return Convert.ToInt32(_session.GetString(SessionDetails.UserTypeId));
        }

        //This method is use to clear the browser sessions
        public void SessionClear()
        {
            _session.Clear();
        }

        //This method is use to set login UserId
        public void SetUserId(int userId)
        {
            _session.SetString(SessionDetails.UserId, userId.ToString());
        }

        //This method is use to set login UserName
        public void SetUserName(string userName)
        {
            _session.SetString(SessionDetails.UserName, userName);
        }

        //This method is use to set login UserTypeId
        public void SetUserTypeId(int userTypeId)
        {
            _session.SetString(SessionDetails.UserTypeId, userTypeId.ToString());
        }
    }
}
