using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Manager
{
    public class DeshboardManager
    {
        String ConnectionString;
        SqlConnection con;

        public DeshboardManager()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DigitalLibraryCon"].ConnectionString;
            con = new SqlConnection(ConnectionString);
        }

        public List<Models.CatagoryModel> getActiveCatagoryList()
        {
            List<Models.CatagoryModel> _catagoryModelList = new List<Models.CatagoryModel>();
            string query = "Select * from CatagoryTable where isActive=1";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Models.CatagoryModel _catagoryModel = new Models.CatagoryModel();
                _catagoryModel.ID = Convert.ToInt32(reader[0].ToString());
                _catagoryModel.catagoryName = reader[1].ToString();

                _catagoryModelList.Add(_catagoryModel);

            }

            con.Close();

            return _catagoryModelList;


        }



        public List<Models.readBook> getBooks(int ID)
        {
            List<Models.readBook> _readBooks = new List<Models.readBook>();
            string query = "select ID,BookName,WriterName ,PublishDate,BookPath,ImagePath,(select CatagoryTitle from CatagoryTable where ID=CatagoryID) as catagoryName from BooksTable where CatagoryID='"+ID+"'";
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


        public string CatagoryTitleByID(int ID)
        {
            string catagory = "";
            string query = "Select CatagoryTitle from CatagoryTable where ID='"+ID+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                catagory=reader[0].ToString();

            }

            con.Close();

            return catagory;


        }
    }
}