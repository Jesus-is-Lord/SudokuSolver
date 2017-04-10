using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuSolver.Models
{
    public class Sudoku
    {
        public List<Block> Blocks { get; set; }
        public Sudoku()
        {
            Blocks = new List<Block>();
            List<Column> columns = BuildSudokuColumns();
            List<Row> rows = BuildSudokuRows();

            for(int i=1;i<10;i++)
            {
                Block b = new Block();
                b.Id = i;
                b.Cells = new List<Cell>();
                for(int j=1;j<10;j++)
                {
                    int index = (i * 9) - (9 - j);
                    Cell c = new Cell();
                    c.Id = index;
                    c.Value = 0;
                    c.InBlock = b;
                    c.InSudoku = this;
                    c.Column = columns.Where(cc => cc.CellNumbers.Contains(index)).First();
                    c.Row = rows.Where(cc => cc.CellNumbers.Contains(index)).First();
                    b.Cells.Add(c);
                }
                Blocks.Add(b);
            }
            
        }

        public List<Column> BuildSudokuColumns()
        {
            List<Column> result = new List<Column>();

            Column one = new Column();
            one.Id = 1;
            one.CellNumbers = new List<int> { 1, 4, 7, 28, 31, 34, 55, 58, 61 };
            Column two = new Column();
            two.Id = 2;
            two.CellNumbers = new List<int> { 2, 5, 8, 29, 32, 35, 56, 59, 62 };
            Column three = new Column();
            three.Id = 3;
            three.CellNumbers = new List<int> { 3, 6, 9, 30, 33, 36, 57, 60, 63 };
            Column four = new Column();
            four.Id = 4;
            four.CellNumbers = new List<int> { 10, 13, 16, 37, 40, 43, 64, 67, 70 };
            Column five = new Column();
            five.Id = 5;
            five.CellNumbers = new List<int> { 11, 14, 17, 38, 41, 44, 65, 68, 71 };
            Column six = new Column();
            six.Id = 6;
            six.CellNumbers = new List<int> { 12, 15, 18, 39, 42, 45, 66, 69, 72 };
            Column seven = new Column();
            seven.Id = 7;
            seven.CellNumbers = new List<int> { 19, 22, 25, 46, 49, 52, 73, 76, 79 };
            Column eight = new Column();
            eight.Id = 8;
            eight.CellNumbers = new List<int> { 20, 23, 26, 47, 50, 53, 74, 77, 80 };
            Column nine = new Column();
            nine.Id = 9;
            nine.CellNumbers = new List<int> { 21, 24, 27, 48, 51, 54, 75, 78, 81 };

            result.Add(one);
            result.Add(two);
            result.Add(three);
            result.Add(four);
            result.Add(five);
            result.Add(six);
            result.Add(seven);
            result.Add(eight);
            result.Add(nine);

            return result;
        }

        public List<Row> BuildSudokuRows()
        {
            List<Row> result = new List<Row>();

            Row one = new Row();
            one.Id = 1;
            one.CellNumbers = new List<int> {1,2,3,10,11,12,19,20,21 };
            Row two = new Row();
            two.Id = 2;
            two.CellNumbers = new List<int> { 4, 5, 6, 13, 14, 15, 22, 23, 24 };
            Row three = new Row();
            three.Id = 3;
            three.CellNumbers = new List<int> { 7,8,9,16,17,18,25,26,27 };
            Row four = new Row();
            four.Id = 4;
            four.CellNumbers = new List<int> { 28,29,30,37,38,39,46,47,48 };
            Row five = new Row();
            five.Id = 5;
            five.CellNumbers = new List<int> {31,32,33,40,41,42,49,50,51 };
            Row six = new Row();
            six.Id = 6;
            six.CellNumbers = new List<int> { 34,35,36,43,44,45,52,53,54 };
            Row seven = new Row();
            seven.Id = 7;
            seven.CellNumbers = new List<int> { 55,56,57,64,65,66,73,74,75 };
            Row eight = new Row();
            eight.Id = 8;
            eight.CellNumbers = new List<int> { 58,59,60,67,68,69,76,77,78 };
            Row nine = new Row();
            nine.Id = 9;
            nine.CellNumbers = new List<int> { 61,62,63,70,71,72,79,80,81 };

            result.Add(one);
            result.Add(two);
            result.Add(three);
            result.Add(four);
            result.Add(five);
            result.Add(six);
            result.Add(seven);
            result.Add(eight);
            result.Add(nine);

            return result;
        }

    }
}