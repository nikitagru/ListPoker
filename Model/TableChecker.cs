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
            foreach(var item in allPlayersChoice)
            {
                var result = 0;
                var sum = 0;
                foreach(var item1 in item.Value)
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
                    
                }

                if (result > item.Key)
                {
                    isCorrect = false;
                }
                return (isCorrect, item.Key);
            }

            return (isCorrect, 0);
        }
    }
}
