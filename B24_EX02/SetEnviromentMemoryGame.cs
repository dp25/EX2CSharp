using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B24_EX02
{
    internal class SetEnviromentMemoryGame
    {
        private static List<char> createSymbolCardsList(int i_NumOfCards)
        {
            if (i_NumOfCards <= 0 || i_NumOfCards % 2 != 0)
            {
                throw new ArgumentException("List length must be a positive even number.", nameof(i_NumOfCards));
            }

            List<char> cardSymbolsList = new List<char>(i_NumOfCards);

            for (int i = 0; i < i_NumOfCards / 2; i++)
            {
                char symbol = (char)('A' + i);
                cardSymbolsList.Add(symbol);
                cardSymbolsList.Add(symbol);
            }

            return cardSymbolsList;
        }

        internal static MemoryGame<char> CreateGameEnviroment(Player i_FirstPlayer, Player i_SecondPlayer, bool i_IsRematch)
        {
            Player firstPlayer = i_FirstPlayer;
            Player secondPlayer = i_SecondPlayer;
            int boardWidth, boardHeight;
            if (!i_IsRematch)
            {
                Console.WriteLine(Messages.GetMessage(MessageKey.InputFirstNamePlayerMessage));
                string firstPlayerName = Console.ReadLine();
                firstPlayer = new Player(firstPlayerName, false);
                secondPlayer = InputValidation.GetSecondPlayer();
            }

            do
            {
                Console.WriteLine(Messages.GetMessage(MessageKey.InputBoardWidthMessage));
                boardWidth = InputValidation.GetBoardDimensions();
                Console.WriteLine(Messages.GetMessage(MessageKey.InputBoardHeightMessage));
                boardHeight = InputValidation.GetBoardDimensions();
                if ((boardHeight * boardWidth) % 2 != 0)
                {
                    Console.WriteLine(Messages.GetMessage(MessageKey.NotEvenNumOfCellsMessage));
                }
            }

            while ((boardHeight * boardWidth) % 2 != 0);

            MemoryGameBoard<char> board = new MemoryGameBoard<char>(boardHeight, boardWidth);
            List<char> symbolCardsList = createSymbolCardsList(boardWidth * boardHeight);
            board.CreateMemoryGameBoard(symbolCardsList);
            MemoryGame<char> currentGame = new MemoryGame<char>(board, firstPlayer, secondPlayer, boardWidth, boardHeight);

            return currentGame;
        }
    }
}
