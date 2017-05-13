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
        static Dictionary<string, Sudoku> dictionary;

        public SudokuHub()
        {
            if (dictionary == null)
                dictionary = new Dictionary<string, Sudoku>();
        }

        private void HandleSudokuFailedEvent(object sender, EventArgs e)
        {
            foreach (var d in dictionary)
            {
                if (d.Value.Equals((Sudoku)sender))
                {
                    Clients.Group(d.Key).updateSudokuUIFailed();
                }
            }
        }

        public void HandleSudokuUpdatedEvent(object sender, SudokuUpdatedEventArgs e)
        {
            foreach (var d in dictionary)
            {
                if (d.Value.Equals((Sudoku)sender))
                {
                    Clients.Group(d.Key).updateSudokuUI(e.Sudoku.Solution);
                }
            }
        }

        public void HandleSudokuSolvedEvent(object sender, SudokuUpdatedEventArgs e)
        {
            foreach (var d in dictionary)
            {
                if (d.Value.Equals((Sudoku)sender))
                {
                    Clients.Group(d.Key).updateSudokuUIFinal(e.Sudoku.Solution);
                }
            }
        }

        public void HandleSudokuGeneratedEvent(object sender, SudokuUpdatedEventArgs e)
        {
            foreach (var d in dictionary)
            {
                if (d.Value.Equals((Sudoku)sender))
                {
                    Clients.Group(d.Key).sudokuGenerated(e.Sudoku.Solution);
                }
            }
        }

        public void Solve()
        {
            dictionary[Context.ConnectionId].Solve();
        }

        public void Generate()
        {
            dictionary[Context.ConnectionId].Generate();
        }

        public void Lock(string game)
        {
            dictionary[Context.ConnectionId].LockNumbers(game);
        }

        public override Task OnConnected()
        {
            Sudoku sudoku = new Sudoku();
            sudoku.RaiseSudokuUpdatedEvent += HandleSudokuUpdatedEvent;
            sudoku.RaiseSudokuSolvedEvent += HandleSudokuSolvedEvent;
            sudoku.RaiseSudokuFailedEvent += HandleSudokuFailedEvent;
            sudoku.RaiseSudokuGeneratedEvent += HandleSudokuGeneratedEvent;

            dictionary.Add(Context.ConnectionId, sudoku);
            Groups.Add(Context.ConnectionId, Context.ConnectionId);

            return base.OnConnected();
        }
    }
}