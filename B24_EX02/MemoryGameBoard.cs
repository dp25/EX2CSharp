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
            r_NumOfBoardRows = i_NumRows;
            r_NumOfBoardColumns = i_NumColumns;
            m_GameBoard = null;
        }

        internal int BoardWidth
        {
            get 
            { 
                return r_NumOfBoardColumns; 
            }
        }

        internal int BoardHeight
        {
            get
            {
                return r_NumOfBoardRows;
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

        internal void DrawBoardGame()
        {
            StringBuilder memoryGameBoard = new StringBuilder();
            memoryGameBoard.Append("    ");
            for (char col = 'A'; col < 'A' + r_NumOfBoardColumns; col++)
            {
                memoryGameBoard.Append($"  {col}   ");
            }
            int lengthOfBoardRow = memoryGameBoard.Length;
            int numOfSpaces = 3;
            memoryGameBoard.AppendLine();
            memoryGameBoard.Append("   ");
            memoryGameBoard.Append('=', lengthOfBoardRow - numOfSpaces);
            memoryGameBoard.AppendLine();

            for (int row = 0; row < this.r_NumOfBoardRows; row++) 
            {
                memoryGameBoard.Append($"{row + 1}  |"); 

                for (int col = 0; col < r_NumOfBoardColumns; col++)
                {
                    if (this.m_GameBoard[row, col].PairOfCardsDiscovered || this.m_GameBoard[row,col].IsCardChosen) 
                    {
                        memoryGameBoard.AppendFormat("  {0}", this.m_GameBoard[row,col].CardValue);
                        memoryGameBoard.Append("  |");
                    }
                    else
                    {
                        memoryGameBoard.Append("     |");
                    }
                }

                memoryGameBoard.AppendLine();
                memoryGameBoard.Append("   ");
                memoryGameBoard.Append('=', lengthOfBoardRow - numOfSpaces);
                memoryGameBoard.AppendLine();
            }

            Console.WriteLine(memoryGameBoard.ToString());
        }

    }
}
