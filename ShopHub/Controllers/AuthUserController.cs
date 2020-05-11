using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopHub.Models.Dtos;
using ShopHub.Models.Models;
using ShopHub.Services.Interface;
using ShopHub.Services.Services;

namespace ShopHub.Controllers
{
    public class AuthUserController : Controller
    {
        private ISessionManager _sessionManager;
        private IUserService _userService;
        public AuthUserController(ISessionManager sessionManager, IUserService userService)
        {
            _sessionManager = sessionManager;
            _userService = userService;
        }
        
        //Register method use to register our new user to our system
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserAuthDto userModel)
        {
            if (ModelState.IsValid)
            {
              var result = await _userService.RegisterUser(userModel);

                _sessionManager.SetUserId(result.Id);
                _sessionManager.SetUserName(result.FirstName + " " + result.LastName);
                _sessionManager.SetUserTypeId(result.UserTypeId);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View(userModel);
            }
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserAuthDto userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AuthUser(userModel);
                if (result.IsSuccessFullLogin)
                {
                    _sessionManager.SetUserId(result.Id);
                    _sessionManager.SetUserName(result.FirstName + " " + result.LastName);
                    _sessionManager.SetUserTypeId(result.UserTypeId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong with your login details.");
                    return View(userModel);
                }
            }
            else
            {
                return View(userModel);
            }

        }

        public IActionResult Logout()
        {
            _sessionManager.SessionClear();
            return RedirectToAction("Login");
        }

    }
}