using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalLibrary.Filters;

namespace DigitalLibrary.Controllers
{
   
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.message="";
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.loginModel _loginModel)
        {

            if (ModelState.IsValid)
            {
                Manager.databaseManager DB = new Manager.databaseManager();

                
                bool exist = DB.checkLogin(_loginModel);

                if (exist==true)
                {
                    Session["IsLogedIn"] = true;
                    if (Models.currentUser.isAdmin==1)
                    {
                        Session["ISAdmin"] = true;
                       
                        return RedirectToAction("adminOrUser", "Home", null);
                    }
                    return RedirectToAction("userDashboard", "Dashboard", null);

                }
                else
                {
                    ViewBag.message = "Error Login";
                }
            }
            return View();
        }

        [HttpGet]


        public ActionResult SignUpForm()
        {
            ViewBag.message = " ";
            return View();
        }


        [HttpPost]
        public ActionResult SignUpForm(Models.signUpModel sp)
        {
            Manager.databaseManager DB = new Manager.databaseManager();


            if (ModelState.IsValid)
            {
                sp.isAdmin = 0;

                if (!DB.already_Exist_Email_or_userName(sp))
                {
                    bool a = DB.signup(sp);

                    if (a == true)
                    {
                        return RedirectToAction("viewData", "Home", sp);
                    }
                }
               
            }


            ViewBag.message = "Invalid UserName or Email (email or username already used)";
            return View();

            

        }

        [HttpGet]
        public ActionResult forgetPassword()
        {
            ViewBag.message = "";
            return View();
        }


        [HttpPost]
        public ActionResult forgetPassword(string Email, string userName)
        {
            Manager.databaseManager DB = new Manager.databaseManager();
            bool a = DB.sendMail(Email, userName);

            if (a==true)
            {

                ViewBag.message = "Password has been sent to your Provided Email";
            }
            else
            {

                ViewBag.message = "Provided Email Is Invalid";
            }

            


            return View();

        }


        public ActionResult emailVerification()
        {
            return View();
        }

        [loginFilters]

        public ActionResult viewData(Models.signUpModel view)
        {

            


            return View(view);


        }

        [loginFilters]

        public ActionResult adminOrUser()
        {
            
            return View();


        }

      

        

    }
}