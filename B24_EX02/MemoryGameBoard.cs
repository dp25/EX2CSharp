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

        /// <summary>
        /// The memory board constructor.
        /// Initializes a new instance of the MemoryGameBoard class with specified dimensions.
        /// </summary>
        /// <param name="i_NumRows">Number of rows for the game board</param>
        /// <param name="i_NumColumns">Number of columns for the game board</param>
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
        /// <param name="i_RowIndex">Row index of the card</param>
        /// <param name="i_ColIndex">Column index of the card</param>
        /// <returns> The value of the card on the board by row and column indices</returns>
        internal MemoryGameCards<T> GetCardValueOnBoard(int i_RowIndex, int i_ColIndex)
        {
            if (i_RowIndex < 0 ||  i_ColIndex < 0 || i_RowIndex >= r_NumOfBoardRows || i_ColIndex >= r_NumOfBoardColumns)
            {
                throw new ArgumentOutOfRangeException("Card indices are out of the board's bound range.");
            }
            return this.m_GameBoard[i_RowIndex, i_ColIndex];
        }

        /// <summary>
        /// The method randomly places cards on the game board.
        /// After placing a card on the board the method removes the card from the card list.
        /// </summary>
        /// <param name="i_ListOfCard">List of cards values to be placed on the board</param>
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

        /// <summary>
        /// Draws the current status of the board
        /// </summary>
        internal void DrawBoardGame()
        {
            StringBuilder memoryGameBoard = new StringBuilder();
            memoryGameBoard.Append("     ");
            for (char col = 'A'; col < 'A' + r_NumOfBoardColumns; col++)
            {
                memoryGameBoard.Append($"  {col}   ");
            }
            int lengthOfBoardRow = memoryGameBoard.Length;
            int numOfSpaces = 5;
            memoryGameBoard.AppendLine();
            memoryGameBoard.Append("     ");
            memoryGameBoard.Append('=', lengthOfBoardRow - numOfSpaces);
            memoryGameBoard.AppendLine();

            for (int row = 1; row <= this.r_NumOfBoardRows; row++) 
            {
                memoryGameBoard.Append($"{row}  |"); 

                for (int col = 0;  col < r_NumOfBoardColumns; col++)
                {
                    if (this.m_GameBoard[row, col].PairOfCardsDiscovered || this.m_GameBoard[row,col].IsFaceUp) 
                    {
                        memoryGameBoard.AppendFormat("  {0}", this.m_GameBoard[row,col].Cardvalue);
                        memoryGameBoard.Append("  |");
                    }
                    else
                    {
                        memoryGameBoard.Append("     |");
                    }
                }

                memoryGameBoard.AppendLine();
                memoryGameBoard.Append("     ");
                memoryGameBoard.Append('=', lengthOfBoardRow - numOfSpaces);
                memoryGameBoard.AppendLine();
            }

            Console.WriteLine(memoryGameBoard.ToString());
        }

    }
}
