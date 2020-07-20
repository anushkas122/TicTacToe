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
            currentPlayer = 1;  //player 1 is 'X', player 2 is 'O'
            end = false;
        }
        /* resets board is players want a rematch */
        public void clearBoard()
        {
            board = new string[3, 3] { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
        }
        /* adds player's move to board*/
        private void addMove(int row, int col)
        {
            string move = "X";
            if(currentPlayer == 2)
            {
                move = "O";
            }
            board[row, col] = move;   //sets place on board with appropriate symbol for player
        }
        /* checks that the player has specificed a valid position on board
           and that spot hasn't been played already */
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
        /* changes active player */
        private void setPlayer()
        {
            currentPlayer = (currentPlayer == 1) ? currentPlayer = 2 : currentPlayer = 1;
            Console.WriteLine("Player {0}, your turn", currentPlayer.ToString());
        }
        /* checks after each round if the board is filled and thus a tie */
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
        /* checks for three in a row */
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
        /* prints out a basic board to the console */
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
        /* runs the game by promting the user to choose row and columns on the board */
        public void startGame()
        {
            end = false;   //is board full or is there a three in a row?
            currentPlayer = 1;

            Console.WriteLine("Player {0}, your turn", currentPlayer.ToString());
            while (!end)
            {
                Console.WriteLine("Row number between 0-2: ");
                bool valid = Int32.TryParse(Console.ReadLine(), out int row);
                while(!valid)  //checks that value entered by user is a valid number
                {
                    Console.WriteLine("Invalid row number, please choose a row number between 0-2");  //row number
                    Console.WriteLine("Row: ");
                    valid = Int32.TryParse(Console.ReadLine(), out row);
                }
                Console.WriteLine("Column number between 0-2: ");
                valid = Int32.TryParse(Console.ReadLine(), out int col);
                while (!valid)
                {
                    Console.WriteLine("Invalid column number, please choose a column number between 0-2");   //column number
                    Console.WriteLine("Column: ");
                    valid = Int32.TryParse(Console.ReadLine(), out row);
                }
                if(validMove(row, col))  //checks that move can be made by player
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
                        setPlayer();  //board isn't full and there's no winner, so swap player and continue
                    }
                } else
                {
                    Console.WriteLine("Invalid move, please choose a row and column each between 0-2");
                }
            }
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
            while (newGame == "y" || newGame == "yes")  //start a new game until user specifies they don't want a new game
            {
                game.clearBoard();
                game.startGame();
                newGame = Console.ReadLine();
            }
        }
    }
}
