using System.Collections.Generic;

namespace SudokuSolver.Models
{
    public class Column
    {
        public int Id { get; set; }
        public List<int> CellNumbers { get; set; }
    }
}