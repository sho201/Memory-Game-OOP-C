using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex05.MemoryGameLogic;

namespace Ex05.MemoryGameUI
{
    public partial class BoardGameForm : Form
    {
        private NameLabels m_NameLabels;
        private CardsComponent m_CardsComponent;

        public BoardGameForm(Board i_Board, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            InitializeComponent();
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.FlowDirection = FlowDirection.TopDown;
            panel.Padding = new Padding(15, 5, 5, 5);
            panel.AutoSize = true;
            panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            m_CardsComponent = new CardsComponent(i_Board);
            m_NameLabels = new NameLabels(i_FirstPlayerName, i_SecondPlayerName);
            panel.Controls.AddRange(new Control[] { m_CardsComponent, m_NameLabels });
            Controls.Add(panel);
            initializeComponent();
            Closed += BoardForm_Closed;
        }

        public CardsComponent CardsComponent
        {
            get
            {
                return m_CardsComponent;
            }
        }

        public NameLabels NameLabels
        {
            get
            {
                return m_NameLabels;
            }
        }

        private void initializeComponent()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        public void RestartBoardForm(Board i_Board)
        {
            m_CardsComponent.RestartComponent(i_Board);
            m_NameLabels.UpdateTextForRestart(new StatusChangedEventArgs(m_NameLabels.CurrentPlayerName, m_NameLabels.FirstPlayerName, m_NameLabels.SecondPlayerName, 0, 0));
            this.Refresh();
        }

        private void BoardForm_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}