using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalLibrary.Filters;

namespace DigitalLibrary.Controllers
{
    [Filters.loginFilters]
    public class BooksController : Controller
    {

        // GET: Books
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult viewBooks()
        {
            return View();
        }

        [HttpGet]
        public ActionResult uploadBooks()
        {
            Manager.BooksManager DB = new Manager.BooksManager();
            ViewBag.itemList = DB.CatagoryListTitles();
            return View();
        }

        [HttpPost]
        public ActionResult uploadBooks(Models.uploadBook _uploadBook)
        {
            if (ModelState.IsValid)
            {


                bool a = upload_a_Book(_uploadBook);

                if (a == true)
                {

                    ModelState.Clear();
                    ViewBag.Message = "Operation Successfull";
                    ViewBag.operation = "successfull";
                }
                else
                {
                    ViewBag.Message = "Error uploading book";
                    ViewBag.operation = "";
                }

               
            }
            else
            {
                ViewBag.Message = "Please fill all required fields";
                ViewBag.operation = "";
            }

            Manager.BooksManager DB = new Manager.BooksManager();

            ViewBag.itemList = DB.CatagoryListTitles();

            return View();
        }

        [HttpGet]
        public ActionResult addCatagory()
        {
            Models.CatagoryModel cm = new Models.CatagoryModel();
            Manager.BooksManager DB = new Manager.BooksManager();

            cm.listOfAvailableCatagories = DB.getAllCatagoryList();

            return View(cm);
        }

        [HttpPost]
        public ActionResult addCatagory(Models.CatagoryModel _catagoryModel)
        {
            Manager.BooksManager DB = new Manager.BooksManager();
            bool a = DB.addCatagory(_catagoryModel.catagoryName);



            Models.CatagoryModel cm = new Models.CatagoryModel();
            cm.listOfAvailableCatagories = DB.getAllCatagoryList();

            if (a)
                ViewBag.Message = "Successfully Added";

            ModelState.Clear();

            return View(cm);
        }

        [HttpGet]
        public ActionResult membershipBooks()
        {
            return View();
        }

    [HttpPost]

        public ActionResult membershipBooks(List<string> myData)
        {
            return View();
        }


        public FileResult Download(int Book_ID)
        {
            Manager.BooksManager DB = new Manager.BooksManager();
            string bookPath = DB.getBookByID(Book_ID);


            string COMPLETEpATH = Server.MapPath(bookPath);
            
            

            return File(COMPLETEpATH, "application/pdf","Book.pdf");

        }

        public ActionResult searchBook(string searchBar)
        {
            Manager.BooksManager DB = new Manager.BooksManager();


            return View(DB.getBooksBySearchTag(searchBar));
        }





        public bool upload_a_Book(Models.uploadBook _uploadBook)
        {
            try
            {

                string fileName = Path.GetFileNameWithoutExtension(_uploadBook.ImageFile.FileName);
                string pdfName = Path.GetFileNameWithoutExtension(_uploadBook.pdfFile.FileName);

                string extension = Path.GetExtension(_uploadBook.ImageFile.FileName);
                string extension2 = Path.GetExtension(_uploadBook.pdfFile.FileName);

                string bookID = (DateTime.Now.ToString("ddmmyyssmmffff"));

                fileName = bookID + extension;
                pdfName = bookID + extension2;


                _uploadBook.imagePath = "~/Content/Books/" + bookID + "/" + fileName;
                _uploadBook.bookPath = "~/Content/Books/" + bookID + "/" + pdfName;


                string _Directory = Server.MapPath("~/Content/Books/" + bookID);

                Directory.CreateDirectory(_Directory);



                fileName = Path.Combine(Server.MapPath("~/Content/Books/" + bookID), fileName);
                pdfName = Path.Combine(Server.MapPath("~/Content/Books/" + bookID), pdfName);

                _uploadBook.ImageFile.SaveAs(fileName);
                _uploadBook.pdfFile.SaveAs(pdfName);

                _uploadBook.BookID = Convert.ToInt64(bookID);
                _uploadBook.uploadedBy = Models.currentUser.ID;
                _uploadBook.uploadingDate = System.DateTime.Now.ToString("yyyyMMdd");
                _uploadBook.searchTag += " " + _uploadBook.bookName + " " + _uploadBook.writerName + " " + _uploadBook.bookCatagory;



                Manager.BooksManager DB = new Manager.BooksManager();

                bool a = DB.addBook(_uploadBook);
                if (a==true)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;


        }
    }
}