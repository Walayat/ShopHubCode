using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopHub.Models.Models
{
    public class Product
    {
        #region Fields
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public  int LocationId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Product Name should have max 50 characters")]
        public String Name { get; set; }
        [Required]
        public String Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Location Location { get; set; }
        public ICollection<Order> Orders { get; set; }
        #endregion

        #region Constructors
        private Product() { } //For Entity Framework
        public Product(String name, String price, Location location)
        {
            Name = name;
            Price = price;
            Quantity = 0;
            Location = location;
        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"{Id}:\t{Name} ({Quantity}, ${Price})";
        }
        #endregion
    }
}
