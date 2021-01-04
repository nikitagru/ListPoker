using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ListPoker.Model
{
    class TableChecker
    {
        public (bool, int) CheckPlayerInfo(Dictionary<int, Dictionary<Player, TextBox[]>> allPlayersChoice)
        {
            var isCorrect = true;
            var correctStep = 0;
            foreach(var item in allPlayersChoice)
            {
                var result = 0;
                var sum = 0;
                var playersGets = 0;
                foreach (var item1 in item.Value)
                {
                    for (var i = 0; i < 2; i++)
                    {
                        bool success = int.TryParse(item1.Value[i].Text, out sum);
                        if (success)
                        {
                            result += int.Parse(item1.Value[i].Text);
                            break;
                        }
                        
                    }
                    
                    var currentPlayerGet = 0;
                    var isNotEmpty = int.TryParse(item1.Value[2].Text, out currentPlayerGet);
                    if (isNotEmpty)
                    {
                        playersGets += int.Parse(item1.Value[2].Text);
                    }
                }



                if (result == item.Key || playersGets > item.Key)
                {
                    isCorrect = false;
                    return (isCorrect, item.Key);   
                } else
                {
                    correctStep = item.Key;
                }
            }
            return (isCorrect, correctStep);
            
        }
    }
}
