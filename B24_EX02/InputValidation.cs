using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B24_EX02
{
    internal class InputValidation
    {
        private const int k_MinimumSizeOfBoard = 4;
        private const int k_MaximumSizeOfBoard = 6;

        internal static int GetBoardDimensions()
        {
            while (true)
            {
                string boardDimensionInput = Console.ReadLine();
                if (int.TryParse(boardDimensionInput, out int dimension) && dimension >= k_MinimumSizeOfBoard && dimension <= k_MaximumSizeOfBoard)
                {
                    return dimension;
                }

                Console.WriteLine(Messages.GetMessage(MessageKey.InvalidInputMessage));
            }
        }

        internal static void CheckRematch(Player i_FirstPlayer, Player i_SecondPlayer)
        {
            Console.WriteLine(Messages.GetMessage(MessageKey.RematchMessage));
            string answerIfRematch = Console.ReadLine();

            while(answerIfRematch != "y" && answerIfRematch != "n")
            {
                Console.WriteLine(Messages.GetMessage(MessageKey.InvalidInputMessage));
                answerIfRematch = Console.ReadLine();
            }

            if (answerIfRematch == "y")
            {
                i_FirstPlayer.PlayerPoints = 0;
                i_SecondPlayer.PlayerPoints = 0;
                MemoryGameManager.GameManager(i_FirstPlayer, i_SecondPlayer, true);
            }

            Environment.Exit(0);
        }

        internal static Player GetSecondPlayer()
        {
            Console.WriteLine(Messages.GetMessage(MessageKey.RequestToPlayAgainstComputerMessage));

            while (true)
            {
                string userInput = Console.ReadLine();
                string secondPlayerName;
                bool isComputer = false;

                if (userInput == "y" || userInput == "n")
                {
                    if (userInput == "y")
                    {
                        secondPlayerName = "BobTheComputer";
                        isComputer = true;
                    }
                    else
                    {
                        Console.WriteLine(Messages.GetMessage(MessageKey.InputSecondNamePlayerMessage));
                        secondPlayerName = Console.ReadLine();
                    }

                    return new Player(secondPlayerName, isComputer);
                }

                Console.WriteLine(Messages.GetMessage(MessageKey.InvalidInputMessage));
            }
        }

        internal static MemoryGameCards<char> GetValidCell(MemoryGame<char> i_CurrentGame)
        {
            char colChar = ' ';
            char rowChar = ' ';
            while (true)
            {
                string input = Console.ReadLine();

                if (input == "Q")
                {
                    Environment.Exit(0);
                }

                if (input.Length == 2 && input != null)
                {
                    colChar = input[0];
                    rowChar = input[1];
                }

                if ((char.IsDigit(rowChar) && char.IsUpper(colChar)) && (input.Length == 2 && input != null))
                {
                    int rowIndex = rowChar - '1';
                    int colIndex = colChar - 'A';

                    if (rowIndex >= 0 && rowIndex < i_CurrentGame.BoardGame.BoardHeight && colIndex >= 0 && colIndex < i_CurrentGame.BoardGame.BoardWidth)
                    {
                        if (i_CurrentGame.BoardGame.GetCardValueOnBoard(rowIndex, colIndex).PairOfCardsDiscovered)
                        {
                            Console.WriteLine(Messages.GetMessage(MessageKey.InvalidChoiseOfCardMessage));
                        }
                        else 
                        {
                            i_CurrentGame.BoardGame.GetCardValueOnBoard(rowIndex, colIndex).IsCardChosen = true;
                            return i_CurrentGame.BoardGame.GetCardValueOnBoard(rowIndex, colIndex);
                        }
                    }
                    else 
                    {
                        Console.WriteLine(Messages.GetMessage(MessageKey.InvalidInputOutOfBoundCellMessage));
                    }
                }
                else
                {
                    Console.WriteLine(Messages.GetMessage(MessageKey.InvalidFormatOfCardMessage));
                }
            }
        }

        internal static int GetIndexOfCard(char i_Char)
        {
            i_Char = char.ToUpper(i_Char);
            return i_Char - 'A';
        }
    }
}
