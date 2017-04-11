using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Models
{
    public class Cell
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public Column Column { get; set; }
        public Row Row { get; set; }
        public Block InBlock { get; set; }
        public Sudoku InSudoku { get; set; }

        public List<int> PossibleValues {
            get
            {
                List<int> result = new List<int>();
                if (this.Value != 0)
                {
                    result.Add(this.Value);
                }
                else
                {
                    //possible values by looking at block
                    List<int> fullSetOfValues = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    List<int> possibleValuesFromBlock = new List<int>();
                    foreach (int p in fullSetOfValues)
                        if (!this.InBlock.Cells.Any(c => c.Value == p) )
                            possibleValuesFromBlock.Add(p);
                    //possible values by looking at column
                    List<int> possibleValuesFromColumn = new List<int>();
                    List<int> columnValues = new List<int>();
                    foreach(var c in this.Column.CellNumbers)
                    {
                        foreach(var b in InSudoku.Blocks)
                        {
                            foreach (var cc in b.Cells)
                            {
                                if (cc.Id == c && cc.Value != 0)
                                    columnValues.Add(cc.Value);
                            }
                        }
                    }
                    foreach (int p in fullSetOfValues)
                        if (!columnValues.Any(c => c == p))
                            possibleValuesFromColumn.Add(p);
                    //possible values by looking at row
                    List<int> possibleValuesFromRow = new List<int>();
                    List<int> rowValues = new List<int>();
                    foreach (var c in this.Row.CellNumbers)
                    {
                        foreach (var b in InSudoku.Blocks)
                        {
                            foreach (var cc in b.Cells)
                            {
                                if (cc.Id == c && cc.Value != 0)
                                    rowValues.Add(cc.Value);
                            }
                        }
                    }
                    foreach (int p in fullSetOfValues)
                        if (!rowValues.Any(c => c == p))
                            possibleValuesFromRow.Add(p);

                    //do a three-way inner join to get the final possible values
                    var q1 = from f_value in possibleValuesFromBlock
                                join s_value in possibleValuesFromColumn on f_value equals s_value
                                select f_value;
                    var q2 = from f_value in q1
                             join s_value in possibleValuesFromRow on f_value equals s_value
                             select f_value;
                    result.AddRange(q2);

                }
                return result;
            }
        }

        public void Solve()
        {
            if ((PossibleValues.Count == 1 && this.Value==0))
            {
                this.InSudoku.AtLeastOneCellSolved = true;
                this.Value = PossibleValues.ElementAt(0);
            }
        }
    }
}