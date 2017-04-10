using System.Collections.Generic;

namespace SudokuSolver.Models
{
    public class Block
    {
        public int Id { get; set; }
        public List<Cell> Cells { get; set; }
    }
}