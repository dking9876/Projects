using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Player
    {
        public string UserName { get; set; }
        public int Password { get; set; }
        public List<Game> GameList { get; set; }


    }
}
