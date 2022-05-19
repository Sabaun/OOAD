using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalLibrary.Controllers
{
    public class UserController : Controller
    {

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditUser()
        {
            Models.signUpModel sp = new Models.signUpModel();
            sp.name = Models.currentUser.name;
            sp.ID = Models.currentUser.ID;
            sp.username = Models.currentUser.username;
            sp.email = Models.currentUser.email;

            ViewBag.message = "";
            return View(sp);
        }

        [HttpPost]
        public ActionResult EditUser(Models.signUpModel ss,string currentPass)
        {
            
            if (ss.password.Length<6 || ss.password!=ss.confirm_pass)
            {
                ViewBag.message = "Password Not Match";
            }
            else if (ss.name==null)
            {
                ViewBag.message = "User Name Required";
            }
            else if (currentPass == Models.currentUser.password)
            {
                Manager.databaseManager DB = new Manager.databaseManager();
                bool a = DB.updatePassword(ss, Models.currentUser.ID);
                if (a == true)
                {
                    Models.currentUser.password = ss.password;
                    ViewBag.message = "Password Changed Successfully";
                    
                }
                else
                {
                    ViewBag.message = "Error. Try Again or contact Management";
                }
            }
            else
            {
                ViewBag.message = "Current Password Not Correct";
            }




            return View(ss);
        }




    }
}