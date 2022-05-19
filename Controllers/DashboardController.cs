using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalLibrary.Filters;

namespace DigitalLibrary.Controllers
{
    [loginFilters]
    public class DashboardController : Controller
    {

        // GET: Dashboard
        public ActionResult Index()
        {
            
            return View();
            
        }

        public ActionResult userDashboard()
        {

            //Manager.BooksManager BM = new Manager.BooksManager();
            Manager.DeshboardManager DB = new Manager.DeshboardManager();
            List<Models.CatagoryModel> myList = DB.getActiveCatagoryList();
            Session["myList"] = myList;
            Session["temp"] = "We are here";



            return View(DB.getActiveCatagoryList());
        }

        [HttpGet]
        public ActionResult contact()
        {
            ViewBag.messae = "";
            return View();
        }

        [HttpPost]
        public ActionResult contact(Models.ContactModel _Contactmodel)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                Manager.sendMail sm = Manager.sendMail.createinstance;
                int a = sm.mail("info.myDigitalLibrary@gmail.com", _Contactmodel.subject, _Contactmodel.message + " \nName : " + _Contactmodel.name + "\nEmail : " + _Contactmodel.email);
                if(a>0)
                {
                    ModelState.Clear();
                    ViewBag.messae = "Successfully Sent";
                }
                else
                {
                    ViewBag.messae = "Network Error";
                }
               
            }

            return View();
        }
        public ActionResult membership()
        {
            return View();
        }
       

       
        public ActionResult goToCatagory(int ID)
        {
            Manager.DeshboardManager DB = new Manager.DeshboardManager();
            
            Models.staticValues.goToOption = ID;
            ViewBag.titleCatagory = DB.CatagoryTitleByID(ID);

            return View(DB.getBooks(ID));
        }

      

        public ActionResult read_it(int Book_ID)
        {
            Manager.BooksManager DB = new Manager.BooksManager();
            string bookPath = DB.getBookByID(Book_ID);

          
            string COMPLETEpATH = Server.MapPath(bookPath);


            Process.Start(COMPLETEpATH, "application/pdf");

            Session["temp"] = COMPLETEpATH;

            return RedirectToAction("goToCatagory",new { ID = Models.staticValues.goToOption });

        }

       

        public ActionResult LogOut()
        {
            Session["IsLogedIn"] = null;
            Session["IsAdmin"] = null;
            return RedirectToAction("Index", "Home");
        }


    }
}