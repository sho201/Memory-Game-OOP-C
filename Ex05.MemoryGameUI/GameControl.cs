using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Ex05.MemoryGameLogic;

namespace Ex05.MemoryGameUI
{
    public class GameControl
    {
        private readonly Game r_Game;
        private readonly SettingsForm r_SettingsForm;
        private BoardGameForm m_BoardGameForm;

        public GameControl()
        {
            r_Game = new Game();
            r_SettingsForm = new SettingsForm();
        }

        public void StartDialog()
        {
            r_SettingsForm.Start += settingsForm_Start;
            r_Game.NewGame += game_NewGame;
            r_SettingsForm.ShowDialog();
        }

        private void boardCard_VisibilityChanged(object sender, EventArgs e)
        {
            Card card = sender as Card;

            if (card != null)
            {
                int row = card.Row;
                int column = card.Column;
                m_BoardGameForm.CardsComponent.GetCardButton(row, column).SetDisabledOrEnabled(card.Visible);
            }
        }

        private void cardsComponent_CardClicked(object sender, CardClickedEventArgs e)
        {
            r_Game.UserTurn(e.Line, e.Col);
        }

        private void game_PcTurn(object sender, EventArgs e)
        {
            m_BoardGameForm.CardsComponent.EnableCards();
            Game game = sender as Game;

            if (game != null)
            {
                r_Game.PcChoice();
            }
        }

        private void game_GameOver(object sender, GameEventArgs e)
        {
            Game game = sender as Game;
            
            if (game != null)
            {
                const string caption = "Winner Message";
                const string question = "Do you want to play another game?";
                string message = string.Format("{0}{1}{2}", game.Winner(), Environment.NewLine, question);
                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    r_Game.AnotherGame(e.Rows, e.Columns, e.FirstPlayerName, e.SecondPlayerName, e.AgainstComputer);
                }
                else
                {
                    if (result == DialogResult.No)
                    {
                        m_BoardGameForm.Close();
                    }
                }
            }
        }

        private void game_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            m_BoardGameForm.NameLabels.UpdateText(e);
        }

        private void game_NewGame(object sender, GameEventArgs e)
        {
            r_Game.StatusChanged += game_StatusChanged;
            r_Game.GameOver += game_GameOver;
            r_Game.RestartGame += game_RestartGame;
            setBoardCardsOnVisibilityChanged();
            m_BoardGameForm = new BoardGameForm(r_Game.Board, e.FirstPlayerName, e.SecondPlayerName);
            m_BoardGameForm.CardsComponent.CardClicked += cardsComponent_CardClicked;
            m_BoardGameForm.ShowDialog();
        }

        private void game_RestartGame(object sender, GameEventArgs e)
        {
            setBoardCardsOnVisibilityChanged();
            m_BoardGameForm.RestartBoardForm(r_Game.Board);
        }

        private void setBoardCardsOnVisibilityChanged()
        {
            int rows = r_Game.Board.Rows;
            int columns = r_Game.Board.Columns;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    r_Game.Board.GetCard(i, j).VisibilityChanged += boardCard_VisibilityChanged;
                }
            }
        }

        private void settingsForm_Start(object sender, GameEventArgs e)
        {
            r_SettingsForm.Hide();
            r_Game.StartGame(e.Rows, e.Columns, e.FirstPlayerName, e.SecondPlayerName, e.AgainstComputer);
        }
    }
}