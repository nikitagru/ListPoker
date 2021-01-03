using ListPoker.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ListPoker.Controller
{
    class PlayTableController
    {
        public (bool, int) CheckPlayerInput(Dictionary<int, Dictionary<Player, TextBox[]>> allPlayersChoice)
        {
            TableChecker tableChecker = new TableChecker();
            return tableChecker.CheckPlayerInfo(allPlayersChoice);
        }
    }
}
