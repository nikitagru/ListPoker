using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ListPoker.View
{
    public partial class PlayTable : Form
    {
        List<Player> players = new List<Player>();
        public PlayTable(string playerNames)
        {
            var nameList = playerNames.Split(" ").ToList();
            for (var i = 0; i < nameList.Count; i++)
            {
                players.Add(new Player(nameList[i]));
            }
            InitializeComponent();
            MakeMainTable();
        }

        private void MakeMainTable()
        {
            PictureBox pic = DrawLine(250 + players.Count * 280, 1, new Point(0, 30));
            PictureBox pic1 = DrawLine(250 + players.Count * 280, 1, new Point(0, pic.Location.Y + 80));
            this.Controls.Add(pic);
            this.Controls.Add(pic1);
            DrawVertLines();
            DrawHorLines();
            DrawPlayerInfoTable();
        }

        private PictureBox DrawLine(int width, int height, Point location)
        {
            PictureBox pic = new PictureBox();
            pic.BackColor = Color.Black;
            pic.Location = location;
            pic.Width = width;
            pic.Height = height;
            return pic;
        }

        private void DrawVertLines()
        {
            var maxCards = 36 / players.Count;
            PictureBox vertLine = DrawLine(1, 110 + ((maxCards - 1) * 2 + players.Count * 2) * 50, new Point(100, 0));
            PictureBox vertLine1 = DrawLine(1, 110 + ((maxCards - 1) * 2 + players.Count * 2) * 50, new Point(vertLine.Location.X + 150, 0));
            this.Controls.Add(vertLine);
            this.Controls.Add(vertLine1);
            for (var i = 1; i <= players.Count; i++)
            {
                PictureBox pic = DrawLine(1, 110 + ((maxCards - 1) * 2 + players.Count * 2) * 50, new Point(vertLine1.Location.X + (i * 280)));
                this.Controls.Add(pic);
            }
        }

        private void DrawHorLines()
        {
            var maxCards = 36 / players.Count;
            for (var i = 0; i < (maxCards - 1) * 2 + players.Count * 2; i++)
            {
                PictureBox pic = DrawLine(250 + players.Count * 280, 1, new Point(0, i * 50 + 110));
                this.Controls.Add(pic);
            }
        }

        private void DrawPlayerInfoTable()
        {
            for (var i = 0; i < players.Count; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    PictureBox pic = DrawLine(1, 80, new Point(250 + i * 280 + j * 70, 30));
                    this.Controls.Add(pic);
                }
            }
        }
    }
}
