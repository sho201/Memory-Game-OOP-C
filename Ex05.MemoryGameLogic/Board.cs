using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.MemoryGameLogic
{
    public class Board
    {
        private const int k_ASCIofA = 65;
        private readonly int r_ColumnsNumber;
        private readonly int r_RowsNumber;
        private readonly Card[,] r_CardsInBoard;
        private List<char> m_ListOfLetters;

        public Board(int i_RowsNumber, int i_ColumnsNumber)
        {
            r_ColumnsNumber = i_ColumnsNumber;
            r_RowsNumber = i_RowsNumber;
            r_CardsInBoard = new Card[i_RowsNumber, i_ColumnsNumber];
            m_ListOfLetters = new List<char>(i_ColumnsNumber * i_RowsNumber);

            fillLetters();
            fillBoard();
        }

        public int Columns
        {
            get
            {
                return r_ColumnsNumber;
            }
        }

        public int Rows
        {
            get
            {
                return r_RowsNumber;
            }
        }

        private void fillLetters()
        {
            int numberOfLetters = (r_ColumnsNumber * r_RowsNumber) / 2;

            for (int i = 0; i < numberOfLetters; i++)
            {
                m_ListOfLetters.Add((char)(k_ASCIofA + i));
                m_ListOfLetters.Add((char)(k_ASCIofA + i));
            }
        }

        private void fillBoard()
        {
            int randomNumber = 0;
            Random random = new Random();

            for (int i = 0; i < r_RowsNumber; i++)
            {
                for (int j = 0; j < r_ColumnsNumber; j++)
                {
                    randomNumber = random.Next(m_ListOfLetters.Count);
                    r_CardsInBoard[i, j] = new Card(m_ListOfLetters[randomNumber], i, j);
                    m_ListOfLetters.RemoveAt(randomNumber);
                }
            }
        }

        public bool EndOfGame()
        {
            bool isEnd = true;

            for (int i = 0; i < r_RowsNumber; i++)
            {
                for (int j = 0; j < r_ColumnsNumber; j++)
                {
                    if (!GetCard(i, j).Visible)
                    {
                        isEnd = false;
                        break;
                    }
                }
            }

            return isEnd;
        }

        public Card GetCard(int i_RowNumber, int i_ColumnNumber)
        {
            return r_CardsInBoard[i_RowNumber, i_ColumnNumber];
        }

        public char GetCardLetter(int i_RowNumber, int i_ColumnNumber)
        {
            return r_CardsInBoard[i_RowNumber, i_ColumnNumber].Letter;
        }
    }
}