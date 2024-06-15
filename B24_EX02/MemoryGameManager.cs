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

            printGameStatistics(i_CurrentGame);
            InputValidation.CheckRematch(i_CurrentGame.FirstPlayer, i_CurrentGame.SecondPlayer);
            Console.WriteLine();
        }

        private static void printGameStatistics(MemoryGame<char> i_CurrentGame)
        {
            string matchWinnerName = null;
            StringBuilder gameResultStringBuilder = new StringBuilder();
            gameResultStringBuilder.Append(Messages.GetMessage(MessageKey.GameEndMessage));
            gameResultStringBuilder.AppendLine();
            gameResultStringBuilder.AppendFormat("{0}\"{1}\"{2} {3}{4}",
                Messages.GetMessage(MessageKey.GameEndStatisticsFirstMessage),
                i_CurrentGame.FirstPlayer.PlayerName,
                Messages.GetMessage(MessageKey.GameEndStatisticsSecondMessage),
                i_CurrentGame.FirstPlayer.PlayerPoints,
                Messages.GetMessage(MessageKey.GameEndStatisticsThirdMessage));
            gameResultStringBuilder.AppendLine();
            gameResultStringBuilder.AppendFormat("{0}\"{1}\"{2} {3}{4}",
                Messages.GetMessage(MessageKey.GameEndStatisticsFirstMessage),
                i_CurrentGame.SecondPlayer.PlayerName,
                Messages.GetMessage(MessageKey.GameEndStatisticsSecondMessage),
                i_CurrentGame.SecondPlayer.PlayerPoints,
                Messages.GetMessage(MessageKey.GameEndStatisticsThirdMessage));

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
                    gameResultStringBuilder.Append(Messages.GetMessage(MessageKey.GameEndTieMessage));
                }
            }

            if (matchWinnerName != null)
            {
                gameResultStringBuilder.AppendLine();
                gameResultStringBuilder.AppendFormat("{0}\"{1}\"{2}",
                    Messages.GetMessage(MessageKey.GameEndStatisticsFirstMessage),
                    matchWinnerName,
                    Messages.GetMessage(MessageKey.GameEndWinningMessage));
            }

            Console.WriteLine(gameResultStringBuilder);
        }
    }
}
