﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ListPoker.View
{
    class GameLabel
    {
        public Label DrawNames(string name, int positionIteration)
        {
            Label label = new Label();
            label.Text = name;
            label.Font = MainFont.font;
            label.Location = new Point(TableInfo.firstColumnWidth + TableInfo.secondColumnWidth + positionIteration * TableInfo.playerColumnWidth + TableInfo.secondRowHeight, 0);
            return label;
        }

        public Label DrawNameDistrib(string name, int positionIteration)
        {
            Label label = new Label();
            label.Text = name;
            label.Font = MainFont.font;
            label.Location = new Point(TableInfo.firstColumnWidth + 20, TableInfo.firstRowHeight + TableInfo.secondRowHeight + positionIteration * TableInfo.roundRowHeight + 10);
            return label;
        }

        public Label CardCount(int counter, int positionIteration)
        {
            Label label = new Label();
            label.Text = counter.ToString();
            label.Font = MainFont.font;
            label.Location = new Point(20, TableInfo.firstRowHeight + TableInfo.secondRowHeight + positionIteration * TableInfo.roundRowHeight + 10);
            return label;
        }

        public Label PlayerInfo(int positionIteration, string info, int currentPlayer)
        {
            Label label = new Label();
            label.Font = MainFont.playerInfo;
            label.Text = info;
            label.Size = new Size(69, 20);
            label.Location = new Point(TableInfo.firstColumnWidth + TableInfo.secondColumnWidth + currentPlayer * TableInfo.playerColumnWidth + TableInfo.playerInfoColumnWidth * positionIteration + 15, TableInfo.roundRowHeight);
            return label;
        }
    }
}
