using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopHub.Controllers;
using ShopHub.Models.Models;
using ShopHub.Services.Interface;
using System;
using Xunit;

namespace ShopHub.Test
{
    public class UnitTest
    {
        private IProductService _productService;
        private ILocation _location;
        private IOrderService _orderService;
        private ISessionManager _sessionManager;
        public UnitTest(IProductService productService, ILocation location, IOrderService orderService, ISessionManager sessionManager)
        {
            _productService = productService;
            _location = location;
            _orderService = orderService;
            _sessionManager = sessionManager;
        }


        /* Check get method return view value, as it should be null because
           in get request of register we are not returning anything*/
        [Fact]
        public void AuthUser_Register_ReturnView_Test()
        {
            // Arrange
            var controller = new AuthUserController(null,null);
            string viewName = null;

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.Equal(viewName, result.ViewName);
        }

        /*This test case should be fail as we are invoking modelError of FirstName
         *BadRequest Error should not appear to front end just provide us 
          the invalid modelState when model is not valid */
        [Fact]
        public void Add_ReturnsBadRequestResult_WhenModelStateIsInvalid_InRegister_Case()
        {
            // Arrange
            var controller = new AuthUserController(null,null);
            controller.ModelState.AddModelError("FirstName", "Required");
            var newUser = HelperMethods.GetTestUserIdForRegisterAndLogin();

            // Act
            var result = controller.Register(newUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        /* This test case should be fail as we are invoking modelError of LastName
         * BadRequest Error should not appear to front end just provide us 
          the invalid modelState when we provide an invalid model */
        [Fact]
        public void Add_WhenModelStateIsInvalid_InRegister_Case()
        {
            // Arrange
            var controller = new AuthUserController(null, null);
            controller.ModelState.AddModelError("LastName", "Required");
            var newUser = HelperMethods.GetTestUserIdNameForRegisterAndLogin();

            // Act
            var result = controller.Register(newUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }


        /* This test case should be fail as we are invoking modelError of UserTypId
         * BadRequest Error should not appear to front end just provide us 
          the invalid modelState when we provide an invalid model */
        [Fact]
        public void Add_WhenModelStateIsInvalid_InRegister_Case_UserTypeId()
        {
            // Arrange
            var controller = new AuthUserController(null, null);
            controller.ModelState.AddModelError("UserTypId", "Required");
            var newUser = HelperMethods.GetTestUserIdCompleteNameForRegisterAndLogin();

            // Act
            var result = controller.Register(newUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        /* This test case should be fail as we are invoking modelError of Password
         * BadRequest Error should not appear to front end just provide us 
          the invalid modelState when we provide an invalid model */
        [Fact]
        public void Add_WhenModelStateIsInvalid_InRegister_Case_Password()
        {
            // Arrange
            var controller = new AuthUserController(null, null);
            controller.ModelState.AddModelError("Password", "Required");
            var newUser = HelperMethods.GetTestUserIdNameUserTypeForRegisterAndLogin();

            // Act
            var result = controller.Register(newUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }


        /* Check get method return view value, as it should be null 
           because in get request of login we are not returning anything*/
        [Fact]
        public void AuthUser_Login_ReturnView_Test()
        {
            // Arrange
            var controller = new AuthUserController(null,null);
            string viewName = null;

            // Act
            var result = controller.Login() as ViewResult;

            // Assert
            Assert.Equal(viewName, result.ViewName);
        }


        /* Home Controller Index return view value, as it should be null because
           we are not returning anything*/
        [Fact]
        public void Home_Controller_Index_ViewTestCase()
        {
            // Arrange
            var controller = new HomeController(null);
            string viewName = null;

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal(viewName, result.ViewName);
        }

        /* Home Controller AccessDenied return view value, as it should be null because
           we are not returning anything*/
        [Fact]
        public void Home_Controller_AccessDenied_ViewTestCase()
        {
            // Arrange
            var controller = new HomeController(null);
            string viewName = null;

            // Act
            var result = controller.AccessDenied() as ViewResult;

            // Assert
            Assert.Equal(viewName, result.ViewName);
        }

        /* Home Controller Error return view value, as it should be null because
           we are not returning anything*/
        [Fact]
        public void Home_Controller_Error_ViewTestCase()
        {
            // Arrange
            var controller = new HomeController(null);
            string viewName = null;

            // Act
            var result = controller.Error() as ViewResult;

            // Assert
            Assert.Equal(viewName, result.ViewName);
        }
        
        
    }
}
