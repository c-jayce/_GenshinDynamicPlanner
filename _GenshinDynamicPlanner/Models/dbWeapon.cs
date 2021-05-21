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
    class dbWeapon
    {
        //[Weapon Attributes]
        public int wepID;  //DB Attribute in Table -> ID
        public string name;
        public int starRating;
        public int wepType;
        //
        public int baseStats;
        public int subStat;
        public string wepDesc;
        public string passiveName;
        public string passiveDesc;
        public int obtainMethod;
        //
        public int ascMaterialWep;
        public int ascMatCommon1;
        public int ascMatCommon2;
        //
        public string imgURL;
        //
        //
        public ArrayList dbWepList = new ArrayList();  //ArrayList for dbWeapons (Multi)
        public ArrayList userCharWeaponData = new ArrayList();

        public ArrayList getdbWeaponsList()
        {
            dbWepList.Clear();

            DataTable myTable = new DataTable();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"SELECT * FROM _genshindynamicplanner_db_weapons;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("@userID", userID);
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
                dbWeapon thisWeaponRecord = new dbWeapon();
                //[Weapon Data Attributes]
                thisWeaponRecord.wepID = (int)row["ID"];
                thisWeaponRecord.name = row["name"].ToString();
                thisWeaponRecord.starRating = (int)row["starRating"];
                thisWeaponRecord.wepType = (int)row["wepType"];
                //
                thisWeaponRecord.baseStats = (int)row["baseStats"];
                thisWeaponRecord.subStat = (int)row["subStat"];
                thisWeaponRecord.wepDesc = row["wepDesc"].ToString();
                thisWeaponRecord.passiveName = row["passiveName"].ToString();
                thisWeaponRecord.passiveDesc = row["passiveDesc"].ToString();
                thisWeaponRecord.obtainMethod = (int)row["obtainMethod"];
                //
                thisWeaponRecord.ascMaterialWep = (int)row["ascMaterialWep"];
                thisWeaponRecord.ascMatCommon1 = (int)row["ascMatCommon1"];
                thisWeaponRecord.ascMatCommon2 = (int)row["ascMatCommon2"];
                //
                thisWeaponRecord.imgURL = row["imgURL"].ToString();

                dbWepList.Add(thisWeaponRecord);
            }

            Console.WriteLine("[dbWepList Returned]");
            return dbWepList;
        }


        public ArrayList getWeaponData(int wepID)
        {
            //ArrayList userCharList = new ArrayList();
            userCharWeaponData.Clear();

            DataTable myTable = new DataTable();
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"SELECT * FROM _genshindynamicplanner_db_weapons WHERE ID = @wepID;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@wepID", wepID);
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
                dbWeapon thisWeaponRecord = new dbWeapon();
                //[Weapon Data Attributes]
                thisWeaponRecord.name = row["name"].ToString();
                thisWeaponRecord.imgURL = row["imgURL"].ToString();

                userCharWeaponData.Add(thisWeaponRecord);
            }

            Console.WriteLine("[WeaponData/[dbWeapons]/(userCharWeaponData) Returned]");
            return userCharWeaponData;
        }



    }
}
