using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using DigitalLibrary.Filters;


namespace DigitalLibrary.Controllers
{
    [Filters.adminFilter]
    public class AdminController : Controller
    {

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminView()
        {


            Manager.adminManager DB = new Manager.adminManager();
            Session["name"] = Models.currentUser.name;
            Session["email"] = Models.currentUser.email;
            return View(DB.getAdminDeshboard());
        }


        public ActionResult ManageUsers()
        {
            List<Models.signUpModel> _signUpModels = new List<Models.signUpModel>();

            Manager.databaseManager DB = new Manager.databaseManager();

            _signUpModels = DB.getAllUsers();

            return View(_signUpModels);
        }

        public ActionResult ManageOtherAdmins()
        {
            return View();
        }

        public ActionResult DeleteUser(int userID)
        {
            Manager.databaseManager DB = new Manager.databaseManager();
            bool a=DB.deleteAUser(userID);

            

            
            return RedirectToAction("ManageUsers", "Admin");

        }

        public ActionResult MakeAdmin(int userID)
        {
            Manager.databaseManager DB = new Manager.databaseManager();
            bool a = DB.makeAdmin(userID);




            return RedirectToAction("ManageUsers", "Admin");

        }


        public ActionResult RemoveAdmin(int userID)
        {
            Manager.databaseManager DB = new Manager.databaseManager();
            bool a = DB.removeAdmin(userID);




            return RedirectToAction("ManageUsers", "Admin");

        }

        public ActionResult viewBooks()
        {
            Manager.BooksManager DB = new Manager.BooksManager();

            List<Models.readBook> Books = DB.getBooks();

            return View(Books);

        }


        public ActionResult read_it(int Book_ID)
        {
            Manager.BooksManager DB = new Manager.BooksManager();
            string bookPath=DB.getBookByID(Book_ID);


            string COMPLETEpATH = Server.MapPath(bookPath);
            Process.Start(COMPLETEpATH,"application/pdf");


            return RedirectToAction("viewBooks");

        }


        public ActionResult userUpload()
        {

            Manager.adminManager DB = new Manager.adminManager();

            List<Models.readBook> Books = DB.getUserUploadedBooks();

            return View(Books);
        }

        public ActionResult Add_userupload_to_library(int Book_ID)
        {
            Manager.BooksManager DB = new Manager.BooksManager();
            bool a = DB.moveBook_temp_to_permanent(Book_ID);
           


            return RedirectToAction("userUpload");
        }


        public ActionResult deleteUserUpload(int Book_ID)
        {

            Manager.BooksManager DB = new Manager.BooksManager();
            string bookPath = DB.get_userUploaded_BookByID(Book_ID);
            bool a = DB.delete_userUploaded_BookByID(Book_ID);

            string COMPLETEpATH = Server.MapPath(bookPath);
            
            string direc = Path.GetDirectoryName(COMPLETEpATH)+"//";

            if(a==true)
            Directory.Delete(direc,true);
           


            return RedirectToAction("userUpload");

           
        }


        public ActionResult deleteBook(int Book_ID)
        {

            Manager.BooksManager DB = new Manager.BooksManager();
            string bookPath = DB.getBookByID(Book_ID);
            bool a = DB.delete_BookByID(Book_ID);

            string COMPLETEpATH = Server.MapPath(bookPath);

            string direc = Path.GetDirectoryName(COMPLETEpATH) + "//";

            if (a == true)
                Directory.Delete(direc, true);



            return RedirectToAction("viewBooks");


        }


        public ActionResult read_user_upload(int Book_ID)
        {
            

            Manager.BooksManager DB = new Manager.BooksManager();
            string bookPath = DB.get_userUploaded_BookByID(Book_ID);


            string COMPLETEpATH = Server.MapPath(bookPath);
            Process.Start(COMPLETEpATH, "application/pdf");


            return RedirectToAction("userUpload");

        }





    }
}