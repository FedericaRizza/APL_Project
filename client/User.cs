using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    internal class User
    {
        public int UserID { get; set; }
        public String Nick { get; set; }
        public IList<String> GameList { get; set; }
        public IDictionary<int, String> FollowingList { get; set; }
        public IList<String> ChatList { get; set; }
        public IDictionary<String, IList<String>> SharedGames { get; set;}
    }    
}
