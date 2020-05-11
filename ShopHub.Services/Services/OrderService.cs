using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShopHub.Models.Context;
using ShopHub.Models.Dtos;
using ShopHub.Models.Models;
using ShopHub.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ShopHub.Services.Services
{
    public class OrderService : IOrderService
    {
        private ShopHubContext _context;
        private IMapper _mapper;
        private IProductService _productService;
        public OrderService(ShopHubContext shopHubContext, IMapper mapper, IProductService productService)
        {
            _context = shopHubContext;
            _mapper = mapper;
            _productService = productService;
        }

        //This method is used to save order of customer to database as well reduce quantity of ordered product from our stock
        public OrderDto SaveOrder(OrderDto order, int actualStockQuantity)
        {
            var mappedData = _mapper.Map<Order>(order);
            mappedData.Product = null;

            _context.Orders.Add(mappedData);
            _context.SaveChanges();

            var currentQuantity = actualStockQuantity - order.Quantity;
            _productService.MinusProductQuantity(order.ProductId, currentQuantity);

            return new OrderDto()
            {
                IsSucceedOrder = true
            };

        }

        /*This method is use to getting order Details by specific userId.
         here I am using Include(x => x.Product) which means to join and get order data with product details.
         Similar case for the location and User Include.
            */
        public List<Order> GetOrderHistoryByUserId(int userId)
        {
           var orderDetails =  _context.Orders.Include(x => x.Product).ThenInclude(x=>x.Location).Include(x => x.User).Where(x => x.UserId == userId).OrderByDescending(x=>x.Id).ToList();
            if (!(orderDetails is null) && orderDetails.Count > 0)
            {
                return orderDetails;
            }
            else
            {
                return null;
            }
        }

        /*This method is similar to above one except in this method we are getting all order details without specifying userId*/
        public List<Order> GetAllOrderHistory()
        {
            var orderDetails = _context.Orders.Include(x => x.Product).ThenInclude(x => x.Location).Include(x => x.User).OrderByDescending(x => x.Id).ToList();
            if (!(orderDetails is null) && orderDetails.Count > 0)
            {
                return orderDetails;
            }
            else
            {
                return null;
            }
        }

        /*This method is use to get all orders of specific location store.
          This method is differnt from all of others , as in this method 
          data is fetching from multiple tables of database using "SQL Stored Procedure"
          I am using spGetOrdersByLocation store procedure to get all orders against 
          specific location store.
          We can connect directly to SQL Stored Procedure using Dapper ORM (Object Relationship Mapper)
             */
        public List<OrderDto> GetAllStorOrdersByLocationId(int LocationId) 
        { 
            using (var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            { 
                var result =  conn.Query<OrderDto>(sql: "[spGetOrdersByLocation]", param: new { locationId = LocationId },
                commandType: CommandType.StoredProcedure); 
                
                return result.ToList(); 
            
            } 
        
        }

    }
}
