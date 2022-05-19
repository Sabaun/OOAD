using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Web.Mvc;


namespace DigitalLibrary.Manager
{
    public class databaseManager
    {
        String ConnectionString;
        SqlConnection con;

        public databaseManager()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DigitalLibraryCon"].ConnectionString;
            con = new SqlConnection(ConnectionString);
        }

        public bool signup(Models.signUpModel _signUpModel)
        {

            using (DigitalLibraryDBEntities DB = new DigitalLibraryDBEntities())
            {
                loginTable _logintable = new loginTable();
                _logintable.ID = _signUpModel.ID;
                _logintable.Email = _signUpModel.email;
                _logintable.isAdmin = _signUpModel.isAdmin;
                _logintable.userName = _signUpModel.username;
                _logintable.password = _signUpModel.password;
                _logintable.Name = _signUpModel.name;

               
                DB.loginTables.Add(_logintable);
                DB.SaveChanges();

                if (_logintable.ID>0)
                {
                  
                    return true;
                }
                else
                {
                    return false;
                }

            }

            
        }

        public bool updatePassword(Models.signUpModel ss,int ID)
        {

            using (var db = new DigitalLibraryDBEntities())
            {
                var result = db.loginTables.SingleOrDefault(b => b.ID == ID);
                if (result != null)
                {
                    result.password = ss.password;
                    result.Name = ss.name;
                    db.SaveChanges();

                    return true;
                }
            }

           
            return false;
        }
        
        public bool already_Exist_Email_or_userName(Models.signUpModel _signupModel)
        {

              using (DigitalLibraryDBEntities DB = new DigitalLibraryDBEntities())
            {
                var request = DB.loginTables.Where(x => x.Email == _signupModel.email).FirstOrDefault();
                if (request == null)
                {
                    Username_already_exists urs = new Username_already_exists();
                    if (!urs.already_exist_userName(_signupModel))
                    return false;

                }
              
            }


            return true;





        }
       
        public bool sendMail(string email, string userName)
        {
            using (DigitalLibraryDBEntities DB = new DigitalLibraryDBEntities())
            {
                var request = DB.loginTables.Where(x => x.userName == userName).FirstOrDefault();
                if (request != null)
                {
                    if(request.Email==email)
                    {
                        Manager.sendMail sm =  Manager.sendMail.createinstance;
                        string subject = "Account Recovery For Digital Library";
                        string Body = "Your Password is : " + request.password;
                        int a=sm.mail(email,subject, Body );

                        if (a>0)
                        {
                            return true;
                        }
                    }

                }

            }


            return false;
        }



        public bool checkLogin(Models.loginModel _loginModel)
        {


            using (DigitalLibraryDBEntities DB = new DigitalLibraryDBEntities())
            {
                var request = DB.loginTables.Where(x => x.userName == _loginModel.userName).FirstOrDefault();
                
                if (request == null)
                {
                    if (request.password == _loginModel.password)
                    {
                        Models.currentUser.ID = request.ID;
                        Models.currentUser.isAdmin = Convert.ToInt32(request.isAdmin);
                        Models.currentUser.email = request.Email;
                        Models.currentUser.name = request.Name;
                        Models.currentUser.username = request.userName;
                        Models.currentUser.password = request.password;
                        Models.currentUser.loginTime = System.DateTime.Now.ToShortTimeString();
                        

                        return true;
                    }

                }
            }


            return false;




        }

        public bool makeAdmin(int userID)
        {
            using (var db = new DigitalLibraryDBEntities())
            {
                var result = db.loginTables.SingleOrDefault(b => b.ID == userID);
                if (result != null)
                {
                    result.isAdmin = 1;
                    db.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        public bool removeAdmin(int userID)
        {
            using (var db = new DigitalLibraryDBEntities())
            {
                var result = db.loginTables.SingleOrDefault(b => b.ID == userID);
                if (result != null)
                {
                    result.isAdmin = 0;
                    db.SaveChanges();

                    return true;
                }
            }


            return false;
         
        }

        public bool deleteAUser(int userID)
        {

            using (DigitalLibraryDBEntities Context = new DigitalLibraryDBEntities())
            {
                loginTable Delete = Context.loginTables.Find(userID);
                Context.loginTables.Remove(Delete);
                Context.SaveChanges();
                return true;
            }

            
            return false;

        }

        public List<Models.signUpModel> getAllUsers()
        {


                List<Models.signUpModel> list = new List<Models.signUpModel>();
            string query = "select * from loginTable";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Models.signUpModel sp = new Models.signUpModel();

                sp.ID = Convert.ToInt32(reader[0].ToString());
                sp.name = reader[1].ToString();
                sp.email = reader[2].ToString();
                sp.username = reader[3].ToString();
                sp.password = reader[4].ToString();
                sp.isAdmin = Convert.ToByte(reader[5].ToString());

                list.Add(sp);
                

            }
            con.Close();

            return list;
        }



    }
}