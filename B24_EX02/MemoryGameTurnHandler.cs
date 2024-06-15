using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B24_EX02
{
    internal class MemoryGameTurnHandler
    {
        private static readonly Random sr_Random = new Random();

        internal static void RevealCards(Player i_Player, MemoryGame<char> i_CurrentGame, MemoryGameCards<char> i_FirstCard, MemoryGameCards<char> i_SecondCard)
        {
            if (i_FirstCard.CardValue == i_SecondCard.CardValue)
            {
                i_Player.PlayerPoints++;
                i_CurrentGame.NumberOfDiscoveredPairs++;
                i_FirstCard.PairOfCardsDiscovered = true;
                i_SecondCard.PairOfCardsDiscovered = true;

                i_CurrentGame.NotRevealedCards.Remove(i_FirstCard);
                i_CurrentGame.NotRevealedCards.Remove(i_SecondCard);

                i_CurrentGame.CardsForComputerAIPlayer.Remove(i_FirstCard);
                i_CurrentGame.CardsForComputerAIPlayer.Remove(i_SecondCard);

                Ex02.ConsoleUtils.Screen.Clear();
                i_CurrentGame.BoardGame.DrawBoardGame();
                System.Threading.Thread.Sleep(2000);
                i_FirstCard.IsCardChosen = false;
                i_SecondCard.IsCardChosen = false;
                Ex02.ConsoleUtils.Screen.Clear();
                i_CurrentGame.BoardGame.DrawBoardGame();
                i_FirstCard.IsFaceUp = true;
                i_SecondCard.IsFaceUp = true;

                PlayTurn(i_Player, i_CurrentGame);
            }
            else
            {
                updateCardPairsForComputer(i_CurrentGame, i_FirstCard, i_SecondCard);
                Ex02.ConsoleUtils.Screen.Clear();
                i_CurrentGame.BoardGame.DrawBoardGame();
                System.Threading.Thread.Sleep(2000);
                i_FirstCard.IsCardChosen = false;
                i_SecondCard.IsCardChosen = false;
                Ex02.ConsoleUtils.Screen.Clear();
                i_CurrentGame.BoardGame.DrawBoardGame();
                i_FirstCard.IsFaceUp = true;
                i_SecondCard.IsFaceUp = true;
            }
        }

        internal static void PlayTurn(Player i_CurrentPlayer, MemoryGame<char> i_CurrentGame)
        {
            MemoryGameCards<char> firstCard = null;
            MemoryGameCards<char> secondCard = null;
            Ex02.ConsoleUtils.Screen.Clear();
            i_CurrentGame.BoardGame.DrawBoardGame();
            Console.WriteLine("{0}\"{1}\"", Messages.GetMessage(MessageKey.TurnOfPlayer), i_CurrentPlayer.PlayerName);
            if (i_CurrentPlayer.IsComputer)
            {
                // Flip coin to choose if computer guess or choose from AI list, to change AI level expend the random range
                int coinResult = sr_Random.Next(0, 2);
                Console.WriteLine(Messages.GetMessage(MessageKey.ComputerThinkMessage));
                System.Threading.Thread.Sleep(3000);
                if (coinResult == 1)
                {
                    computerWithAITurn(i_CurrentPlayer, i_CurrentGame);
                }
                else
                {
                    computerWithoutAITurn(i_CurrentPlayer, i_CurrentGame);
                }
            }
            else
            {
                Console.WriteLine(Messages.GetMessage(MessageKey.ChoseTwoCellsMessage));
                firstCard = InputValidation.GetValidCell(i_CurrentGame);
                Ex02.ConsoleUtils.Screen.Clear();
                i_CurrentGame.BoardGame.DrawBoardGame();
                Console.WriteLine(Messages.GetMessage(MessageKey.SecondCellToOpenMessage));
                secondCard = InputValidation.GetValidCell(i_CurrentGame);
                RevealCards(i_CurrentPlayer, i_CurrentGame, firstCard, secondCard);
            }
        }

        private static void computerWithAITurn(Player i_CurrentPlayer, MemoryGame<char> i_CurrentGame)
        {
            MemoryGameCards<char> firstCardChosen;
            MemoryGameCards<char> secondCardChosen;
            if (i_CurrentGame.CardsForComputerAIPlayer.Count == 0)
            {   // AI list is empty guess first card and check if can chose a match pair with previous data 
                Random cardRandom = new Random();
                int chosenIndexCard = cardRandom.Next(0, i_CurrentGame.NotRevealedCards.Count);
                firstCardChosen = i_CurrentGame.NotRevealedCards[chosenIndexCard];
                int intOfCharValueInRandomCard = InputValidation.GetIntFromCharLetter(firstCardChosen.CardValue);
                if (i_CurrentGame.RevealedCards[intOfCharValueInRandomCard] != null && firstCardChosen != i_CurrentGame.RevealedCards[intOfCharValueInRandomCard])
                {
                    secondCardChosen = i_CurrentGame.RevealedCards[intOfCharValueInRandomCard];
                    firstCardChosen.IsCardChosen = true;
                    secondCardChosen.IsCardChosen = true;
                    RevealCards(i_CurrentPlayer, i_CurrentGame, firstCardChosen, secondCardChosen);
                }
                else
                {
                    // Don't have enough data to play second turn with AI
                    computerWithoutAITurn(i_CurrentPlayer, i_CurrentGame);
                }
            }
            else
            {
                // Don't have enough data to play turn with AI
                firstCardChosen = i_CurrentGame.CardsForComputerAIPlayer[0];
                secondCardChosen = i_CurrentGame.RevealedCards[InputValidation.GetIntFromCharLetter(firstCardChosen.CardValue)];
                firstCardChosen.IsCardChosen = true;
                secondCardChosen.IsCardChosen = true;
                RevealCards(i_CurrentPlayer, i_CurrentGame, firstCardChosen, secondCardChosen);
            }
        }

        private static void computerWithoutAITurn(Player i_CurrentPlayer, MemoryGame<char> i_CurrentGame)
        {
            int chosenIndexCard = sr_Random.Next(0, i_CurrentGame.NotRevealedCards.Count);
            MemoryGameCards<char> firstCardChosen = i_CurrentGame.NotRevealedCards[chosenIndexCard];
            i_CurrentGame.NotRevealedCards.RemoveAt(chosenIndexCard);
            chosenIndexCard = sr_Random.Next(0, i_CurrentGame.NotRevealedCards.Count);
            MemoryGameCards<char> secondCardChosen = i_CurrentGame.NotRevealedCards[chosenIndexCard];
            i_CurrentGame.NotRevealedCards.RemoveAt(chosenIndexCard);
            firstCardChosen.IsCardChosen = true;
            secondCardChosen.IsCardChosen = true;
            RevealCards(i_CurrentPlayer, i_CurrentGame, firstCardChosen, secondCardChosen);
            if (firstCardChosen.CardValue != secondCardChosen.CardValue)
            {
                i_CurrentGame.NotRevealedCards.Add(firstCardChosen);
                i_CurrentGame.NotRevealedCards.Add(secondCardChosen);
                updateCardPairsForComputer(i_CurrentGame, firstCardChosen, secondCardChosen);
            }
        }

        private static void updateCardPairsForComputer(MemoryGame<char> i_CurrentGame, MemoryGameCards<char> i_FirstCard, MemoryGameCards<char> i_SecondCard)
        {
            int indexOfFirstCard = InputValidation.GetIntFromCharLetter(i_FirstCard.CardValue);
            int indexOfSecondCard = InputValidation.GetIntFromCharLetter(i_SecondCard.CardValue);

            if (i_CurrentGame.RevealedCards[indexOfFirstCard] == null)
            {
                i_CurrentGame.RevealedCards[indexOfFirstCard] = i_FirstCard;
            }
            else
            {
                if (i_CurrentGame.RevealedCards[indexOfFirstCard] != i_FirstCard && !i_CurrentGame.NotRevealedCards.Contains(i_FirstCard))
                {
                    i_CurrentGame.NotRevealedCards.Add(i_FirstCard);
                }
            }

            if (i_CurrentGame.RevealedCards[indexOfSecondCard] == null)
            {
                i_CurrentGame.RevealedCards[indexOfSecondCard] = i_SecondCard;
            }
            else
            {
                if (i_CurrentGame.RevealedCards[indexOfSecondCard] != i_SecondCard && !i_CurrentGame.NotRevealedCards.Contains(i_SecondCard))
                {
                    i_CurrentGame.NotRevealedCards.Add(i_SecondCard);
                }
            }
        }
    }
}
