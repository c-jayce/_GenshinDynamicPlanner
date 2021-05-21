using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GenshinDynamicPlanner.Models
{
    class dbItem
    {
        //[Item Attributes]
        public int itemID;
        public string name;
        public string desc;
        public string imgURL;

        //FK Linked in DB
        public int itemType;  //1
        public int obtainMethod;  //2

        //Foreign Rows from Triple Inner Join
        public string itemTypeString;  //1
        public string obtainMethodString;  //2

    }
}
