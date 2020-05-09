using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopHub.Models.Dtos
{
    public class UserAuthDto
    {
        [Key]
        public int Id { get; set; }

        public int UserTypeId { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "First Name should have max 50 characters")]
        [DisplayName("First Name")]
        public String FirstName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Last Name should have max 50 characters")]
        [DisplayName("Last Name")]
        public String LastName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Password should have max 50 characters")]
        public String Password { get; set; }

        public bool IsSuccessFullLogin { get; set; }

    }
}
