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
    class dbCharacter
    {
        //[Character Attributes]
        public int charID;  //DB Attribute in Table -> ID
        public string Name;
		public int starRating;
        public int elementType;
        public int weaponType;

        public string talent1Name;
        public string talent1Desc;
        public string talent2Name;
        public string talent2Desc;
        public string talent3Name;
        public string talent3Desc;

        public string imgURLchar;
        public string imgURLt1;
        public string imgURLt2;
        public string imgURLt3;

        public int ascElementalMat;
        public int ascElementalBossMat;
        public int ascRegionalSpecialty;
        public int ascCommonAndTalentMat;
        public int ascTalentBossMat;

        //ArrayList charList = new ArrayList();


        



    }
}
