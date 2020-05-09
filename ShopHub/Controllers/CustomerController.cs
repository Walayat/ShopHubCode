using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopHub.Filters;
using ShopHub.Services.Utilities.Enums;

namespace ShopHub.Views.Home
{
    [AuthFilter(UserTypeNames.Admin,UserTypeNames.Customer)]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}