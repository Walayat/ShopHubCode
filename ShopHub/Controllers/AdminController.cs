using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopHub.Filters;
using ShopHub.Models.Dtos;
using ShopHub.Services.Interface;
using ShopHub.Services.Utilities.Enums;

namespace ShopHub.Controllers
{
    [AuthFilter(UserTypeNames.Admin)]
    public class AdminController : Controller
    {
        private ILocation _location;
        private IProductService _productService;
        public AdminController(ILocation location, IProductService productService)
        {
            _location = location;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }



        #region Location i.e Create, Delete , List

        [HttpGet]
        public IActionResult CreateLocation()
        {
            return View();
        }

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

        public IActionResult DeleteLocation(int locationId)
        {
            _location.RemoveLocation(locationId);
            return RedirectToAction("Location");
        }

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

        public IActionResult ProductList()
        {
            var products = _productService.GetAllProducts();
            if (products is null)
            {
                products = new List<ProductDto>();
            }
            return View(products);
        }

        public IActionResult DeleteProduct(int productId)
        {
            _productService.RemoveProduct(productId);
            return RedirectToAction("ProductList");
        }


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
    }
}