using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopHub.Models.Models
{
    public class UserType
    {
        #region Fields
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Name should have max 20 characters")]
        public String Name { get; set; }
        [Required]
        [MaxLength(200,ErrorMessage ="Description should have max 200 characters")]
        public String Description { get; set; }
        public ICollection<User> Users { get; set; }
        #endregion
    }
}
