using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopHub.Filters;
using ShopHub.Models.Dtos;
using ShopHub.Models.Models;
using ShopHub.Services.Interface;
using ShopHub.Services.Utilities.Enums;

namespace ShopHub.Controllers
{
    [AuthFilter(UserTypeNames.Admin)]
    public class AdminController : Controller
    {
        /*Dependency Injection Code Start
         As we are not creating instance like
         ProductService product = new ProductService()
         we are creating private type of class type variable (obj)
         which is populating from constructor. So there is not need
         to dispose the object it will automatically dispose from memroy
             */

       /*IProductService is a interface which contains definitions of
      some methods and these method's implementations are in their 
      respective services class i.e for IProductService there is a
      service class which is ProductService.
         */
        /*ILocation is a interface which contains definitions of 
          store location details. LocationService implements ILocation.
            */
        /*IOrderService is a interface which contains definitions of 
           orders details. OrderService implements IOrderService.
        */
        
        private ILocation _location;
        private IProductService _productService;
        private IOrderService _orderService;
        public AdminController(ILocation location, IProductService productService, IOrderService orderService)
        {
            _location = location;
            _productService = productService;
            _orderService = orderService;
        }
        /*Dependency Injection Code End*/


        #region Location i.e Create, Delete , List

        /*This method only return a view for create new location*/
        [HttpGet]
        public IActionResult CreateLocation()
        {
            return View();
        }

        /* Post method for create location
           In every post method I am checking 
           the server side validation that they 
           are ok or not as defined on annoations.
           If model data is valid then I am using 
           CreateLocation service to create location.
           As these services interacting with database
           layer and interchange data in all out application.
             */
        [HttpPost]
        public IActionResult CreateLocation(LocationDto location)
        {
            if (ModelState.IsValid)
            {
                _location.CreateLocation(location);
                return RedirectToAction("Location");
            }
            else
            {
                return View(location);
            }
        }
        /*As clear from name this method is for delete the
         location by its Id, and after remove location
         RedirectToAction redirect page to location listing
         page where we can see location is remove or not.
             */
        public IActionResult DeleteLocation(int locationId)
        {
            _location.RemoveLocation(locationId);
            return RedirectToAction("Location");
        }

        /*This method is simply use to get all locations
         using service GetAllLocations, this method is use
         for listing purpose.
             */
        public IActionResult Location()
        {
            var locations = _location.GetAllLocations();
            if (locations is null)
            {
                locations = new List<LocationDto>();
            }
            return View(locations);
        }
        #endregion


        #region Products All CRUD

        /* Get method for create product
        Here I am using GetAllLocations service to 
        populate locations dropdown and returning
        product type dto which contains list of
        locations in it.
             */

        [HttpGet]
        public IActionResult CreateProduct()
        {
            ProductDto product = new ProductDto();
            var locations = _location.GetAllLocations();
            if (locations is null)
            {
                locations = new List<LocationDto>();
            }
            else
            {
                product.Locations = locations;
            }
            return View(product);
        }

        /* Post method for create product
           In every post method I am checking 
           the server side validation using
           ModelState.IsValid that the given model properites 
           are ok or not as defined on annoations.
           If model data is valid then I am using 
           AddProduct service to create product.

             */
        [HttpPost]
        public IActionResult CreateProduct(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction("ProductList");
            }
            else
            {
                return View(product);
            }
        }

        /*This method is simply use to get all products
         using service GetAllProducts, this method is use
         for listing purpose.
             */
        public IActionResult ProductList()
        {
            var products = _productService.GetAllProducts();
            if (products is null)
            {
                products = new List<ProductDto>();
            }
            return View(products);
        }

        /*As clear from name this method is for delete the
         product by its Id, and after remove product
         RedirectToAction redirect page to product listing
         page where we can see product is remove or not.
             */
        public IActionResult DeleteProduct(int productId)
        {
            _productService.RemoveProduct(productId);
            return RedirectToAction("ProductList");
        }

        /*This method is use to update product,
          as this is the get method so it will 
          populate relevant data to its fields.
          I am getting the product by its Id and 
          then populating its fields for update.
             */
        [HttpGet]
        public IActionResult UpdateProduct(int productId)
        {
           var productData = _productService.GetProductById(productId);
            var locations = _location.GetAllLocations();
            if (locations is null)
            {
                locations = new List<LocationDto>();
                productData.Locations = locations;
            }
            else
            {
                productData.Locations = locations;
            }
           return View(productData);
        }

        /*As this one this the post method I am using 
         UpdateProduct service to update and then redirecting
         my page to listing page to see the changes reflects.
             */
        [HttpPost]
        public IActionResult UpdateProduct(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction("ProductList");
            }
            else
            {
                return View(product);
            }
        }
        #endregion

        #region Customer Order History Details

        //This method is for Location base order
        //In this method I just populating the locations
        //dropdown. Location base order history will
        // populating with another method by making an ajax get call
        public IActionResult LocationOrderHistory()
        {
            OrderDto product = new OrderDto();

            var locations = _location.GetAllLocations();
            if (locations is null)
            {
                locations = new List<LocationDto>();
                product.Locations = locations;
            }
            else
            {
                product.Locations = locations;
            }
            return View(product);
        }

        // In this method I will populate orders base on location Id
        // This method is handy to above LocationOrderHistory method
        // It will be call through ajax get request from LocationOrderHistory View
        public IActionResult LocationBaseOrderData(int locationId)
        {
           var data = _orderService.GetAllStorOrdersByLocationId(locationId);
           return Json(data);
        }

        // In this method all order of stores are populating without any location specify
        public IActionResult AllOrderHistory()
        {
            var orderDetails = _orderService.GetAllOrderHistory();
            if (!(orderDetails is null) && orderDetails.Count > 0)
            {
                return View(orderDetails);
            }
            else
            {
                orderDetails = new List<Order>();
                return View(orderDetails);
            }
        }
        #endregion
    }
}