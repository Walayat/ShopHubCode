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
        private readonly ISessionManager _sessionManager;
        public HomeController(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
