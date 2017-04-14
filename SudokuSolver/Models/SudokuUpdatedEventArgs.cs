using System;

namespace SudokuSolver.Models
{
    public class SudokuUpdatedEventArgs : EventArgs
    {
        public SudokuUpdatedEventArgs(Sudoku s)
        {
            sudoku = s;
        }

        private Sudoku sudoku;

        public Sudoku Sudoku
        {
            get { return sudoku; }
            set { sudoku = value; }
        }
    }

}