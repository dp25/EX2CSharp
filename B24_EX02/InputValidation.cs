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
            Console.WriteLine(Messages.GetMessage(MessageKey.AskIfSecondPlayerIsComputerMessage));

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
            while (true)
            {
                string input = Console.ReadLine();

                if (input == "Q")
                {
                    Environment.Exit(0);
                }

                if (input.Length != 2)
                {
                    Console.WriteLine(Messages.GetMessage(MessageKey.InvalidInputWrongFormatCellFirstMessage));
                    continue; 
                }

                char colChar = input[0];
                char rowChar = input[1];

                if (!(char.IsDigit(rowChar) && char.IsUpper(colChar)))
                {
                    Console.WriteLine(Messages.GetMessage(MessageKey.InvalidInputWrongFormatCellFirstMessage));
                    continue;
                }

                int rowIndex = rowChar - '1';
                int colIndex = colChar - 'A';

                if (rowIndex < 0 || rowIndex >= i_CurrentGame.BoardGame.BoardHeight || colIndex < 0 || colIndex >= i_CurrentGame.BoardGame.BoardWidth)
                {
                    Console.WriteLine(Messages.GetMessage(MessageKey.InvalidInputOutOfBoundCellMessage));
                    continue;
                }

                MemoryGameCards<char> chosenCell = i_CurrentGame.BoardGame.GetCardValueOnBoard(rowIndex, colIndex);

                if (chosenCell.PairOfCardsDiscovered)
                {
                    Console.WriteLine(Messages.GetMessage(MessageKey.InvalidInputChosenOpenedCellMessage));
                    continue;
                }

                chosenCell.IsCardChosen = true;
                return chosenCell;
            }
        }

        internal static int GetIntFromCharLetter(char i_CurrentChar)
        {
            i_CurrentChar = char.ToUpper(i_CurrentChar);
            return i_CurrentChar - 'A';
        }
    }
}
