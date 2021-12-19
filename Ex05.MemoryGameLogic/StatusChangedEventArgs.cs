using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGameLogic
{
    public class StatusChangedEventArgs : EventArgs
    {
        private readonly string r_CurrentPlayerName;
        private readonly string r_FirstPlayerName;
        private readonly string r_SecondPlayerName;
        private readonly int r_FirstPlayerPoints;
        private readonly int r_SecondPlayerPoints;

        public StatusChangedEventArgs(string i_CurrentPlayerName, string i_FirstPlayerName, string i_SecondPlayerName, int i_FirstPlayerPoints, int i_SecondPlayerPoints)
        {
            r_CurrentPlayerName = i_CurrentPlayerName;
            r_FirstPlayerName = i_FirstPlayerName;
            r_SecondPlayerName = i_SecondPlayerName;
            r_FirstPlayerPoints = i_FirstPlayerPoints;
            r_SecondPlayerPoints = i_SecondPlayerPoints;
        }

        public string CurrentPlayerName
        {
            get
            {
                return r_CurrentPlayerName;
            }
        }

        public string FirstPlayerName
        {
            get
            {
                return r_FirstPlayerName;
            }
        }

        public int FirstPlayerPoints
        {
            get
            {
                return r_FirstPlayerPoints;
            }
        }

        public string SecondPlayerName
        {
            get
            {
                return r_SecondPlayerName;
            }
        }

        public int SecondPlayerPoints
        {
            get
            {
                return r_SecondPlayerPoints;
            }
        }
    }
}