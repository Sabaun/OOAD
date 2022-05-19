using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Models
{
    public class AdminDashboardInformation
    {
        public int totalBooks { get; set; }
        public List<string[]> catagoryviseList { get; set; }

        public int AllUsers { get; set; }
        public int totalUsers { get; set; }

        public int totalAdmins { get; set; }


       public int pendingApprovels { get; set; }

        public string Name { get; set; }
        public string RegesteredEmail { get; set; }

        public string LoginTime { get; set; }

        public int PandingUserUploads { get; set; }
    }
}