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
    class UserSavedCharacterData : dbCharacter
    {
        //[UserSaveData Attributes (Records)]
        public int rowID;
        //userID exists in User.cs
        //charID exists in Character.cs
        public int charLevel;
        public int t1Level;
        public int t2Level;
        public int t3Level;
        public int wepID;  //Another function to get a table of weapons to be displayed
        public int wepLevel;
        public int ArtifactPlaceholder;
        //toggleCalc Flag Value: 0 is off, 1 is on... (INT boolean)
        public int toggleCalc;
        public int charDesiredLevel;
        public int t1DesiredLevel;
        public int t2DesiredLevel;
        public int t3DesiredLevel;
        public int wepDesiredLevel;

        public ArrayList userCharList = new ArrayList();


        //[Query DB -> Retrieve Whole List]
        public ArrayList getUserCharacterData(int userID)
        {
            //ArrayList userCharList = new ArrayList();
            userCharList.Clear();

            DataTable myTable = new DataTable();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"SELECT * FROM _genshindynamicplanner_usersavedcharacterdata t1
                                INNER JOIN _genshindynamicplanner_db_characters t2 ON t1.charID = t2.ID
                                INNER JOIN _genshindynamicplanner_db_weapons t3 ON t1.wepID = t3.ID
										  WHERE userID = 1 ORDER BY rowID ASC;";
                /*string sql = @"SELECT * FROM _genshindynamicplanner_usersavedcharacterdata t1
                                INNER JOIN _genshindynamicplanner_db_characters t2 ON t1.charID = t2.ID
                                WHERE userID = @userID ORDER BY rowID ASC;";*/
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
                UserSavedCharacterData thisCharRecord = new UserSavedCharacterData();
                //[Character Data Attributes]
                thisCharRecord.charID = (int)row["ID"];
                thisCharRecord.Name = row["name"].ToString();
                thisCharRecord.starRating = (int)row["starRating"];
                thisCharRecord.elementType = (int)row["elementType"];
                thisCharRecord.weaponType = (int)row["weaponType"];
                //
                thisCharRecord.talent1Name = row["talent1Name"].ToString();
                thisCharRecord.talent1Desc = row["talent1Desc"].ToString();
                thisCharRecord.talent2Name = row["talent2Name"].ToString();
                thisCharRecord.talent2Desc = row["talent2Desc"].ToString();
                thisCharRecord.talent3Name = row["talent3Name"].ToString();
                thisCharRecord.talent3Desc = row["talent3Desc"].ToString();
                //
                thisCharRecord.imgURLchar = row["imgURLchar"].ToString();
                thisCharRecord.imgURLt1 = row["imgURLt1"].ToString();
                thisCharRecord.imgURLt2 = row["imgURLt2"].ToString();
                thisCharRecord.imgURLt3 = row["imgURLt3"].ToString();
                //
                thisCharRecord.ascElementalMat = (int)row["ascElementalMat"];
                thisCharRecord.ascElementalBossMat = (int)row["ascElementalBossMat"];
                thisCharRecord.ascRegionalSpecialty = (int)row["ascRegionalSpecialty"];
                thisCharRecord.ascCommonAndTalentMat = (int)row["ascCommonAndTalentMat"];
                thisCharRecord.ascTalentBossMat = (int)row["ascTalentBossMat"];
                //
                //
                //Records Joined Information:
                //[UserSaveData Attributes]
                thisCharRecord.charLevel = (int)row["charLevel"];
                thisCharRecord.t1Level = (int)row["t1Level"];
                thisCharRecord.t2Level = (int)row["t2Level"];
                thisCharRecord.t3Level = (int)row["t3Level"];
                thisCharRecord.wepID = (int)row["wepID"];
                thisCharRecord.wepLevel = (int)row["wepLevel"];
                thisCharRecord.ArtifactPlaceholder = (int)row["ArtifactPlaceholder"];
                //
                //toggleCalc Flag Value
                thisCharRecord.toggleCalc = (int)row["toggleCalc"];
                //DesiredLevels | Calc
                thisCharRecord.charDesiredLevel = (int)row["charDesiredLevel"];
                thisCharRecord.t1DesiredLevel = (int)row["t1DesiredLevel"];
                thisCharRecord.t2DesiredLevel = (int)row["t2DesiredLevel"];
                thisCharRecord.t3DesiredLevel = (int)row["t3DesiredLevel"];
                thisCharRecord.wepDesiredLevel = (int)row["wepDesiredLevel"];

                userCharList.Add(thisCharRecord);
            }

            Console.WriteLine("[userCharList Returned]");
            return userCharList;
        }


        public ArrayList addNewCharList = new ArrayList();
        //Query For Adding New Chars
        public ArrayList getUserAddNewCharList(int userID)
        {
            //ArrayList addNewCharList = new ArrayList();
            addNewCharList.Clear();  //Clear ArrayList Before Doing


            DataTable myTable = new DataTable();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"SELECT * FROM _genshindynamicplanner_db_characters t1 WHERE NOT EXISTS
                                (SELECT charID FROM _genshindynamicplanner_usersavedcharacterdata t2
                                WHERE t1.ID = t2.charID AND userID = @userID) ORDER BY t1.name ASC;";
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
                UserSavedCharacterData thisCharRecord = new UserSavedCharacterData();
                //[Character Data Attributes]
                thisCharRecord.charID = (int)row["ID"];
                thisCharRecord.Name = row["name"].ToString();

                addNewCharList.Add(thisCharRecord);
            }

            Console.WriteLine("[addNewCharList Returned]");
            return addNewCharList;
        }


        public void addCharRecord(int userID, int charID)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"INSERT IGNORE INTO _genshindynamicplanner_usersavedcharacterdata (userID, charID, wepID)
                                VALUES (@userID, @charID, -1);";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@charID", charID);
                cmd.ExecuteNonQuery();
                Console.WriteLine("[SQL Query Executed] User Character Records Updated w/ New Character...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }


        public void delCharRecord(int userID, int charID)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"DELETE FROM _genshindynamicplanner_usersavedcharacterdata
                                WHERE userID=@userID AND charID=@charID;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@charID", charID);
                cmd.ExecuteNonQuery();
                Console.WriteLine("[SQL Query Executed] User Character Records Updated w/ New Character...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }


        //toggleCalc Switched -> Value Update
        public void updateToggleCalc(int userID, int charID, int toggleCalc)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"UPDATE _genshindynamicplanner_usersavedcharacterdata
                                SET toggleCalc=@toggleCalc WHERE userID=@userID AND charID=@charID;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@charID", charID);
                cmd.Parameters.AddWithValue("@toggleCalc", toggleCalc);
                cmd.ExecuteNonQuery();
                Console.WriteLine("[SQL Query Executed] toggleCalc Flag Updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }


        //[add Weapon]
        public void addCharWeaponRecord(int userID, int charID, int wepID)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"UPDATE _genshindynamicplanner_usersavedcharacterdata
                                SET wepID=@wepID WHERE userID=@userID AND charID=@charID;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@charID", charID);
                cmd.Parameters.AddWithValue("@wepID", wepID);
                cmd.ExecuteNonQuery();
                Console.WriteLine("[SQL Query Executed] User Character Records Updated w/ New Character Weapon Record...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }


        //[del Weapon]
        public void delCharWeaponRecord(int userID, int charID)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"UPDATE _genshindynamicplanner_usersavedcharacterdata
                                SET wepID=@wepID WHERE userID=@userID AND charID=@charID;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@charID", charID);
                cmd.Parameters.AddWithValue("@wepID", -1);
                cmd.ExecuteNonQuery();
                Console.WriteLine("[SQL Query Executed] User Character Records Updated w/ New Character Weapon Record...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }


        //[Update (Recorded) Levels]
        //int not void to return error handling flags
        public int updateLevel(int userID, int charID, string colNameLevel, string colNameDesiredLevel, int newLevel, int newDesiredLevel)
        {
            if (newLevel > newDesiredLevel)
            {
                return 0;
            }
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                /*string sql = @"UPDATE _genshindynamicplanner_usersavedcharacterdata
                                SET @colNameLevel=@newLevel, @colNameDesiredLevel=@newDesiredLevel
                                WHERE userID=@userID AND charID=@charID;";*/
                string sql = "UPDATE _genshindynamicplanner_usersavedcharacterdata SET " + colNameLevel + "=@newLevel, " + colNameDesiredLevel + "=@newDesiredLevel WHERE userID=@userID AND charID=@charID;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@charID", charID);
                //cmd.Parameters.AddWithValue("@colNameLevel", colNameLevel);
                cmd.Parameters.AddWithValue("@newLevel", newLevel);
                //cmd.Parameters.AddWithValue("@colNameDesiredLevel", colNameDesiredLevel);
                cmd.Parameters.AddWithValue("@newDesiredLevel", newDesiredLevel);
                cmd.ExecuteNonQuery();
                Console.WriteLine("[SQL Query Executed] UserCharRecord Level/DesiredLevel Updated...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            
            return 1;
        }
    }
}
