using System.Collections.Generic;

namespace SudokuSolver.Models
{
    public class Row
    {
        public int Id { get; set; }
        public List<int> CellNumbers { get; set; }
    }
}