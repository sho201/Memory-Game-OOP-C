using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGameLogic
{
    public class Card
    {
        private readonly char r_Letter;
        private readonly int r_Row;
        private readonly int r_Column;
        private bool m_IsVisible;

        public event EventHandler VisibilityChanged;

        public Card(char i_Letter, int i_Row, int i_Column)
        {
            r_Letter = i_Letter;
            r_Row = i_Row;
            r_Column = i_Column;
            m_IsVisible = false;
        }

        public char Letter
        {
            get
            {
                return r_Letter;
            }
        }

        public int Row
        {
            get
            {
                return r_Row;
            }
        }

        public int Column
        {
            get
            {
                return r_Column;
            }
        }

        public bool Visible
        {
            get
            {
                return m_IsVisible;
            }

            set
            {
                if (m_IsVisible != value)
                {
                    m_IsVisible = value;
                    OnVisibilityChanged();
                }
            }
        }

        public bool IsEqual(Card i_CardToCheck)
        {
            return this.r_Letter.Equals(i_CardToCheck.Letter);
        }

        private void OnVisibilityChanged()
        {
            VisibilityChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}