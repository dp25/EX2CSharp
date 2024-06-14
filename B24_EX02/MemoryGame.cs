using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B24_EX02
{
    internal class MemoryGame<T>
    {
        private readonly MemoryGameBoard<T> r_BoardGame;
        private readonly List<MemoryGameCards<T>> r_NotRevealedCards;
        private readonly List<MemoryGameCards<T>> r_CardsForComputer;
        private readonly MemoryGameCards<T>[] r_RevealedCards;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private int m_NumberOfDiscoveredPairs;

        internal MemoryGame(MemoryGameBoard<T> i_BoardGameMatrix, Player i_FirstPlayer, Player i_SecondPlayer, int i_NumRows, int i_NumColumns)
        {
            r_BoardGame = i_BoardGameMatrix;
            m_FirstPlayer = i_FirstPlayer;
            m_SecondPlayer = i_SecondPlayer;
            m_NumberOfDiscoveredPairs = 0;
            r_RevealedCards = new MemoryGameCards<T>[((i_NumRows * i_NumColumns) / 2)];
            r_NotRevealedCards = getCardsForComputerPlayer();
            r_CardsForComputer = new List<MemoryGameCards<T>>();


        }

        internal Player FirstPlayer
        {
            get
            {
                return m_FirstPlayer;
            }

            set
            {
                m_FirstPlayer = value;
            }
        }

        internal Player SecondPlayer
        {
            get
            {
                return m_SecondPlayer;
            }

            set
            {
                m_SecondPlayer = value;
            }
        }

        internal MemoryGameBoard<T> BoardGame
        {
            get
            {
                return r_BoardGame;
            }
        }

        internal int NumberOfDiscoveredPairs
        {
            get
            {
                return m_NumberOfDiscoveredPairs;
            }

            set
            {
                m_NumberOfDiscoveredPairs = value;
            }
        }

        internal List<MemoryGameCards<T>> NotRevealedCards
        {
            get
            {
                return r_NotRevealedCards;
            }
        }

        internal MemoryGameCards<T>[] RevealedCards
        {
            get
            {
                return r_RevealedCards;
            }
        }

        internal List<MemoryGameCards<T>> CardsForComputer
        {
            get
            {
                return r_CardsForComputer;
            }
        }

        private List<MemoryGameCards<T>> getCardsForComputerPlayer()
        {
            var cardsForComputer = new List<MemoryGameCards<T>>();
            foreach (var currentCard in r_BoardGame.GameBoard)
            {
                cardsForComputer.Add(currentCard);
            }

            return cardsForComputer;
        }
    }
}
