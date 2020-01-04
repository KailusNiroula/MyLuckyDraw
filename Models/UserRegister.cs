using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lucky.Models
{
    public class UserRegister
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }

        [StringLength(400, MinimumLength = 0)]
        [Index(IsUnique = true)]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}