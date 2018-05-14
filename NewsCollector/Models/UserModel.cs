using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsCollector.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20), Required]
        public string Name { get; set; }
        [MaxLength(20), Required]
        public string Surname { get; set; }
        
        [MaxLength(50), Required]
        public string Email { get; set; }        

        [MaxLength(11)]
        public string Status { get; set; }
    }
}