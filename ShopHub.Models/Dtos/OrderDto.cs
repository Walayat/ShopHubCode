using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopHub.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsSucceedOrder { get; set; }
        public int ActualStockQuantity { get; set; }
        public List<LocationDto> Locations { get; set; }
        public int LocationId { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }

    }
}
