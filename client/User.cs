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
        public String[] GameList { get; set; }
        public String[] FollowingList { get; set; }
    }
}
