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

        internal MemoryGameCards<T> GetCardValueOnBoard(int i_RowIndex, int i_ColIndex)
        {
                return this.m_GameBoard[i_RowIndex, i_ColIndex];
        }
    }
}
