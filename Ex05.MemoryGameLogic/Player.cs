using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGameLogic
{
    public class Player
    {
        private const string k_PCName = "Computer";
        private readonly string r_PlayerName;
        private int m_Points = 0;
        private bool m_IsPC;

        public Player(string i_PlayerName)
        {
            r_PlayerName = i_PlayerName;
            m_IsPC = false;
        }

        public Player(bool i_IsPC)
        {
            r_PlayerName = k_PCName;
            m_IsPC = i_IsPC;
        }

        public bool IsPC()
        {
            return m_IsPC;
        }

        public int Points
        {
            get
            {
                return m_Points;
            }

            set
            {
                m_Points = value;
            }
        }

        public string Name
        {
            get
            {
                return r_PlayerName;
            }
        }
    }
}