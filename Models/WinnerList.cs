using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lucky.Models
{
    public class WinnerList
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]

        public string Email { get; set; }
        public string PrizeType { get; set; }
        [Required]
        public int WinningNumber { get; set; }
    }
}