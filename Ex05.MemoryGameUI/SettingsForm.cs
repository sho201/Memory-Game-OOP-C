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
    public partial class SettingsForm : Form
    {
        private const string k_ComputerLabel = "- computer -";
        private const string k_ComputerName = "Computer";
        private const string k_AgainstComputerLabel = "Against Computer";
        private const string k_AgainstFriendLabel = "Against a Friend";
        private const int k_MaxColumns = 6;
        private const int k_MaxRows = 6;
        private const int k_MinColumns = 4;
        private const int k_MinRows = 4;
        private int m_CurrentColumn = 4;
        private int m_CurrentRow = 4;
        private bool m_AgainstFriend;

        public event EventHandler<GameEventArgs> Start;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void OnStart(GameEventArgs e)
        {
            Start?.Invoke(this, e);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int rows = m_CurrentRow;
            int columns = m_CurrentColumn;
            string firstName = textBoxFirstPlayer.Text;
            string secondName;
            bool againstComputer = !m_AgainstFriend;

            if (m_AgainstFriend)
            {
                secondName = textBoxSecondPlayer.Text;
            }
            else
            {
                secondName = k_ComputerName;
            }
            
            OnStart(new GameEventArgs(rows, columns, firstName, secondName, againstComputer));
        }

        private void buttonFriendComputer_Click(object sender, EventArgs e)
        {
            m_AgainstFriend = !m_AgainstFriend;

            if (!m_AgainstFriend)
            {
                disableSecondPlayerTextBox();
            }
            else
            {
                enableSecondPlayerTextBox();
            }
        }

        private void disableSecondPlayerTextBox()
        {
            textBoxSecondPlayer.Enabled = false;
            textBoxSecondPlayer.Text = k_ComputerLabel;
            buttonAgainstFriend.Text = k_AgainstFriendLabel;
        }

        private void enableSecondPlayerTextBox()
        {
            textBoxSecondPlayer.Enabled = true;
            textBoxSecondPlayer.Text = string.Empty;
            buttonAgainstFriend.Text = k_AgainstComputerLabel;
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            m_CurrentColumn++;

            if (m_CurrentColumn > k_MaxColumns)
            {
                m_CurrentColumn = k_MinColumns;
                m_CurrentRow++;
            }

            if (m_CurrentRow > k_MaxRows)
            {
                m_CurrentRow = k_MinRows;
            }

            if (m_CurrentRow == 5 && m_CurrentColumn == 5)
            {
                m_CurrentColumn = 6;
                m_CurrentRow = 5;
            }

            buttonBoardSize.Text = string.Format("{0} x {1}", m_CurrentRow, m_CurrentColumn);
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
        }
    }
}