using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopHub.Models.Dtos
{
    public class LocationDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Location Name should have max 50 characters")]
        [DisplayName("Location Name")]
        public String Name { get; set; }
    }
}
