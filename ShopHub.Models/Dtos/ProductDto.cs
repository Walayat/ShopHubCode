using ShopHub.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopHub.Models.Dtos
{
    public class ProductDto
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Location Name")]
        public int LocationId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Product Name should have max 50 characters")]
        [DisplayName("Product Name")]
        public String Name { get; set; }
        [Required]
        public String Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public List<LocationDto> Locations { get; set; }
        public Location Location { get; set; }
    }
}
