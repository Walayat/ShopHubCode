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
