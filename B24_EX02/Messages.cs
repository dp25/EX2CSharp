using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B24_EX02
{
    public static class Messages
    {
        private static readonly Dictionary<MessageKey, string> s_Messages = new Dictionary<MessageKey, string>
        {
            { MessageKey.GameEndTieMessage, "It's a Tie!" },
            { MessageKey.EnterFirstCardMessage, "Please choose 2 cards.Enter first card you want to choose: " },
            { MessageKey.EnterSecondCardMessage, "Please insert second card:" },
            { MessageKey.GameEndStatisticsFirstMessage, "The Player " },
            { MessageKey.GameEndStatisticsSecondMessage, " got" },
            { MessageKey.GameEndStatisticsThirdMessage, " points" },
            { MessageKey.GameEndWinningMessage, " IS THE WINNER!!!" },
            { MessageKey.InputFirstNamePlayerMessage, "Please enter your name: " },
            { MessageKey.InputBoardWidthMessage, "Please enter board width: " },
            { MessageKey.InputBoardHeightMessage, "Please enter board height: " },
            { MessageKey.NotEvenNumOfCellsMessage, "Number of cells on the board must be even. Try again: " },
            { MessageKey.InvalidInputMessage, "Entered an invalid input. Please try again" },
            { MessageKey.RequestToPlayAgainstComputerMessage, "Do you want to play against the computer? Answer y / n" },
            { MessageKey.ComputerBufferingMessage, "Buffering..." },
            { MessageKey.InvalidChoiseOfCardMessage, "This card cannot be chosen. Try a different card:" },
            { MessageKey.InvalidInputOutOfBoundCellMessage, "This card does not exist. Try a different card: " },
            { MessageKey.InvalidFormatOfCardMessage, "This is not a card. The format: [A-F][1-6]. Try again: " },
            { MessageKey.InputSecondNamePlayerMessage, "Please enter the name of the second player: " },
            { MessageKey.TurnOfPlayer, "'s turn:" },
            { MessageKey.RematchMessage, "Do you want a rematch? please insert y / n" },
            { MessageKey.GameOverMessage, "Game Over!" }
        };

        public static string GetMessage(MessageKey key)
        {
            return s_Messages.TryGetValue(key, out string message) ? message : null;
        }
    }
}
