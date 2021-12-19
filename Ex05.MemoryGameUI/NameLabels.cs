using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex05.MemoryGameLogic;

namespace Ex05.MemoryGameUI
{
    public class NameLabels : FlowLayoutPanel
    {
        private readonly Label r_CurrentPlayer;
        private readonly Label r_FirstPlayer;
        private readonly Label r_SecondPlayer;
        private readonly Color r_FirstColor = ColorControl.FirstColor;
        private readonly Color r_SecondColor = ColorControl.SecondColor;
        private string m_CurrentPlayerName;
        private string m_FirstPlayerName;
        private string m_SecondPlayerName;

        public string FirstPlayerName
        {
            get
            {
                return m_FirstPlayerName;
            }

            set
            {
                m_FirstPlayerName = value;
            }
        }

        public string SecondPlayerName
        {
            get
            {
                return m_SecondPlayerName;
            }

            set
            {
                m_SecondPlayerName = value;
            }
        }

        public string CurrentPlayerName
        {
            get
            {
                return m_CurrentPlayerName;
            }

            set
            {
                m_CurrentPlayerName = value;
            }
        }

        public NameLabels(string i_FirstPlayerName, string i_SecondPlayerName)
        {
            m_FirstPlayerName = i_FirstPlayerName;
            m_CurrentPlayerName = i_FirstPlayerName;
            m_SecondPlayerName = i_SecondPlayerName;
            r_CurrentPlayer = new Label();
            setLabel(r_CurrentPlayer, r_FirstColor);
            r_FirstPlayer = new Label();
            setLabel(r_FirstPlayer, r_FirstColor);
            r_SecondPlayer = new Label();
            setLabel(r_SecondPlayer, r_SecondColor);
            r_CurrentPlayer.Text = currentPlayerText(m_CurrentPlayerName);
            r_FirstPlayer.Text = playerText(m_FirstPlayerName, 0);
            r_SecondPlayer.Text = playerText(m_SecondPlayerName, 0);
            setup();
        }

        public void setLabel(Label i_NameLabel, Color i_Color)
        {
            i_NameLabel.Size = new Size(100, 20);
            i_NameLabel.AutoSize = true;
            i_NameLabel.Font = new Font("Microsoft Sans Serif", 10.00F);
            i_NameLabel.Margin = new Padding(0, 10, 5, 10);
            i_NameLabel.BackColor = i_Color;
        }

        public void UpdateText(StatusChangedEventArgs e)
        {
            this.Refresh();
            if (!m_CurrentPlayerName.Equals(e.CurrentPlayerName))
            {
                ColorControl.ChangeCurrentPlayerColor();
            }

            m_FirstPlayerName = e.FirstPlayerName;
            m_SecondPlayerName = e.SecondPlayerName;
            m_CurrentPlayerName = e.CurrentPlayerName;
            r_FirstPlayer.Text = playerText(e.FirstPlayerName, e.FirstPlayerPoints);
            r_SecondPlayer.Text = playerText(e.SecondPlayerName, e.SecondPlayerPoints);
            r_CurrentPlayer.Text = currentPlayerText(m_CurrentPlayerName);
            r_CurrentPlayer.BackColor = ColorControl.CurrentPlayerColor;
            this.Refresh();
        }

        public void UpdateTextForRestart(StatusChangedEventArgs e)
        {
            this.Refresh();
            m_FirstPlayerName = e.FirstPlayerName;
            m_SecondPlayerName = e.SecondPlayerName;
            m_CurrentPlayerName = e.FirstPlayerName;
            r_FirstPlayer.Text = playerText(e.FirstPlayerName, e.FirstPlayerPoints);
            r_SecondPlayer.Text = playerText(e.SecondPlayerName, e.SecondPlayerPoints);
            r_CurrentPlayer.Text = currentPlayerText(m_CurrentPlayerName);
            if (r_CurrentPlayer.BackColor != ColorControl.FirstColor)
            {
                ColorControl.ChangeCurrentPlayerColor();
                r_CurrentPlayer.BackColor = ColorControl.FirstColor;
            }
            
            this.Refresh();
        }

        private static string currentPlayerText(string i_Name)
        {
            return string.Format("Current Player: {0}", i_Name);
        }

        private static string playerText(string i_Name, int i_Score)
        {
            return string.Format("{0}: {1} Pairs", i_Name, i_Score);
        }

        private void setup()
        {
            FlowDirection = FlowDirection.TopDown;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(5, 5, 5, 5);
            Control[] labels = { r_CurrentPlayer, r_FirstPlayer, r_SecondPlayer };
            Controls.AddRange(labels);
        }
    }
}