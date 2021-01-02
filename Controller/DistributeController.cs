using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ListPoker.Controller
{
    class DistributeController
    {
        public (Label, TextBox) drawDistribuion(int i, Font font)
        {
            Label label = new Label();
            label.Text = i == 0 ? "Игрок (раздающий) " + (i + 1) : "Игрок " + (i + 1);
            label.Size = new Size(200, 30);
            label.Font = font;
            label.Location = new Point(20, 30 + i * 35);

            TextBox playerName = new TextBox();
            playerName.Location = new Point(label.Location.X + 200 + 50, label.Location.Y + 5);
            playerName.Size = new Size(100, 10);

            var playerNameString = (label, playerName);

            return playerNameString;
        }
    }
}
