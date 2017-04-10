using SudokuSolver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuSolver.Controllers
{
    public class HomeController : Controller
    {
        public static Sudoku sudoku;
        int iterations;

        public ActionResult Index()
        {
            sudoku = new Sudoku();
            return View(sudoku);
        }

        public ActionResult LockNumbers(string id)
        {
            //for testing purposes - complex
            id = "8,,,,,3,,7,,,,,6,,,,9,,,,,,,,2,,,,5,,,,,,,,,,7,,4,5,1,,,,,,7,,,,3,,,,1,,,8,,9,,,,,5,,,,,,,6,8,,1,,4,,";
            //for testing purposes - medium
            //id = "4,,,,,7,,,,1,,,,,,2,3,,,,,,3,,5,,,8,,,,1,,,,,,,,,,9,8,,,7,,,,,,1,,2,,,3,,6,,,,,7,,2,,,,9,,3,,,,2,4,,6,1,";
            //for testing purposes -easy
            id = "9,5,,,,,,,8,,1,7,2,,9,4,,,,2,4,5,,,,,9,,,,,,7,,,,3,7,8,,,,6,9,1,,,,1,,,,,,8,,,,,4,7,3,,,,3,7,,5,9,4,,9,,,,,,,5,1";

            string[] values = id.Split(',');
            foreach (var b in sudoku.Blocks)
            {
                foreach (var c in b.Cells)
                {
                    if (!values[c.Id - 1].Equals(""))
                        c.Value = Convert.ToInt16(values[c.Id - 1]);
                }
            }

            return Json(new { Data = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Solve()
        {
            bool solved = true;
            do
            {
                solved = true;
                foreach (var b in sudoku.Blocks)
                {
                    foreach (var c in b.Cells)
                    {
                        solved = c.Solve() && solved;
                    }
                }
                iterations++;
                if (iterations>30)
                {
                    return SolveApproachTwo();
                }
            } while (!solved);

            string result = "";
            foreach (var b in sudoku.Blocks)
            {
                foreach (var c in b.Cells)
                {
                    result = result + "," + c.Value;
                }
            }

            var r = Json(new { Data = result }, JsonRequestBehavior.AllowGet);
            return r;
        }

        public ActionResult SolveApproachTwo()
        {
            Debug.WriteLine("SolveApproachTwo() is being called again...");
            bool foundAnchor = false;

            foreach (var b in sudoku.Blocks)
            {
                foreach (var c in b.Cells)
                {
                    if (c.PossibleValues.Count > 1 && c.Value == 0)
                        Debug.WriteLine("Cell ID " + c.Id + " has " + c.PossibleValues.Count + " possible solutions.");
                    if (c.PossibleValues.Count ==0 && c.Value == 0)
                        Debug.WriteLine("Error with solution. Cell ID " + c.Id + " cannot be set");

                    if (c.PossibleValues.Count == 2 && c.Value == 0)
                    {
                        c.Value = c.PossibleValues.ElementAt(1);
                        foundAnchor = true;
                        break;
                    }
                }
                if (foundAnchor)
                    break;
            }

            return Solve();
        }



    }
}