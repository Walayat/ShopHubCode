﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopHub.Filters;
using ShopHub.Models.Dtos;
using ShopHub.Models.Models;
using ShopHub.Services.Interface;
using ShopHub.Services.Utilities.Enums;

namespace ShopHub.Views.Home
{
    [AuthFilter(UserTypeNames.Admin,UserTypeNames.Customer)]
    public class CustomerController : Controller
    {
        private IProductService _productService;
        private ILocation _location;
        private IOrderService _orderService;
        private ISessionManager _sessionManager;
        public CustomerController(IProductService productService, ILocation location, IOrderService orderService, ISessionManager sessionManager)
        {
            _productService = productService;
            _location = location;
            _orderService = orderService;
            _sessionManager = sessionManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        //This method is use to populate location dropdown on store page
        public IActionResult StorePlace()
        {
            ProductDto product = new ProductDto();

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

        /*This method is the handy method to above one, as location will select from dropdown this method will render
         all products of selected location.
         This method return json array of products to view and we are populating these arrays to our view in tables.
             */
        public IActionResult GetProductsAgainstLocation(int locationId)
        {
            var products = _productService.GetProductsByLocationId(locationId);
            if (!(products is null))
            {
                return Json(products);
            }
            else
            {
                products = new List<ProductDto>();
                return Json(products);
            }
        }

        //This method will use when customer place their order to stores
        public IActionResult PlaceOrder(int userId, int productId, int quantity, int actualStockQuantity)
        {
            OrderDto order = new OrderDto()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
                ActualStockQuantity = actualStockQuantity,
                Timestamp = DateTime.UtcNow
            };
             _orderService.SaveOrder(order, actualStockQuantity);
            
            var returnobj = new { IsError = false, Message = "Your order is successfully placed" };
            return Json(returnobj);
        }

        //Customer can view their order history using this method
        public IActionResult OrderHistory()
        {
            var userId = _sessionManager.GetUserId();
            var orderDetails = _orderService.GetOrderHistoryByUserId(userId);
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
    }
}