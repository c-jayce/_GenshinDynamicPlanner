using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace _GenshinDynamicPlanner.Models
{
    class UserSavedInventoryData : dbItem
    {
        public int rowID;
        //userID exists in User.cs
        //itemID exists in Item.cs
        public int count;


        public ArrayList userInvList = new ArrayList();


        //[Create/Update User Inventory Record]
        //Called on User Login
        public void updateUserInvData(int userID)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"INSERT IGNORE INTO _genshindynamicplanner_usersavedinventorydata (userID, itemID, count, needed)
                        SELECT @userID, ID, 0, 0 FROM _genshindynamicplanner_db_items t1 WHERE NOT EXISTS
                        (SELECT itemID FROM _genshindynamicplanner_usersavedinventorydata t2 WHERE t2.itemID = t1.ID AND userID = @userID);";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.ExecuteNonQuery();
                Console.WriteLine("[SQL Query Executed] User Inventory Records Updated w/ Items DB...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }





        //[Get User's Record of Items as ArrayList]
        //Called after updateUserInvData() on User Login
        public ArrayList getUserInventoryData(int userID)
        {
            //ArrayList userInvList = new ArrayList();
            userInvList.Clear();

            DataTable myTable = new DataTable();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"SELECT * FROM _genshindynamicplanner_usersavedinventorydata t1
                        INNER JOIN _genshindynamicplanner_db_items t2 ON t1.itemID = t2.ID
                        INNER JOIN _genshindynamicplanner_dba_itemtype t3 ON t2.itemType = t3.ID
                        INNER JOIN _genshindynamicplanner_dba_obtainmethod t4 ON t2.obtainMethod = t4.ID
                        WHERE userID = @userID;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable);
                Console.WriteLine("Table is ready.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            //Convert the retrieved data to events and save them to the list
            foreach (DataRow row in myTable.Rows)
            {
                //Character newChar = new Character();
                UserSavedInventoryData thisItemRecord = new UserSavedInventoryData();
                //[Item Data Attributes]
                thisItemRecord.itemID = (int)row["itemID"];
                thisItemRecord.name = row["name"].ToString();
                thisItemRecord.desc = row["desc"].ToString();
                thisItemRecord.imgURL = row["imgURL"].ToString();
                //
                //FK Linked in DB
                thisItemRecord.itemType = (int)row["itemType"];
                thisItemRecord.obtainMethod = (int)row["obtainMethod"];
                //
                //Foreign Rows from Triple Inner Join
                thisItemRecord.itemTypeString = row["itemTypeString"].ToString();
                thisItemRecord.obtainMethodString = row["obtainMethodString"].ToString();
                //
                //[UserSaveData Attributes]
                thisItemRecord.rowID = (int)row["rowID"];
                //userID exists in User.cs
                //itemID exists in Item.cs
                thisItemRecord.count = (int)row["count"];

                userInvList.Add(thisItemRecord);
            }

            Console.WriteLine("[userInvList Returned]");
            return userInvList;
        }


        //[Update User Record when User Changes Item Count]
        public void updateUserItemCount(int userID, int itemID, int count)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"UPDATE _genshindynamicplanner_usersavedinventorydata SET count=@count WHERE userID=@userID AND itemID=@itemID;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@itemID", itemID);
                cmd.Parameters.AddWithValue("@count", count);
                cmd.ExecuteNonQuery();
                Console.WriteLine("[SQL Query Executed] User Inventory Item Count Updated...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }


        //[For genCalc | ArrayLists]
        public ArrayList userInvEXPBooks = new ArrayList();
        //[For genCalc | Get # of EXPBooks]
        public ArrayList getUserInvEXPBooks(int userID)
        {
            //ArrayList userInvList = new ArrayList();
            userInvEXPBooks.Clear();

            DataTable myTable = new DataTable();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"SELECT * FROM _genshindynamicplanner_usersavedinventorydata t1
                        INNER JOIN _genshindynamicplanner_db_items t2 ON t1.itemID = t2.ID
                        INNER JOIN _genshindynamicplanner_dba_itemtype t3 ON t2.itemType = t3.ID
                        INNER JOIN _genshindynamicplanner_dba_obtainmethod t4 ON t2.obtainMethod = t4.ID
                        WHERE userID = 1 AND ItemID BETWEEN 1 AND 3;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable);
                Console.WriteLine("Table is ready.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            //Convert the retrieved data to events and save them to the list
            foreach (DataRow row in myTable.Rows)
            {
                //Character newChar = new Character();
                UserSavedInventoryData thisItemRecord = new UserSavedInventoryData();
                //[Item Data Attributes]
                thisItemRecord.itemID = (int)row["itemID"];
                thisItemRecord.name = row["name"].ToString();
                thisItemRecord.desc = row["desc"].ToString();
                thisItemRecord.imgURL = row["imgURL"].ToString();
                //
                //FK Linked in DB
                thisItemRecord.itemType = (int)row["itemType"];
                thisItemRecord.obtainMethod = (int)row["obtainMethod"];
                //
                //Foreign Rows from Triple Inner Join
                thisItemRecord.itemTypeString = row["itemTypeString"].ToString();
                thisItemRecord.obtainMethodString = row["obtainMethodString"].ToString();
                //
                //[UserSaveData Attributes]
                thisItemRecord.rowID = (int)row["rowID"];
                //userID exists in User.cs
                //itemID exists in Item.cs
                thisItemRecord.count = (int)row["count"];

                userInvEXPBooks.Add(thisItemRecord);
            }

            Console.WriteLine("[userInvList Returned]");
            return userInvEXPBooks;
        }


    }
}
