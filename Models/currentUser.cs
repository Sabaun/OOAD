using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Models
{
    public class currentUser
    {
        public static int ID { get; set; }

  

        public static string name { get; set; }

       
        public static string email { get; set; }

       
        public static string username { get; set; }
        
        public static string password { get; set; }

      
        public static string confirm_pass { get; set; }

        
        public static int isAdmin { get; set; }

        public static string loginTime { get; set; }
    }
}