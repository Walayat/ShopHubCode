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
        /*IUserService is a interface which contains definitions of 
          user login and register methods. UserService implements IUserService.
        
        /*ISessionManager is a interface which is use to set
          and get the sessions details like Id,Name,UserTypeId of logged in user
          SessionManager implements ISessionManager.
        */
        private ISessionManager _sessionManager;
        private IUserService _userService;
        public AuthUserController(ISessionManager sessionManager, IUserService userService)
        {
            _sessionManager = sessionManager;
            _userService = userService;
        }
        
        //This method only returns view of registeration page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /*Register post method use to register our new user to our system
          I am using RegisterUser to register user and afer register to
          system I am setting the session values of logged in user.
             */
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
        //This method only returns view of login page
        public IActionResult Login()
        {
            return View();
        }

        /*Login post method use to authenticate user to our system
        I am using _userService.AuthUser to authenticate user and afer
        login to the system I am setting the session values of logged in 
        user and if it is not authentic user then i am pushing an error
        using ModelState.AddModelError .
            */
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

        /*This method is simply use to destroy sessions of
         login in user and redirect the user to login page
         */
        public IActionResult Logout()
        {
            _sessionManager.SessionClear();
            return RedirectToAction("Login");
        }

    }
}