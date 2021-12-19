using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGameLogic
{
    public class GameEventArgs : EventArgs
    {
        private readonly int r_BoardRows;
        private readonly int r_BoardColumns;
        private readonly bool r_AgainstComputer;
        private readonly string r_FirstPlayerName;
        private readonly string r_SecondPlayerName;

        public GameEventArgs(int i_BoardRows, int i_BoardColumns, string i_FirstPlayerName, string i_SecondPlayerName, bool i_AgainstComputer)
        {
            r_BoardRows = i_BoardRows;
            r_BoardColumns = i_BoardColumns;
            r_FirstPlayerName = i_FirstPlayerName;
            r_SecondPlayerName = i_SecondPlayerName;
            r_AgainstComputer = i_AgainstComputer;
        }

        public int Columns
        {
            get
            {
                return r_BoardColumns;
            }
        }

        public int Rows
        {
            get
            {
                return r_BoardRows;
            }
        }

        public bool AgainstComputer
        {
            get
            {
                return r_AgainstComputer;
            }
        }

        public string FirstPlayerName
        {
            get
            {
                return r_FirstPlayerName;
            }
        }

        public string SecondPlayerName
        {
            get
            {
                return r_SecondPlayerName;
            }
        }
    }
}