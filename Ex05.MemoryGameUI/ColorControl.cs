using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ex05.MemoryGameUI
{
    public class ColorControl
    {
        private static readonly Color sr_FirstPlayerColor = Color.FromArgb((int)(byte)192, (int)(byte)255, (int)(byte)192);
        private static readonly Color sr_SecondPlayerColor = Color.FromArgb((int)(byte)191, (int)(byte)191, (int)(byte)255);
        private static Color s_CurrentPlayerColor = Color.FromArgb((int)(byte)192, (int)(byte)255, (int)(byte)192);

        public static Color CurrentPlayerColor
        {
            get
            {
                return s_CurrentPlayerColor;
            }
        }

        public static Color FirstColor
        {
            get
            {
                return sr_FirstPlayerColor;
            }
        }

        public static Color SecondColor
        {
            get
            {
                return sr_SecondPlayerColor;
            }
        }

        public static void ChangeCurrentPlayerColor()
        {
            if (s_CurrentPlayerColor == sr_FirstPlayerColor)
            {
                s_CurrentPlayerColor = sr_SecondPlayerColor;
            }
            else
            {
                s_CurrentPlayerColor = sr_FirstPlayerColor;
            }
        }
    }
}