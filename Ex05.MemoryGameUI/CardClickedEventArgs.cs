using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGameUI
{
    public class CardClickedEventArgs : EventArgs
    {
        private readonly int r_Row;
        private readonly int r_Column;

        public CardClickedEventArgs(int i_Row, int i_Column)
        {
            r_Row = i_Row;
            r_Column = i_Column;
        }

        public int Col
        {
            get
            {
                return r_Column;
            }
        }

        public int Line
        {
            get
            {
                return r_Row;
            }
        }
    }
}