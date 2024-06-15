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
            { MessageKey.RematchMessage, "Would you like to play another match? please insert y / n" },
            { MessageKey.GameEndMessage, "The Game End!" },
            { MessageKey.GameEndTieMessage, "The game finish on tie" },
            { MessageKey.GameEndStatisticsFirstMessage, "The Player " },
            { MessageKey.GameEndStatisticsSecondMessage, " got" },
            { MessageKey.GameEndStatisticsThirdMessage, " points" },
            { MessageKey.GameEndWinningMessage, " won the game!" },
            { MessageKey.InputNameMessage, "Please enter your name: " },
            { MessageKey.InputBoardWidthMessage, "Please enter the board width: " },
            { MessageKey.InputBoardHeightMessage, "Please enter the board height: " },
            { MessageKey.InputNeedHaveEvenCellsMessage, "The number of cells needs to be even" },
            { MessageKey.InvalidInputMessage, "You entered an invalid input, please try again" },
            { MessageKey.AskIfSecondPlayerIsComputerMessage, "Do you want to play against the computer? Answer y / n" },
            { MessageKey.ComputerThinkMessage, "The computer thinks!" },
            { MessageKey.ChoseTwoCellsMessage, "Please choose two cells: " },
            { MessageKey.SecondCellToOpenMessage, "Please choose second cell to open" },
            { MessageKey.InvalidInputChosenOpenedCellMessage, "You choose an open cell, please choose another" },
            { MessageKey.InvalidInputOutOfBoundCellMessage, "Your chosen cell is out of board game, please choose another cell" },
            { MessageKey.InvalidInputWrongFormatCellFirstMessage, "Your input is incorrect, please enter your chosen cell in the following " },
            { MessageKey.InputSecondNamePlayerMessage, "Please enter second player name" },
            { MessageKey.TurnOfPlayer, "This is the turn of " }
        };

        public static string GetMessage(MessageKey key)
        {
            return s_Messages.TryGetValue(key, out string message) ? message : null;
        }
    }
}
