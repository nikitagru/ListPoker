using ListPoker.Controller;
using ListPoker.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ListPoker
{
    public partial class Distribute : Form
    {
        List<Player> players = new List<Player>();
        Label lastPlayerText;
        TextBox lastPlayerTextBox;
        private int playersCount { get; set; }
        public Distribute(int players)
        {
            InitializeComponent();
            this.playersCount = players;
        }

        private void Distribute_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < playersCount; i++)
            {
                DistributeController controller = new DistributeController();
                var playerNameString = controller.drawDistribuion(i, MainFont.font);
                
                lastPlayerText = playerNameString.Item1;
                lastPlayerTextBox = playerNameString.Item2;

                this.Controls.Add(playerNameString.Item1);
                this.Controls.Add(playerNameString.Item2);
            }
            DrawDistributionHelp(lastPlayerTextBox.Location);

            Button select = new Button();
            select.Location = new Point(lastPlayerText.Location.X + 200 + 300, lastPlayerText.Location.Y + 50);
            select.Text = "Далее";
            this.Controls.Add(select);

            Button help = new Button();
            
        }

        private void DrawDistributionHelp(Point nameTextBox)
        {
            Label helpText = new Label();
            helpText.Text = "Раздающим является тот, \nкому выпал туз на жеребьевке ";
            helpText.Font = MainFont.font;
            helpText.Location = new Point(nameTextBox.X + 100 + 50, this.Padding.Top + 30);
            helpText.Size = new Size(200, 100);
            this.Controls.Add(helpText);
        }
    }
}
