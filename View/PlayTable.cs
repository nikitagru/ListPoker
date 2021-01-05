﻿using ListPoker.Controller;
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
        // key - current step, value - player and his choice
        Dictionary<int, Dictionary<Player, TextBox[]>> allPlayersChoice = new Dictionary<int, Dictionary<Player, TextBox[]>>();
        Dictionary<int, List<Label>> playersResults = new Dictionary<int, List<Label>>();
        
        string[] playerInfo = new string[] { "заказ", "темная", "взятка", "итого" };
        Brush br;
        
        int currentStep;
        public PlayTable(string playerNames)
        {
            var nameList = playerNames.Split(" ").ToList();
            for (var i = 0; i < nameList.Count; i++)
            {
                players.Add(new Player(nameList[i]));
            }
            
            InitializeComponent();
            MakeTable();
            DrawPlayerNames();
            DrawDistributor();
            DrawCardCount();
            DrawPlayerInfo();
            CreateRoundArea();
            CreateResultLabels();
            timer1.Interval = 10000;
            timer1.Start();
            timer1.Tick += Update;
        }

        private void Update(object sender, EventArgs e)
        {
            this.Invalidate();
            PlayTableController tableController = new PlayTableController();
            (bool, int) isCorrectUserInput = tableController.CheckPlayerInput(allPlayersChoice);
            if (!isCorrectUserInput.Item1)
            {
                br = new SolidBrush(Color.FromArgb(184, 81, 81));
                currentStep = isCorrectUserInput.Item2;
                this.Paint += Form1_Paint;
            } else
            {
                currentStep = -10;
                this.Paint += Form1_Paint;
                (int, List<int>) results = tableController.CalculatePlayersScore(allPlayersChoice, playersResults);
                ShowResults(results);
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (br != null)
            {
                Rectangle rec = new Rectangle(new Point(0, TableInfo.firstRowHeight + TableInfo.secondRowHeight + (currentStep - 1) * TableInfo.roundRowHeight), 
                    new Size(
                    TableInfo.firstColumnWidth + TableInfo.secondColumnWidth + TableInfo.playerColumnWidth * players.Count,
                    TableInfo.roundRowHeight));
                
                e.Graphics.FillRectangle(br, rec);
            }
        }

        private void MakeTable()
        {
            PictureBox pic = DrawLine(TableInfo.firstColumnWidth + TableInfo.secondColumnWidth + players.Count * TableInfo.playerColumnWidth, 
                                        1, new Point(0, TableInfo.firstRowHeight));
            
            PictureBox pic1 = DrawLine(TableInfo.firstColumnWidth + TableInfo.secondColumnWidth + players.Count * TableInfo.playerColumnWidth, 
                                        1, new Point(0, pic.Location.Y + TableInfo.secondRowHeight));
            
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
            PictureBox vertLine = DrawLine(1, TableInfo.firstRowHeight + TableInfo.secondRowHeight + ((maxCards - 1) * 2 + players.Count * 2) * TableInfo.roundRowHeight, 
                                                                            new Point(TableInfo.firstColumnWidth, 0));
            Label label = new Label();
            label.Text = "Раунд";
            label.Location = new Point(vertLine.Location.X - 85, TableInfo.firstRowHeight);
            label.Size = new Size(80, 29);
            label.Font = MainFont.font;
            this.Controls.Add(label);
            
            PictureBox vertLine1 = DrawLine(1, TableInfo.firstRowHeight + TableInfo.secondRowHeight + ((maxCards - 1) * 2 + players.Count * 2) * TableInfo.roundRowHeight, 
                                                                            new Point(vertLine.Location.X + TableInfo.secondColumnWidth, 0));
            Label label1 = new Label();
            label1.Text = "Раздающий";
            label1.Location = new Point(vertLine1.Location.X - 130, TableInfo.firstRowHeight);
            label1.Size = new Size(129, 29);
            label1.Font = MainFont.font;
            this.Controls.Add(label1);



            this.Controls.Add(vertLine);
            this.Controls.Add(vertLine1);
            for (var i = 1; i <= players.Count; i++)
            {
                PictureBox pic = DrawLine(2, TableInfo.firstRowHeight + TableInfo.secondRowHeight + ((maxCards - 1) * 2 + players.Count * 2) * TableInfo.roundRowHeight, 
                                                                        new Point(vertLine1.Location.X + (i * TableInfo.playerColumnWidth), 0));
                this.Controls.Add(pic);
            }
        }

        private void DrawHorLines()
        {
            var maxCards = 36 / players.Count;
            for (var i = 0; i < (maxCards - 1) * 2 + players.Count * 2; i++)
            {
                PictureBox pic = DrawLine(TableInfo.firstColumnWidth + TableInfo.secondColumnWidth + players.Count * TableInfo.playerColumnWidth, 
                                                                        1, new Point(0, i * TableInfo.roundRowHeight + TableInfo.firstRowHeight + TableInfo.secondRowHeight));
                this.Controls.Add(pic);
            }
        }

        private void DrawPlayerInfoTable()
        {
            var maxCards = 36 / players.Count;
            for (var i = 0; i < players.Count; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    PictureBox pic = DrawLine(1, TableInfo.secondRowHeight + ((maxCards - 1) * 2 + players.Count * 2) * TableInfo.roundRowHeight, new Point(TableInfo.firstColumnWidth + TableInfo.secondColumnWidth + i * TableInfo.playerColumnWidth + j * TableInfo.playerInfoColumnWidth, TableInfo.firstRowHeight));
                    this.Controls.Add(pic);
                }
            }
        }

        private void DrawPlayerNames()
        {
            GameLabel gameLabel = new GameLabel();
            for (var i = 0; i < players.Count; i++)
            {
                this.Controls.Add(gameLabel.DrawNames(players[i].name, i));
            }
        }


        private void DrawDistributor()
        {
            var maxCards = 36 / players.Count;
            var counter = 0;
            GameLabel gameLabel = new GameLabel();
            for (var i = 0; i < (maxCards - 1) * 2 + players.Count * 2; i++)
            {
                this.Controls.Add(gameLabel.DrawNameDistrib(players[counter].name, i));
                counter++;
                if (counter == players.Count)
                {
                    counter = 0;
                }
            }
        }

        private void DrawCardCount()
        {
            var maxCards = 36 / players.Count;
            var counter = 1;
            
            GameLabel gameLabel = new GameLabel();

            for (var i = 0; i < maxCards - 1; i++)
            {
                this.Controls.Add(gameLabel.CardCount(counter, i));
                counter++;
            }
            var tempCounter = counter - 1;
            for (var i = 0; i < players.Count; i++)
            {
                this.Controls.Add(gameLabel.CardCount(counter, tempCounter));
                tempCounter++;
            }
            counter--;
            for (var i = maxCards - 1; i > 0; i--)
            {
                this.Controls.Add(gameLabel.CardCount(counter, tempCounter));
                tempCounter++;
                counter--;
            }

            for (var i = 0; i < players.Count; i++)
            {
                this.Controls.Add(gameLabel.CardCount(maxCards, tempCounter));
                tempCounter++;
            }
        }

        private void DrawPlayerInfo()
        {
            GameLabel gameLabel = new GameLabel();
            for (var i = 0; i < players.Count; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    this.Controls.Add(gameLabel.PlayerInfo(j, playerInfo[j], i));
                }
            }
        }

        private void CreateRoundArea()
        {
            var maxCards = 36 / players.Count;
            for (var k = 1; k <= (maxCards - 1) * 2 + players.Count * 2; k++)
            {
                Dictionary<Player, TextBox[]> currentPlayerInfo = new Dictionary<Player, TextBox[]>();
                for (var i = 0; i < players.Count; i++)
                {
                    TextBox[] playerInfo = new TextBox[3];
                    for (var j = 0; j < 3; j++)
                    {
                        TextBox playerChoice = new TextBox();
                        playerChoice.Location = new Point(TableInfo.firstColumnWidth + TableInfo.secondColumnWidth + j * TableInfo.playerInfoColumnWidth + 10 + TableInfo.playerColumnWidth * i,
                                                            TableInfo.firstRowHeight + TableInfo.secondRowHeight + (k - 1) * TableInfo.roundRowHeight + 6);
                        playerChoice.Size = new Size(60, 30);
                        playerInfo[j] = playerChoice;
                        this.Controls.Add(playerChoice);
                    }
                    currentPlayerInfo.Add(players[i], playerInfo);
                }
                allPlayersChoice.Add(k, currentPlayerInfo);
            }
        }

        private void CreateResultLabels()
        {
            var maxCards = 36 / players.Count;
            for (var k = 1; k <= (maxCards - 1) * 2 + players.Count * 2; k++)
            {
                List<Label> stepResults = new List<Label>();
                for (var i = 1; i <= players.Count; i++)
                {
                    Label label = new Label();
                    label.Font = MainFont.font;
                    label.Location = new Point(TableInfo.firstColumnWidth + TableInfo.secondColumnWidth + i * TableInfo.playerColumnWidth - TableInfo.playerInfoColumnWidth,
                                                TableInfo.firstRowHeight + TableInfo.secondRowHeight + (k - 1) * TableInfo.roundRowHeight);
                    this.Controls.Add(label);
                    stepResults.Add(label);
                }
                playersResults.Add(k, stepResults);
            }
        }

        private void ShowResults((int, List<int>) results)
        {
            for (var i = 0; i < results.Item2.Count; i++)
            {
                if (results.Item1 == 1)
                {
                    playersResults[results.Item1][i].Text = results.Item2[i].ToString();
                } else
                {
                    playersResults[results.Item1][i].Text = (int.Parse(playersResults[results.Item1 - 1][i].Text) + results.Item2[i]).ToString();
                }
            }

        }
    }
}
