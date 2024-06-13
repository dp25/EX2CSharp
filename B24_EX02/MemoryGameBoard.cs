using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace B24_EX02
{
    internal class MemoryGameBoard<T>
    {
        private readonly int r_NumOfBoardRows;
        private readonly int r_NumOfBoardColumns;
        private MemoryGameCards<T>[,] m_GameBoard; 
        private static readonly Random sr_RandomCardOnBoard = new Random();

        internal MemoryGameBoard(int i_NumRows,  int i_NumColumns)
        {
            this.r_NumOfBoardRows = i_NumRows;
            this.r_NumOfBoardColumns = i_NumColumns;
            this.m_GameBoard = null;
        }

        internal int BoardWidth
        {
            get 
            { 
                return r_NumOfBoardRows; 
            }
        }

        internal int BoardHeight
        {
            get
            {
                return r_NumOfBoardColumns;
            }
        }

        internal MemoryGameCards<T>[,] GameBoard
        {
            get 
            {
                return m_GameBoard;
            }
        }

        /// <summary>
        /// The method return the value of the card by its position on the board
        /// </summary>
        /// <param name="i_RowIndex"></param>
        /// <param name="i_ColIndex"></param>
        /// <returns> The value of the card on the board by row and column indecies</returns>
        internal MemoryGameCards<T> GetCardValueOnBoard(int i_RowIndex, int i_ColIndex)
        {
                return this.m_GameBoard[i_RowIndex, i_ColIndex];
        }

        /// <summary>
        /// The method randomly places cards i
        /// </summary>
        /// <param name="i_ListOfCard"></param>
        internal void CreateMemoryGameBoard (List<T> i_ListOfCard)
        {
            this.m_GameBoard = new MemoryGameCards<T>[this.r_NumOfBoardRows, this.r_NumOfBoardColumns];

            for (int i = 0; i < this.r_NumOfBoardRows; i++) 
            {
                for (int j = 0; j < this.r_NumOfBoardColumns; j++) 
                {
                    int randomCardIndex = sr_RandomCardOnBoard.Next(0, i_ListOfCard.Count);
                    this.m_GameBoard[i,j] = new MemoryGameCards<T>(i_ListOfCard[randomCardIndex], i,j);
                    i_ListOfCard.RemoveAt(randomCardIndex);
                }
            }
        }

    }
}
