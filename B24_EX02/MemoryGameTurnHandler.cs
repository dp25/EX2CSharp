﻿using System;
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

                updateDisplayCardsAndBoard(i_CurrentGame, i_FirstCard, i_SecondCard);

                if (i_CurrentGame.NumberOfDiscoveredPairs < (i_CurrentGame.BoardGame.BoardWidth * i_CurrentGame.BoardGame.BoardHeight) / 2)
                {
                    PlayTurn(i_Player, i_CurrentGame);
                }
            }
            else
            {
                updatePairsForComputer(i_CurrentGame, i_FirstCard, i_SecondCard);
                updateDisplayCardsAndBoard(i_CurrentGame, i_FirstCard, i_SecondCard);
            }
        }

        private static void updateDisplayCardsAndBoard(MemoryGame<char> i_CurrentGame, MemoryGameCards<char> i_FirstCard, MemoryGameCards<char> i_SecondCard)
        {
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

        internal static void PlayTurn(Player i_CurrentPlayer, MemoryGame<char> i_CurrentGame)
        {
            MemoryGameCards<char> firstCard = null;
            MemoryGameCards<char> secondCard = null;
            Ex02.ConsoleUtils.Screen.Clear();
            i_CurrentGame.BoardGame.DrawBoardGame();
            Console.WriteLine("{0}{1}", i_CurrentPlayer.PlayerName, Messages.GetMessage(MessageKey.TurnOfPlayer));
            if (i_CurrentPlayer.IsComputer)
            {
                int coinResult = sr_Random.Next(0, 4);
                Console.WriteLine(Messages.GetMessage(MessageKey.ComputerBufferingMessage));
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
                Console.WriteLine(Messages.GetMessage(MessageKey.EnterFirstCardMessage));
                firstCard = InputValidation.GetValidCell(i_CurrentGame);
                Ex02.ConsoleUtils.Screen.Clear();
                i_CurrentGame.BoardGame.DrawBoardGame();
                Console.WriteLine("{0}{1}", i_CurrentPlayer.PlayerName, Messages.GetMessage(MessageKey.TurnOfPlayer));
                Console.WriteLine(Messages.GetMessage(MessageKey.EnterSecondCardMessage));
                secondCard = InputValidation.GetValidCell(i_CurrentGame);
                RevealCards(i_CurrentPlayer, i_CurrentGame, firstCard, secondCard);
            }
        }

        private static void computerWithAITurn(Player i_CurrentPlayer, MemoryGame<char> i_CurrentGame)
        {
            MemoryGameCards<char> firstCardByComputerAI;
            MemoryGameCards<char> secondCardChosen;
            if (i_CurrentGame.CardsForComputerAIPlayer.Count == 0)
            {   
                Random randomCard = new Random();
                int chosenRandomCard = randomCard.Next(0, i_CurrentGame.NotRevealedCards.Count);
                firstCardByComputerAI = i_CurrentGame.NotRevealedCards[chosenRandomCard];
                int indexOfRandomCard = InputValidation.GetIndexOfCard(firstCardByComputerAI.CardValue);
                if (i_CurrentGame.RevealedCards[indexOfRandomCard] != null && firstCardByComputerAI != i_CurrentGame.RevealedCards[indexOfRandomCard])
                {
                    secondCardChosen = i_CurrentGame.RevealedCards[indexOfRandomCard];
                    firstCardByComputerAI.IsCardChosen = true;
                    secondCardChosen.IsCardChosen = true;
                    RevealCards(i_CurrentPlayer, i_CurrentGame, firstCardByComputerAI, secondCardChosen);
                }
                else
                {
                    computerWithoutAITurn(i_CurrentPlayer, i_CurrentGame);
                }
            }
            else
            {
                firstCardByComputerAI = i_CurrentGame.CardsForComputerAIPlayer[0];
                secondCardChosen = i_CurrentGame.RevealedCards[InputValidation.GetIndexOfCard(firstCardByComputerAI.CardValue)];
                firstCardByComputerAI.IsCardChosen = true;
                secondCardChosen.IsCardChosen = true;
                RevealCards(i_CurrentPlayer, i_CurrentGame, firstCardByComputerAI, secondCardChosen);
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
                updatePairsForComputer(i_CurrentGame, firstCardChosen, secondCardChosen);
            }
        }

        private static void updatePairsForComputer(MemoryGame<char> i_CurrentGame, MemoryGameCards<char> i_FirstCard, MemoryGameCards<char> i_SecondCard)
        {
            int firstCardIndex = InputValidation.GetIndexOfCard(i_FirstCard.CardValue);
            int secondCardIndex = InputValidation.GetIndexOfCard(i_SecondCard.CardValue);

            if (i_CurrentGame.RevealedCards[firstCardIndex] == null)
            {
                i_CurrentGame.RevealedCards[firstCardIndex] = i_FirstCard;
            }
            else
            {
                if (i_CurrentGame.RevealedCards[firstCardIndex] != i_FirstCard && !i_CurrentGame.NotRevealedCards.Contains(i_FirstCard))
                {
                    i_CurrentGame.NotRevealedCards.Add(i_FirstCard);
                }
            }

            if (i_CurrentGame.RevealedCards[secondCardIndex] == null)
            {
                i_CurrentGame.RevealedCards[secondCardIndex] = i_SecondCard;
            }
            else
            {
                if (i_CurrentGame.RevealedCards[secondCardIndex] != i_SecondCard && !i_CurrentGame.NotRevealedCards.Contains(i_SecondCard))
                {
                    i_CurrentGame.NotRevealedCards.Add(i_SecondCard);
                }
            }
        }
    }
}
