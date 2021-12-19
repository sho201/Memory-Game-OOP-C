using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace Ex05.MemoryGameUI
{
    public class CardButton : Button
    {
        private const int k_Height = 80;
        private const int k_Width = 80;
        private readonly int r_Column;
        private readonly int r_Row;
        private readonly char r_Letter;

        public CardButton(int i_Row, int i_Column, char i_Letter)
        {
            r_Row = i_Row;
            r_Column = i_Column;
            r_Letter = i_Letter;
            Size = new Size(k_Width, k_Height);
            Margin = new Padding(5, 5, 5, 5);
            Text = string.Empty;
            Font = new Font("Microsoft Sans Serif", 12.00F);
        }

        public int Column
        {
            get
            {
                return r_Column;
            }
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

        public void SetDisabledOrEnabled(bool i_Value)
        {
            if (i_Value)
            {
                setDisabled();
            }
            else
            {
                setEnabled();
            }
        }

        private void setDisabled()
        {
            Text = Letter.ToString();
            BackColor = ColorControl.CurrentPlayerColor;
            Enabled = false;
        }

        private void setEnabled()
        {
            Text = string.Empty;
            ResetBackColor();
            Enabled = true;
        }
    }
}