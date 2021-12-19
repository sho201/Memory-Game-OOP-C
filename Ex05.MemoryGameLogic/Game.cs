using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace Ex05.MemoryGameLogic
{
    public class Game
    {
        private const int k_ASCIofA = 65;
        private const int k_NumOfMSSleep = 1000;
        private Board m_Board;
        private int m_Columns;
        private int m_Rows;
        private Player m_CurrentPlayer;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private Card m_FirstChoice;
        private Card m_SecondChoice;
        private bool m_AgainstComputer;
        private bool m_IsGameOver;
        private bool m_IsFirstChoice;

        public event EventHandler<GameEventArgs> NewGame;

        public event EventHandler<GameEventArgs> RestartGame;

        public event EventHandler<GameEventArgs> GameOver;

        public event EventHandler<StatusChangedEventArgs> StatusChanged;

        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return m_IsGameOver;
            }

            private set
            {
                if (value)
                {
                    m_IsGameOver = value;
                    OnGameOver(new GameEventArgs(m_Rows, m_Columns, m_FirstPlayer.Name, m_SecondPlayer.Name, m_AgainstComputer));
                }
            }
        }

        public int FirstPlayerPoint
        {
            get
            {
                return m_FirstPlayer.Points;
            }

            set
            {
                m_FirstPlayer.Points = value;
            }
        }

        public bool IsFirstChoice
        {
            get
            {
                return m_IsFirstChoice;
            }

            set
            {
                m_IsFirstChoice = value;
            }
        }

        public int SecondPlayerPoint
        {
            get
            {
                return m_SecondPlayer.Points;
            }

            set
            {
                m_SecondPlayer.Points = value;
            }
        }

        public void StartGame(int i_Rows, int i_Columns, string i_FirstPlayerName, string i_SecondPlayerName, bool i_AgainstComputer)
        {
            m_Board = new Board(i_Rows, i_Columns);
            m_Columns = i_Columns;
            m_Rows = i_Rows;
            m_FirstPlayer = new Player(i_FirstPlayerName);
            m_CurrentPlayer = m_FirstPlayer;
            m_IsFirstChoice = true;
            m_AgainstComputer = i_AgainstComputer;

            if (!i_AgainstComputer)
            {
                m_SecondPlayer = new Player(i_SecondPlayerName);
            }
            else
            {
                m_SecondPlayer = new Player(true);
            }

            OnNewGame(new GameEventArgs(i_Rows, i_Columns, i_FirstPlayerName, i_SecondPlayerName, i_AgainstComputer));
        }

        public void AnotherGame(int i_Rows, int i_Columns, string i_FirstPlayerName, string i_SecondPlayerName, bool i_AgainstComputer)
        {
            m_Board = new Board(m_Rows, m_Columns);
            m_FirstPlayer.Points = 0;
            m_SecondPlayer.Points = 0;
            m_CurrentPlayer = m_FirstPlayer;
            OnRestartGame(new GameEventArgs(i_Rows, i_Columns, i_FirstPlayerName, i_SecondPlayerName, i_AgainstComputer));
        }

        public void OnRestartGame(GameEventArgs e)
        {
            RestartGame?.Invoke(this, e);
        }

        private void OnNewGame(GameEventArgs e)
        {
            NewGame?.Invoke(this, e);
        }

        private void OnGameOver(GameEventArgs e)
        {
            GameOver?.Invoke(this, e);
        }

        private void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }

        public void UserTurn(int i_Row, int i_Column)
        {
            if (IsFirstChoice)
            {
                m_FirstChoice = m_Board.GetCard(i_Row, i_Column);
                m_Board.GetCard(i_Row, i_Column).Visible = true;
            }
            else
            {
                m_SecondChoice = m_Board.GetCard(i_Row, i_Column);
                m_Board.GetCard(i_Row, i_Column).Visible = true;
                Thread.Sleep(k_NumOfMSSleep);

                if (m_FirstChoice.Letter == m_SecondChoice.Letter)
                {
                    m_CurrentPlayer.Points++;
                    OnStatusChanged(new StatusChangedEventArgs(m_CurrentPlayer.Name, m_FirstPlayer.Name, m_SecondPlayer.Name, m_FirstPlayer.Points, m_SecondPlayer.Points));
                    checkGameOver();
                }
                else
                {
                    m_Board.GetCard(m_FirstChoice.Row, m_FirstChoice.Column).Visible = false;
                    m_Board.GetCard(m_SecondChoice.Row, m_SecondChoice.Column).Visible = false;

                    if (m_CurrentPlayer == m_FirstPlayer)
                    {
                        m_CurrentPlayer = m_SecondPlayer;
                    }
                    else
                    {
                        m_CurrentPlayer = m_FirstPlayer;
                    }

                    OnStatusChanged(new StatusChangedEventArgs(m_CurrentPlayer.Name, m_FirstPlayer.Name, m_SecondPlayer.Name, m_FirstPlayer.Points, m_SecondPlayer.Points));

                    if (m_AgainstComputer)
                    {
                        PcChoice();
                    }
                }
            }

            m_IsFirstChoice = !m_IsFirstChoice;
        }

        private void checkGameOver()
        {
            IsGameOver = m_Board.EndOfGame();
        }

        public void PcChoice()
        {
            while (true)
            {
                Thread.Sleep(k_NumOfMSSleep);
                Random random = new Random();
                int randomColumns = random.Next(m_Board.Columns);
                int randomRows = random.Next(m_Board.Rows);

                while (m_Board.GetCard(randomRows, randomColumns).Visible)
                {
                    randomColumns = random.Next(m_Board.Columns);
                    randomRows = random.Next(m_Board.Rows);
                }

                Card firstChoice = m_Board.GetCard(randomRows, randomColumns);
                m_Board.GetCard(randomRows, randomColumns).Visible = true;
                Thread.Sleep(k_NumOfMSSleep);

                int secondRandomColumns = random.Next(m_Board.Columns);
                int secondRandomRows = random.Next(m_Board.Rows);

                while (m_Board.GetCard(secondRandomRows, secondRandomColumns).Visible)
                {
                    secondRandomColumns = random.Next(m_Board.Columns);
                    secondRandomRows = random.Next(m_Board.Rows);
                }

                Card secondChoice = m_Board.GetCard(secondRandomRows, secondRandomColumns);
                m_Board.GetCard(secondRandomRows, secondRandomColumns).Visible = true;
                Thread.Sleep(k_NumOfMSSleep);

                if (firstChoice.Letter == secondChoice.Letter)
                {
                    m_CurrentPlayer.Points++;
                    OnStatusChanged(new StatusChangedEventArgs(m_CurrentPlayer.Name, m_FirstPlayer.Name, m_SecondPlayer.Name, m_FirstPlayer.Points, m_SecondPlayer.Points));
                    checkGameOver();
                    if (IsGameOver)
                    {
                        break;
                    }
                }
                else
                {
                    m_Board.GetCard(randomRows, randomColumns).Visible = false;
                    m_Board.GetCard(secondRandomRows, secondRandomColumns).Visible = false;

                    if (m_CurrentPlayer == m_FirstPlayer)
                    {
                        m_CurrentPlayer = m_SecondPlayer;
                    }
                    else
                    {
                        m_CurrentPlayer = m_FirstPlayer;
                    }

                    OnStatusChanged(new StatusChangedEventArgs(m_CurrentPlayer.Name, m_FirstPlayer.Name, m_SecondPlayer.Name, m_FirstPlayer.Points, m_SecondPlayer.Points));
                    break;
                }
            }
        }

        public string Winner()
        {
            string winnerMessage;

            if (m_FirstPlayer.Points > m_SecondPlayer.Points)
            {
                winnerMessage = string.Format("The winner is: {0}!", m_FirstPlayer.Name);
            }
            else
            {
                if (m_SecondPlayer.Points > m_FirstPlayer.Points)
                {
                    winnerMessage = string.Format("The winner is: {0}!", m_SecondPlayer.Name);
                }
                else
                {
                    winnerMessage = "It's a tie!";
                }
            }

            return winnerMessage;
        }
    }
}