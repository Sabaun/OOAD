using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Models
{
    public class loginModel
    {
        [Required]
        [EmailAddress]
        public string userName { get; set; }

        [Required]

        public string password { get; set; }

        
        public bool remember { get; set; }
    }
}