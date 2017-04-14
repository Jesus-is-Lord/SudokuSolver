using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SudokuSolver.Models;
using System.Threading.Tasks;

namespace SudokuSolver.Hubs
{
    public class SudokuHub : Hub
    {
        public static Sudoku sudoku;

        public void Hello()
        {
            Clients.All.hello();
        }

        public SudokuHub()
        {
            if(sudoku==null)
                sudoku = new Sudoku();
            sudoku.RaiseSudokuUpdatedEvent += HandleSudokuUpdatedEvent;
            sudoku.RaiseSudokuSolvedEvent += HandleSudokuSolvedEvent;
        }

        public void HandleSudokuUpdatedEvent(object sender, SudokuUpdatedEventArgs e)
        {
            Clients.All.updateSudokuUI(e.Sudoku.Solution);
        }

        public void HandleSudokuSolvedEvent(object sender, SudokuUpdatedEventArgs e)
        {
            Clients.All.updateSudokuUIFinal(e.Sudoku.Solution);
        }

        public void Solve()
        {
            sudoku.Solve();
        }

        public void Lock(string game)
        {
            sudoku.LockNumbers(game);
        }
    }
}