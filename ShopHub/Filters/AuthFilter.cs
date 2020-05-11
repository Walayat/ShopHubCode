using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using ShopHub.Services.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopHub.Filters
{
    /* In Asp.net Mvc there are five types of different filters which runs
     before and after the request came to specific method or controllers.
     One of them is "ActionFilterAttribute" which having method OnActionExecuting
     We are using this filter i.e AuthFilter to our controllers to filter out users
     according to their userTypes or we can say role type.

     This is AuthFilter means user authentication and authorization filter.
     It checks wheather sessions of user are available or not, if sessions are
     not available it will redirect user to login page.
     And if user is not an admin then user cannot be visit the admin pages i.e authorization work.
        */
    public class AuthFilter : ActionFilterAttribute
    {
        public string[] Roles { get; set; }
        public AuthFilter(params string[] roles)
        {
            this.Roles = roles;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userTypeId = context.HttpContext.Session.GetString(SessionDetails.UserTypeId);

            if (context.HttpContext.Session.GetString(SessionDetails.UserId) == null)
            {
                string[] excludePath = { "/AuthUser/Login" };

                if (!excludePath.Contains(context.HttpContext.Request.Path.Value))
                {
                    bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

                    if (!isAjaxCall)
                    {
                        context.Result = new RedirectToRouteResult("Default",
                           new RouteValueDictionary{
                                {"controller", "AuthUser"},
                                {"action", "Login"},
                                {"returnUrl", context.HttpContext.Request.Path.Value}
                           });
                    }
                }
            }
            else
            {
                if (!Roles.Contains(userTypeId))
                {
                    context.Result = new RedirectToRouteResult("Default",
                            new RouteValueDictionary{
                            {"controller", "Home"},
                            {"action", "AccessDenied"},
                            {"returnUrl", context.HttpContext.Request.Path.Value}
                            });
                }

            }

            base.OnActionExecuting(context);
        }

    }
}
