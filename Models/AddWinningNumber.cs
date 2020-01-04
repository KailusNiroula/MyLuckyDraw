using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lucky.Models
{
    public class AddWinningNumber
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="You must login first")]
        public string UserId { get; set; }

        [Required]
       // [RegularExpression("\\d{4}")]
        public int LuckyNo { get; set; }

        [StringLength(4,MinimumLength =4,ErrorMessage ="Enter only 4 Digit")]
        [RegularExpression(@"\d{4}",ErrorMessage ="Enter 4 Digit")]
        public string Byhand { get; set; }
    }
}