using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using MySql.Data.MySqlClient;
using System.Configuration;

namespace _GenshinDynamicPlanner.Models
{
    class User
    {
        //[User Variables]
        Boolean isLoggedIn = false;

        //change this back later  vvv
        //public int userID = -1;
        public int userID = 1;  //TESTING

        public string email;

        public string username;
        public string password;
        public string confirmPassword;

        public int adventureRankLevel;


        //[User Class] - CONSTRUCTOR
        public User()
        {

        }



        public Boolean userLogin()
        {
            Console.WriteLine("userID NOT LOGGED IN: -1 == " + userID);
            Console.WriteLine("\n\n\n");
            //Console.WriteLine("email: " + email);
            //Console.WriteLine("firstName: " + firstName);
            //Console.WriteLine("lastName: " + lastName);
            Console.WriteLine("username: " + username);
            Console.WriteLine("password: " + password);
            //Console.WriteLine("confirmPassword: " + confirmPassword);

            //Send SQL Query
            //string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            //MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                //add/alter code for user ID after coding functional login
                string sql = "SELECT * FROM _genshindynamicplanner_users WHERE username=@username AND password=@password";  //QUERY TO SEE IF ACC EXISTS
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                //cmd.Parameters.AddWithValue("@firstName", firstName);
                //cmd.Parameters.AddWithValue("@lastName", lastName);
                //cmd.Parameters.AddWithValue("@username", username);
                //cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();

                Console.WriteLine("executescalar: " + cmd.ExecuteScalar());
                object pulledRecords = cmd.ExecuteScalar();
                Int32 records = 0;  //Initial Value 0
                if (pulledRecords != null)
                {
                    //records = (Int32)cmd.ExecuteScalar();  //Get INT value of # of rows returned...
                    records = Convert.ToInt32(cmd.ExecuteScalar());
                }
                Console.WriteLine("records: " + records);
                if (records == 0)
                {
                    return false;
                }
                
                
                
                //REGISTER USER - INSERRT ROW
                //RESET SQL ADAPTER
                //cmd.Parameters.Clear();  //Clear SQL Statement
                /*conn.Close();
                conn.Open();

                sql = "SELECT * FROM _genshindynamicplanner_users WHERE username=@username AND password=@password";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();*/

                else {

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userID = (int)reader["ID"];
                        adventureRankLevel = (int)reader["adventureRankLevel"];
                        //old
                        //role = reader["role"].ToString();

                    }

                    Console.WriteLine("reader: " + reader);
                    Console.WriteLine("userID: " + userID);
                    Console.WriteLine("adventureRankLevel: " + adventureRankLevel);
                    //Console.WriteLine("role: " + role);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");

            
            isLoggedIn = true;
            return true;


        }









    }
}
