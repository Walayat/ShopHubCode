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

        public OrderDto SaveOrder(OrderDto order, int actualStockQuantity)
        {
            var mappedData = _mapper.Map<Order>(order);
            _context.Orders.Add(mappedData);
            _context.SaveChanges();

            var currentQuantity = actualStockQuantity - order.Quantity;
            _productService.MinusProductQuantity(order.ProductId, currentQuantity);

            return new OrderDto()
            {
                IsSucceedOrder = true
            };

        }

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
