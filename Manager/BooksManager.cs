using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Manager
{
    public class BooksManager
    {
        String ConnectionString;
        SqlConnection con;

        public BooksManager()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DigitalLibraryCon"].ConnectionString;
            con = new SqlConnection(ConnectionString);
        }
    
        public bool addBook(Models.uploadBook _uploadBook)
        {
            string query;
            if (Models.currentUser.isAdmin==1 && HttpContext.Current.Session["IsAdmin"]!=null)
            {
                Upload_Book_by_Admin up_ad = new Upload_Book_by_Admin();
              query =  up_ad.uploaded_Book(_uploadBook);
            }
            else
            {
                Upload_Book_by_user up_ur = new Upload_Book_by_user();
               query= up_ur.uploaded_Book(_uploadBook);
            }
            

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int a=cmd.ExecuteNonQuery();
            con.Close();

            if (a>0)
            {
                return true;
            }
            return false;
        }

        public  bool addCatagory(string name)
        {
            string query = "insert into catagoryTable(CatagoryTitle,isActive) values('" + name + "','1')";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int a=cmd.ExecuteNonQuery();
            con.Close();

            if (a>0)
            {
                return true;
            }
            return false;
        }

        public List<Models.CatagoryModel> getAllCatagoryList()
        {
            Adding_catagory_adapter add_adapter = new Adding_catagory_adapter();
            List<Models.CatagoryModel> _catagoryModelList = add_adapter.Adding_catagories();
            return _catagoryModelList;
        }


        public List<string> CatagoryListTitles()
        {
            List<string> myList = new List<string>();
            string query = "Select CatagoryTitle from CatagoryTable";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                myList.Add(reader[0].ToString());

            }

            con.Close();

            return myList;


        }



        public List<Models.readBook> getBooks()
        {
            List<Models.readBook> _readBooks = new List<Models.readBook>();
            string query = "select ID,BookName,WriterName ,PublishDate,BookPath,ImagePath,(select CatagoryTitle from CatagoryTable where ID=CatagoryID) as catagoryName from BooksTable order by CatagoryID";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Models.readBook getBook = new Models.readBook();
                getBook.BookID = Convert.ToInt32(reader[0].ToString());
                getBook.bookName =reader[1].ToString();
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

        public string getBookByID(int ID)
        {
            string bookPath = "";
            string query = "Select BookPath from BooksTable where ID='"+ID+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                bookPath = reader[0].ToString();

            }

            con.Close();

            return bookPath;
        }

        public string get_userUploaded_BookByID(int ID)
        {
            string bookPath = "";
            string query = "Select BookPath from userUploadedBooksTable where ID='" + ID + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                bookPath = reader[0].ToString();

            }

            con.Close();

            return bookPath;
        }

        public int GetTotalBooks()
        {
            int count = 0;
            string query = "Select Count(*) from BooksTable";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                count = Convert.ToInt32(reader[0].ToString());

            }

            con.Close();

            return count;
        }

        public bool delete_userUploaded_BookByID(int ID)
        {
            string query = "Delete userUploadedBooksTable where ID='"+ID+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            int a = cmd.ExecuteNonQuery();

            if (a>0)
            {
                return true;
            }

            con.Close();

            return false;
        }

        public bool delete_BookByID(int ID)
        {
            string query = "Delete BooksTable where ID='" + ID + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            int a = cmd.ExecuteNonQuery();

            if (a > 0)
            {
                return true;
            }

            con.Close();

            return false;
        }


        public List<string[]> CatagoryViseCOunt()
        {
            List<string[]> titleviseCount = new List<string[]>();
            List<int> catagoriesList = new List<int>();
            string query = "select ID from CatagoryTable";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                catagoriesList.Add(Convert.ToInt32(reader[0].ToString()));

            }

            con.Close();

            
            foreach (int item in catagoriesList)
            {
                con.Open();
                string[] a = new string[2];
                
                query = "Select (select count(CatagoryID) from BooksTable where CatagoryID='" + item + "') , (select CatagoryTitle from CatagoryTable where ID='" + item + "')";
              SqlCommand cmd2 = new SqlCommand(query, con);
               SqlDataReader reader2 = cmd2.ExecuteReader();
                while(reader2.Read())
                {

                    a[0] = reader2[0].ToString();
                    a[1] = reader2[1].ToString();

                }

                titleviseCount.Add(a);
                con.Close();
            }

            

            return titleviseCount;
        }


        public bool moveBook_temp_to_permanent(int ID)
        {


            string query = "INSERT INTO BooksTable(BookID,BookName,BookPath,CatagoryID,ImagePath,PublishDate,UploadedBy,WriterName,UploadingDate,SearchTag) SELECT BookID,BookName,BookPath,CatagoryID,ImagePath,PublishDate,UploadedBy,WriterName,UploadingDate,SearchTag FROM userUploadedBooksTable WHERE userUploadedBooksTable.ID='" + ID+"' Delete userUploadedBooksTable where ID='"+ID+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            int a = cmd.ExecuteNonQuery();

            if (a > 0)
            {
                return true;
            }

            con.Close();

            return false;
        }

        public List<Models.readBook> getBooksBySearchTag(string searchTag)
        {
            List<Models.readBook> _readBooks = new List<Models.readBook>();
            string query = "select ID,BookName,WriterName ,PublishDate,BookPath,ImagePath,(select CatagoryTitle from CatagoryTable where ID=CatagoryID) as catagoryName from BooksTable where SearchTag like '%" + searchTag + "%' order by CatagoryID";
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