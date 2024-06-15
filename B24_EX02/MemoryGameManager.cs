using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B24_EX02
{
    internal class MemoryGameManager
    {
        internal static void GameManager(Player i_FirstPlayer, Player i_SecondPlayer, bool i_IsRematch)
        {
            MemoryGame<char> currentGame = SetEnviromentMemoryGame.CreateGameEnviroment(i_FirstPlayer, i_SecondPlayer, i_IsRematch);
            startMemoryGame(currentGame);
        }

        private static void startMemoryGame(MemoryGame<char> i_CurrentGame)
        {
            while (i_CurrentGame.NumberOfDiscoveredPairs < (i_CurrentGame.BoardGame.BoardWidth * i_CurrentGame.BoardGame.BoardHeight) / 2)
            {

                MemoryGameTurnHandler.PlayTurn(i_CurrentGame.FirstPlayer, i_CurrentGame);
                if (i_CurrentGame.NumberOfDiscoveredPairs < (i_CurrentGame.BoardGame.BoardWidth * i_CurrentGame.BoardGame.BoardHeight) / 2)
                {
                    MemoryGameTurnHandler.PlayTurn(i_CurrentGame.SecondPlayer, i_CurrentGame);
                }
            }

            displayGameStatistics(i_CurrentGame);
            InputValidation.CheckRematch(i_CurrentGame.FirstPlayer, i_CurrentGame.SecondPlayer);
            Console.WriteLine();
        }

        private static void displayGameStatistics(MemoryGame<char> i_CurrentGame)
        {
            string matchWinnerName = null;
            StringBuilder gameResultStringBuilder = new StringBuilder();
            gameResultStringBuilder.Append(Messages.GetMessage(MessageKey.GameOverMessage));
            gameResultStringBuilder.AppendLine();
            gameResultStringBuilder.AppendFormat("{0} with {1} points",
                i_CurrentGame.FirstPlayer.PlayerName,
                i_CurrentGame.FirstPlayer.PlayerPoints);
            gameResultStringBuilder.AppendLine();
            gameResultStringBuilder.AppendFormat("{0} with {1} points",
                i_CurrentGame.SecondPlayer.PlayerName,
                i_CurrentGame.SecondPlayer.PlayerPoints);

            if (i_CurrentGame.FirstPlayer.PlayerPoints > i_CurrentGame.SecondPlayer.PlayerPoints)
            {
                matchWinnerName = i_CurrentGame.FirstPlayer.PlayerName;
            }
            else
            {
                if (i_CurrentGame.FirstPlayer.PlayerPoints < i_CurrentGame.SecondPlayer.PlayerPoints)
                {
                    matchWinnerName = i_CurrentGame.SecondPlayer.PlayerName;
                }
                else
                {
                    gameResultStringBuilder.AppendLine();
                    gameResultStringBuilder.Append(Messages.GetMessage(MessageKey.TieGameMessage));
                }
            }

            if (matchWinnerName != null)
            {
                gameResultStringBuilder.AppendLine();
                gameResultStringBuilder.AppendFormat("{0}{1}",
                    matchWinnerName,
                    Messages.GetMessage(MessageKey.WinnerMessage));
            }

            Console.WriteLine(gameResultStringBuilder);
        }
    }
}
