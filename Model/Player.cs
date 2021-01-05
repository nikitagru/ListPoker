using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ListPoker
{
    class Player
    {
        public string name { get; set; }
        public int score { get; set; }

       public Player(string name)
        {
            this.name = name;
        }
    }
}
