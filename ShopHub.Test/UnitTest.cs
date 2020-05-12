using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopHub.Controllers;
using ShopHub.Models.Context;
using ShopHub.Models.Dtos;
using ShopHub.Models.Models;
using ShopHub.Services.Interface;
using ShopHub.Views.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShopHub.Test
{
    public class UnitTest
    {
        //private IProductService _productService;
        private Mock<ILocation> _location;
        private Mock<IOrderService> _orderService;
        private Mock<ISessionManager> _sessionManager;
        private Mock<ShopHubContext> _context;
        private Mock<IMapper> _mapper;
        private Mock<IProductService> _productService;
        public UnitTest()
        {
            _productService = new Mock<IProductService>();
            _location = new Mock<ILocation>();
            _orderService = new Mock<IOrderService>();
            _sessionManager = new Mock<ISessionManager>();
            _context = new Mock<ShopHubContext>();
            _mapper = new Mock<IMapper>();
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

        /*Test product list scenario using simulation list of product without interacting with database
        In this test case we are also verifying the model count quantity are same or not.*/
        [Fact]
        public void Test_ProductList_AdminController()
        {
            // Arrange
            var productList = new List<ProductDto>
            {
                new ProductDto {  Id = 1, LocationId = 2 , Name = "Test product 1", Price = "500", Quantity =20 },
                new ProductDto {  Id = 2, LocationId = 3 , Name = "Test product 2", Price = "400", Quantity =30 }
            };
            _productService
                .Setup(repo => repo.GetAllProducts())
                .Returns(productList);
            var controller = new AdminController(_location.Object, _productService.Object, _orderService.Object);

            // Act
            var result =  controller.ProductList();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }


        /*Test create product Post Method scenario using simulation object without interacting with database
          In this test case we are also verifying redirect to action Name value after successfull
          creation of product.*/
        [Fact]
        public void CreateProduct_When_ModelState_IsValid_AdminController()
        {
            // Arrange

            var controller = new AdminController(_location.Object, _productService.Object, _orderService.Object);
            var newEmployee = HelperMethods.GetTestProduct();

            // Act
            var result = controller.CreateProduct(newEmployee);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("ProductList", redirectToActionResult.ActionName);
            _productService.Verify();
        }


        /*Test create product Get Method scenario using simulation object without interacting with database
          In this test case we are also verifying redirect to action Name value after successfull
          creation of product.*/
        [Fact]
        public void CreateProduct_GetMethod_AdminController()
        {
            // Arrange

            var controller = new AdminController(_location.Object, _productService.Object, _orderService.Object);
            var newEmployee = HelperMethods.GetTestProduct();

            // Act
            var result = controller.CreateProduct(newEmployee);

            // Assert
            //Assert.IsType(ProductDto,  viewResult.Model);
            _productService.Verify();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductDto>(viewResult.ViewData.Model);
            //Assert.Equal(2, model);
        }

        /*Test product list scenario using simulation list of product without interacting with database
        In this test case we are also verifying the model count quantity are same or not.*/
        [Fact]
        public void Test_LocationList_AdminController()
        {
            // Arrange
            var locationList = new List<LocationDto>
            {
                new LocationDto {  Id = 1, Name = "Test location1"},
                new LocationDto {  Id = 2, Name = "Test location2"},
            };
            _location
                .Setup(repo => repo.GetAllLocations())
                .Returns(locationList);
            var controller = new AdminController(_location.Object, _productService.Object, _orderService.Object);

            // Act
            var result = controller.Location();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<LocationDto>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
            //Assert.Equal(2, model.Count());
        }


        /*This test case test the method logic of delete the location 
          and confirm there is no exception occur, and verify the redirect
          result is location or not.
             */
        [Fact]
        public void DeleteLocation_Test_AdminController()
        {
            // Arrange
            var controller = new AdminController(_location.Object, _productService.Object, _orderService.Object);

            // Act
            var result = controller.DeleteLocation(2);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Location", redirectToActionResult.ActionName);
        }


        /*This test case test the method logic of delete the product 
          and confirm there is no exception occur, and verify the redirect
          result is ProductList or not.
             */
        [Fact]
        public void DeleteProduct_Test_AdminController()
        {
            // Arrange
            var controller = new AdminController(_location.Object, _productService.Object, _orderService.Object);

            // Act
            var result = controller.DeleteProduct(2);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ProductList", redirectToActionResult.ActionName);
        }

        /*This test case test the method logic of update the product 
          and confirm there are no exception occur, and verify the redirect
          result is ProductList or not.
             */
        [Fact]
        public void UpdateProduct_Test_AdminController()
        {
            // Arrange
            var controller = new AdminController(_location.Object, _productService.Object, _orderService.Object);
            var productObj = HelperMethods.GetTestProduct();
            // Act
            var result = controller.UpdateProduct(productObj);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ProductList", redirectToActionResult.ActionName);
        }

        /* Customer Controller Index return view value, as it should be null because
           we are not returning anything to the view*/
        [Fact]
        public void Customer_Controller_Index_View_TestCase()
        {
            // Arrange
            var controller = new CustomerController(_productService.Object, _location.Object, _orderService.Object,_sessionManager.Object);
            string viewName = null;

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal(viewName, result.ViewName);
        }


        /*Test product list scenario using simulation list of product without interacting with database
        In this test case we are also verifying the model count quantity are same or not.*/
        [Fact]
        public void Test_GetProductsAgainstLocation_CustomerController()
        {
            // Arrange
            var productList = new List<ProductDto>
            {
                new ProductDto {  Id = 1, LocationId = 2 , Name = "Test product 1", Price = "500", Quantity =20 },
                new ProductDto {  Id = 2, LocationId = 3 , Name = "Test product 2", Price = "400", Quantity =30 }
            };
            _productService
                .Setup(repo => repo.GetProductsByLocationId(2))
                .Returns(productList);
            var controller = new CustomerController(_productService.Object, _location.Object, _orderService.Object, _sessionManager.Object);

            // Act
            var result = controller.GetProductsAgainstLocation(2);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            //Assert.Equal(2, jsonResult.Value);
           // var model = Assert.IsAssignableFrom<IEnumerable<LocationDto>>(JsonResult.);
           // Assert.Equal(2, model.Count());
        }
    }
}
