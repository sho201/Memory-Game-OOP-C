using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex05.MemoryGameLogic;

namespace Ex05.MemoryGameUI
{
    public class CardsComponent : Panel
    {
        private const int k_Padding = 5;
        private readonly List<CardButton> r_DisabledCards;
        private CardButton[,] m_Cards;
        private int m_startX;
        private int m_startY;

        public event EventHandler<CardClickedEventArgs> CardClicked;

        public CardsComponent(Board i_Board)
        {
            m_Cards = new CardButton[i_Board.Rows, i_Board.Columns];
            r_DisabledCards = new List<CardButton>();
            m_startX = Left;
            m_startY = Top;
            initializeComponent();
            setCards(i_Board);
        }

        public void RestartComponent(Board i_Board)
        {
            this.Refresh();
            Controls.Clear();
            m_Cards = new CardButton[i_Board.Rows, i_Board.Columns];
            ClearDisabledCards();
            initializeComponent();
            setCards(i_Board);
            this.Refresh();
        }

        public void MakeVisibleAndDisableCard(int i_Row, int i_Column)
        {
            CardButton cardButton = m_Cards[i_Row, i_Column];
            r_DisabledCards.Add(cardButton);
            cardButton.SetDisabledOrEnabled(true);
        }

        public void ClearDisabledCards()
        {
            r_DisabledCards.Clear();
        }

        public void EnableCards()
        {
            foreach (CardButton card in r_DisabledCards)
            {
                card.SetDisabledOrEnabled(false);
            }

            r_DisabledCards.Clear();
        }

        public CardButton GetCardButton(int i_Row, int i_Column)
        {
            return m_Cards[i_Row, i_Column];
        }

        private void OnCardClicked(CardClickedEventArgs e)
        {
            CardClicked?.Invoke(this, e);
        }

        private void cardButton_Click(object sender, EventArgs e)
        {
            CardButton card = sender as CardButton;
            if (card != null)
            {
                OnCardClicked(new CardClickedEventArgs(card.Row, card.Column));
            }
        }

        private void setCards(Board i_Board)
        {
            for (int i = 0; i < i_Board.Rows; i++)
            {
                for (int j = 0; j < i_Board.Columns; j++)
                {
                    char letter = i_Board.GetCardLetter(i, j);
                    m_Cards[i, j] = new CardButton(i, j, letter);
                    int xPosition = m_startX + ((k_Padding + m_Cards[i, j].Width) * i);
                    int yPosition = m_startY + ((k_Padding + m_Cards[i, j].Height) * j);
                    m_Cards[i, j].Location = new Point(xPosition, yPosition);
                    m_Cards[i, j].Click += cardButton_Click;
                    Controls.Add(m_Cards[i, j]);
                }
            }
        }

        private void initializeComponent()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }
    }
}