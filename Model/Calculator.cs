using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ListPoker.Model
{
    class Calculator
    {
        public (int, List<int>) CalculateScore(Dictionary<int, Dictionary<Player, TextBox[]>> allPlayersChoice, Dictionary<int, List<Label>> playersResults)
        {
            List<int> results = new List<int>();
            foreach (var item in allPlayersChoice)
            {
                foreach (var item1 in item.Value)
                {
                    if ((item1.Value[0].Text != "" || item1.Value[1].Text != "") && item1.Value[2].Text != "")
                    {
                        var playerWish = 0;
                        for (var i = 0; i < 2; i++)
                        {
                            bool success = int.TryParse(item1.Value[i].Text, out playerWish);
                            if (success)
                            {
                                break;
                            }
                        }
                        int playerGets;
                        var isNotEmpty = int.TryParse(item1.Value[2].Text, out playerGets);
                        if (isNotEmpty)
                        {
                            if (playerWish == playerGets)
                            {
                                results.Add(playerGets * 10);
                            }
                            else if (playerWish > playerGets)
                            {
                                results.Add((playerWish - playerGets) * 10);
                            }
                            else if (playerWish == 0 && playerGets == 0)
                            {
                                results.Add(5);
                            }
                            else
                            {
                                results.Add(playerGets * 2);
                            }
                        }
                    } else
                    {
                        break;
                    }
                }
                var isCurrentStep = false;
                for (var i = 0; i < playersResults[item.Key].Count; i++)
                {
                    if (playersResults[item.Key][i].Text != "")
                    {
                        isCurrentStep = false;
                        break;
                    } else
                    {
                        isCurrentStep = true;
                    }
                }
                if (isCurrentStep && results.Count == item.Value.Count)
                {
                    if (item.Key == 2)
                    {
                        Console.WriteLine();
                    }
                    return (item.Key, results);
                }
                results.Clear();
            }
            return (0, results);
        }
    }
}
