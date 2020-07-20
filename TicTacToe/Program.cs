using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TicTacToe
{
    class Game
    {
        private string[,] board;
        private int currentPlayer;
        private bool end;
        public Game()
        {
            board = new string[3,3] { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } }; 
            currentPlayer = 1;
            end = false;
        }
        public void clearBoard()
        {
            board = new string[3, 3] { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
        }
        private void addMove(int row, int col)
        {
            string move = "X";
            if(currentPlayer == 2)
            {
                move = "O";
            }
            board[row, col] = move;
        }
        public bool validMove(int row, int col)
        {
            if (row < 0 || row > 2 || col < 0 || col > 2)
            {
                return false;
            }
            if (board[row, col]  != " ")
            {
                return false;
            }
            return true;
        }
        private void setPlayer()
        {
            currentPlayer = (currentPlayer == 1) ? currentPlayer = 2 : currentPlayer = 1;
            Console.WriteLine("Player {0}, your turn", currentPlayer.ToString());
        }
        private void fullBoard()
        {
            bool full = true;
            //full board no winner
            foreach(string move in board)
            {
                if(move == " ")
                {
                    full = false;
                }
            }
            if(full)
            {
                end = true;
                Console.WriteLine("Tie Game! Would you like to play another round? y/n");
            }
        }
        private void gameisWon()
        {
            bool threeInARow = false;
            int player = 1;
            string symbol = " ";
            for(int r = 0; r < 3; r++)
            {
                for(int c = 0; c < 3; c++)
                {
                    symbol = board[r, c];
                    if (r == 0 && c == 0)
                    {
                        if(board[r, c + 1] == symbol && board[r, c + 2] == symbol && symbol != " ")   //across row
                        {
                            threeInARow = true;
                        }
                        if (board[r + 1, c + 1] == symbol && board[r + 2, c + 2] == symbol && symbol != " ")   //diagonal
                        {
                            threeInARow = true;
                        }
                        if (board[r + 1, c] == symbol && board[r + 2, c] == symbol && symbol != " ")   //down column
                        {
                            threeInARow = true;
                        }
                    }
                    else if (r == 0 && c == 1)
                    {
                        if (board[r + 1, c] == symbol && board[r + 2, c] == symbol && symbol != " ")   //down column
                        {
                            threeInARow = true;
                        }
                    }
                    else if (r == 0 && c == 2)
                    {
                        if (board[r + 1, c - 1] == symbol && board[r + 2, c - 2] == symbol && symbol != " ")   //diagonal
                        {
                            threeInARow = true;
                        }
                        if (board[r + 1, c] == symbol && board[r + 2, c] == symbol && symbol != " ")   //down column
                        {
                            threeInARow = true;
                        }
                    }
                    else if (r == 1 && c == 0)
                    {
                        if (board[r, c + 1] == symbol && board[r, c + 2] == symbol && symbol != " ")   //across row
                        {
                            threeInARow = true;
                        }
                    }
                    else if (r == 2 && c == 0)
                    {
                        if (board[r, c + 1] == symbol && board[r, c + 2] == symbol && symbol != " ")   //across row
                        {
                            threeInARow = true;
                        }
                    }
                }
            }
            if(threeInARow)
            {
                if (symbol == "O")
                {
                    player = 2;
                }
                Console.WriteLine("Player {0} wins! Would you like to play another round? y/n", player.ToString());
                end = true;
            }
        }

        private void printBoard()
        {
            Console.WriteLine("\n");
            Console.WriteLine("      0   1   2");
            Console.WriteLine("\n");
            for (int r = 0; r < 3; r++)
            {
                Console.WriteLine("{0}     {1}   {2}   {3}", r, board[r, 0], board[r, 1], board[r, 2]);
            }

        }
        public void startGame()
        {
            end = false;
            currentPlayer = 1;
            int row, col;

            Console.WriteLine("Player {0}, your turn", currentPlayer.ToString());
            while (!end)
            {
                Console.WriteLine("Row number between 0-2: ");
                bool valid = Int32.TryParse(Console.ReadLine(), out row);
                while(!valid)
                {
                    Console.WriteLine("Invalid row number, please choose a row number between 0-2");
                    Console.WriteLine("Row: ");
                    valid = Int32.TryParse(Console.ReadLine(), out row);
                }
                Console.WriteLine("Column number between 0-2: ");
                valid = Int32.TryParse(Console.ReadLine(), out col);
                while (!valid)
                {
                    Console.WriteLine("Invalid column number, please choose a column number between 0-2");
                    Console.WriteLine("Column: ");
                    valid = Int32.TryParse(Console.ReadLine(), out row);
                }
                if(validMove(row, col))
                {
                    addMove(row, col);
                    printBoard();
                    gameisWon();
                    if(!end)
                    {
                        fullBoard();
                    }
                    if (!end)
                    {
                        setPlayer();
                    }
                } else
                {
                    Console.WriteLine("Invalid move, please choose a row and column each between 0-2");
                }
            }
            return;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //start game
            Game game = new Game();
            game.startGame();
            string newGame = Console.ReadLine();
            while (newGame == "y" || newGame == "yes")
            {
                game.clearBoard();
                game.startGame();
                newGame = Console.ReadLine();
            }
            {

            }
        }
    }
}
