using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopHub.Filters;
using ShopHub.Models;
using ShopHub.Services.Interface;
using ShopHub.Services.Utilities.Enums;

namespace ShopHub.Controllers
{
    [AuthFilter(UserTypeNames.Admin, UserTypeNames.Customer)]
    public class HomeController : Controller
    {
         /*ISessionManager is a interface which is use to set
          and get the sessions details like Id,Name,UserTypeId of 
          logged in user SessionManager implements ISessionManager.*/

        private readonly ISessionManager _sessionManager;
        public HomeController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        /*This is the main view arrive when customer or admin login to application*/
        public IActionResult Index()
        {
            return View();
        }

        //This method will call when an simple user will try to access admin pages from URL
        public IActionResult AccessDenied()
        {
            return View();
        }

        //This method is added for the reason of Global Exception handling purposes
        public IActionResult Error()
        {
            return View();
        }

    }
}
