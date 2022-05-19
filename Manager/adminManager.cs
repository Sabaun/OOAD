using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigitalLibrary.Models;
using System.Data.SqlClient;

namespace DigitalLibrary.Manager
{
    public class adminManager
    {
        String ConnectionString;
        SqlConnection con;
       private Total_user_get_class tur;
       private Getting_Admin ga;
       private Panding_user_get pur;
       private Get_users gur;
        public adminManager()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DigitalLibraryCon"].ConnectionString;
            con = new SqlConnection(ConnectionString);
            tur = new Total_user_get_class();
            ga = new Getting_Admin();
            pur = new Panding_user_get();
            gur = new Get_users();
        }
        
        public AdminDashboardInformation getAdminDeshboard()
        {
            AdminDashboardInformation _admin = new AdminDashboardInformation();
            Manager.BooksManager DB = new Manager.BooksManager();

            _admin.totalBooks = DB.GetTotalBooks();
            _admin.catagoryviseList = DB.CatagoryViseCOunt();
            //Facade pattern
            _admin.AllUsers = tur.getTotalUsers();
            _admin.totalUsers = gur.getusers();
            _admin.totalAdmins = ga.getAdmins();
            _admin.Name = currentUser.name;
            _admin.RegesteredEmail = currentUser.email;
            _admin.LoginTime = currentUser.loginTime;
            _admin.PandingUserUploads = pur.getPandingUserUploads();

            return _admin;



        }
        public List<Models.readBook> getUserUploadedBooks()
        {
            List<Models.readBook> _readBooks = new List<Models.readBook>();
            string query = "select ID,BookName,WriterName ,PublishDate,BookPath,ImagePath,(select CatagoryTitle from CatagoryTable where ID=CatagoryID) as catagoryName from userUploadedBooksTable";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Models.readBook getBook = new Models.readBook();
                getBook.BookID = Convert.ToInt32(reader[0].ToString());
                getBook.bookName = reader[1].ToString();
                getBook.writerName = reader[2].ToString();
                getBook.publishDate = reader[3].ToString();
                getBook.bookPath = reader[4].ToString();
                getBook.imagePath = reader[5].ToString();
                getBook.bookCatagory = reader[6].ToString();


                _readBooks.Add(getBook);

            }

            con.Close();

            return _readBooks;


        }

    }
}