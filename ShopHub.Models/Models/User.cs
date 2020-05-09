using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopHub.Models.Models
{
    public class User
    {
        #region Fields
        [Key]
        //[Database;enerated(DatabaseGeneratedOption.Identity)]
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
        public UserType UserType { get; set; }
        public ICollection<Order> Orders { get; set; }
        #endregion

        #region Constructors
        private User() { } //For Entity Framework
        public User(String firstName, String lastName, String password, UserType userType)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            UserType = userType;
        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"User Id:\t{Id}\n"
                + $"Name:\t\t{FirstName} {LastName}\n"
                + $"User Type:\t{UserType.Name}";
        }
        #endregion
    }
}
