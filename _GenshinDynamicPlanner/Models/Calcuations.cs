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
    public static class ExtensionMethods
    {
        public static int RoundOff(this int i)
        {
            return ((int)Math.Round(i / 10.0)) * 10;
        }
    }

    class Calcuations
    {

        //Resets whole 'needed' column in _genshindynamicplanner_usersavedinventorydata to '0' per user
        private void resetNeededInventoryData(int userID)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["_db"].ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = @"UPDATE _genshindynamicplanner_usersavedinventorydata SET needed=0 WHERE userID=@userID;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.ExecuteNonQuery();
                Console.WriteLine("[SQL Query Executed] User Inventory Item Count Updated...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }



        //[EXP TABLE - ESTIMATIONS FROM https://genshin-impact.fandom.com/wiki/Character_EXP]
        //RANGES
        //LVL RANGE -> #_Purple
        //1-20 -> 6
        //20-40 -> 29
        //40-50 -> 29
        //50-60 -> 43
        //60-70 -> 60
        //70-80 -> 81
        //80-90 -> 171
        int[] RangesEXPTablePurples = new int[]
        {
            6, 29, 29, 43, 60, 81, 171
        };

        public int calcRangesEXPTablePurples(int level, int desiredLevel)
        {
            int numPurples = 0;
            int levelRounded = (int)level.RoundOff() + 1;
            int desiredLevelRounded = (int)desiredLevel.RoundOff();

            Console.WriteLine("Level = " + level + "  |  Level Rounded = " + levelRounded);
            Console.WriteLine("desiredLevel = " + desiredLevel + "  |  Desired Level Rounded = " + desiredLevelRounded);

            //for (int i=levelRounded/10; i<desiredLevelRounded/10; i++)
            while (levelRounded < desiredLevel)
            {
                if (levelRounded == 90)
                {
                    Console.WriteLine("numPurples == " + numPurples);
                    return numPurples;
                }
                //
                if (levelRounded <= 20)
                {
                    Console.WriteLine("test: levelRounded = " + levelRounded + "  |  desiredLevelRounded = " + desiredLevelRounded);
                    numPurples += 6;
                    levelRounded = 21;
                }
                else if (levelRounded >= 21 && levelRounded <= 40)
                {
                    numPurples += 29;
                    levelRounded = 41;
                }
                else if (levelRounded >= 41 && levelRounded <= 50)
                {
                    numPurples += 29;
                    levelRounded = 51;
                }
                else if (levelRounded >= 51 && levelRounded <= 60)
                {
                    numPurples += 43;
                    levelRounded = 61;
                }
                else if (levelRounded >= 61 && levelRounded <= 70)
                {
                    numPurples += 60;
                    levelRounded = 71;
                }
                else if (levelRounded >= 71 && levelRounded <= 79)
                {
                    numPurples += 81;
                    levelRounded = 81;
                }
                else if (levelRounded >= 80 && levelRounded <= 90)
                {
                    numPurples += 171;
                    levelRounded = 90;
                }
            }


            Console.WriteLine("numPurples == " + numPurples);
            //vv dummy case numPurples == 0;
            return numPurples;
        }

        public int[] calcEXPBookDistribution(int numPurplesNeeded, int invPurples, int invBlues, int invGreens)
        {
            int[] bookArray = new int[0];

            if (invPurples >= numPurplesNeeded)
            {
                bookArray = bookArray.Concat(new int[] { 1, 3, invPurples, 2, invBlues, 3, invGreens }).ToArray();
            }
            else if (numPurplesNeeded > invPurples)
            {
                //case2
                int c2invBlues = invBlues;
                int c2invPurples = invPurples;
                c2invPurples += (c2invBlues / 3);
                c2invBlues %= 3;
                if (c2invPurples >= numPurplesNeeded)
                {
                    bookArray = bookArray.Concat(new int[] { 2, 3, c2invPurples, 2, c2invBlues, 3, invGreens }).ToArray();
                }

                //case3
                invBlues += (invGreens / 3);
                invGreens %= 3;
                invPurples += (invBlues / 3);
                invBlues %= 3;

                if (invPurples > numPurplesNeeded)
                {
                    bookArray = bookArray.Concat(new int[] { 3, 3, invPurples, 2, invBlues, 3, invGreens }).ToArray();
                }
                else
                {
                    bookArray = bookArray.Concat(new int[] { 0, 3, invPurples, 2, invBlues, 3, invGreens }).ToArray();
                }
            }

            //bookArray = bookArray.Concat(new int[] { [FLAG], 3, invPurples, 2, invBlues, 3, invGreens }).ToArray();
            //[FLAG] || 0==NotEnoughPurples, 1==EnoughPurples, 2==EnoughPurples w/ Purple+Blue, 3==EnoughPurples using all books.
            Console.WriteLine("bookArray = = = " + string.Join(", ", bookArray));
            return bookArray;
        }


        //[Full EXP Table]
        //{This table is not complete and no online resources have a completed version...}
        //{Application will be updated in the future to use a completed full EXP table}
        //https://www.gensh.in/info-sheets/experience-table
        int[] FullEXPTable = new int[]
        {1000,  //1
        1325,
        1700,
        2150,
        2625,
        3150,
        3725,
        4350,
        5000,  //9->10
        5700,
        6450,
        7225,
        8050,
        8925,
        9825,
        10750,
        11725,
        12725,
        13775,  //19->20
        14875,
        16800,
        18000,
        19250,
        20550,
        21875,
        23250,
        24650,
        26100,
        27575,  //29->30
        29100,
        30650,
        32250,
        33875,
        35550,
        37250,
        38975,
        40750,
        42575,
        44425,  //39->40
        46300,
        50625,
        52700,
        54775,
        56900,
        59075,
        61275,
        63525,
        65800,
        69125,  //49->50
        70475,
        76500,
        79050,
        81650,
        84275,
        86950,
        89650,
        92400,
        95175,
        98000,  //59->60
        100875,
        108950,
        112050,
        115175,
        118325,
        121525,
        124775,
        128075,
        131400,
        134775,  //69->70
        138175,
        148700,
        152375,
        156075,
        159825,
        163600,
        167425,
        171300,
        175225,
        179175,  //79->80
        183175};

        private int start(int level, int desiredLevel)
        {
            if (level == desiredLevel)
            {
                //return;
            }
            return start(level+1, desiredLevel);

        }




        //int starRating,
        private int[] calculateCharacterMaterials (int level, int desiredLevel, int ascElementalMatID, int ascElementalBossMat, int ascRegionalMat, int ascCommonMat)
        {
            //[Array]
            int[] data = { 1 };
            //int[0] = ID
            //int[1] = #have
            //int[2] = #needed

            int loopCount = (desiredLevel - level) / 10;

            //Calculate EXP [Book Item ID's -> 1(1k), 2(5k), 3(20k)]
            /*for (int i=0; i<loopCount; i++)
            {


            }
            




            */
            return data;
        }

        //calculateCharacterMaterials


        private int[] calculateMaterials(string type, int starRating, int level, int desiredLevel, int materialID, int r1mat, int r2mat, int r3mat, int r4mat)
        {
            //int total = 0;

            int[] data = {1};


            if (type == "char")
            {

            }
            if (type == "wep")
            {

            }
            if (type == "talent")
            {

            }

            //boom boom functino go here



            //loop to check if item already exists in array?






            return data;
        }

        






    }
}
