using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B24_EX02
{
    internal class MemoryGameCards<T>
    {
        private readonly T r_CardValue;
        private readonly int r_CardRowIndex;
        private readonly int r_CardColumnIndex;
        private bool m_CardIsFaceUp;
        private bool m_PairOfCardsDiscovered;
        private bool m_IsCardChosen;

        internal MemoryGameCards(T i_CardValue, int i_CardRowIndex, int i_CardColumnIndex) 
        {
            r_CardValue = i_CardValue;
            r_CardRowIndex = i_CardRowIndex;
            r_CardColumnIndex = i_CardColumnIndex;
            this.m_CardIsFaceUp = false;
            this.m_PairOfCardsDiscovered = false;
            this.m_IsCardChosen = false;
        }

        internal T CardValue
        {
            get 
            { 
                return r_CardValue; 
            }
        }

        internal int CardRowIndex
        { 
            get 
            { 
                return r_CardRowIndex; 
            } 
        }

        internal int CardColumnIndex
        {
            get 
            { 
                return r_CardColumnIndex;
            }
        }

        internal bool PairOfCardsDiscovered
        {
            get 
            {
                return m_PairOfCardsDiscovered;
            }

            set
            {
                m_PairOfCardsDiscovered = value;
            }
        }

        internal bool IsFaceUp
        {
            get 
            {
                return m_CardIsFaceUp;
            }

            set 
            {
                m_CardIsFaceUp = value;
            }
        }

        internal bool IsCardChosen
        {
            get
            {
                return m_IsCardChosen;
            }

            set
            {
                m_IsCardChosen = value;
            }
        }
    }
}
