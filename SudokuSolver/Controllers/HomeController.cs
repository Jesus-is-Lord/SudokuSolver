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

        public ActionResult Index()
        {
            sudoku = new Sudoku();
            return View();
        }

        public ActionResult LockNumbers(string id)
        {
            //for testing purposes - complex
            id = "8,,,,,3,,7,,,,,6,,,,9,,,,,,,,2,,,,5,,,,,,,,,,7,,4,5,1,,,,,,7,,,,3,,,,1,,,8,,9,,,,,5,,,,,,,6,8,,1,,4,,";
            //for testing purposes - medium
            //id = "4,,,,,7,,,,1,,,,,,2,3,,,,,,3,,5,,,8,,,,1,,,,,,,,,,9,8,,,7,,,,,,1,,2,,,3,,6,,,,,7,,2,,,,9,,3,,,,2,4,,6,1,";
            //for testing purposes -easy
            //id = "9,5,,,,,,,8,,1,7,2,,9,4,,,,2,4,5,,,,,9,,,,,,7,,,,3,7,8,,,,6,9,1,,,,1,,,,,,8,,,,,4,7,3,,,,3,7,,5,9,4,,9,,,,,,,5,1";

            sudoku.LockNumbers(id);

            return Json(new { Data = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Solve()
        {
            string result = sudoku.Solve();
            
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }

        private List<Sudoku> TryLuckWithChild(Sudoku s)
        {
            Debug.WriteLine("I am being called ...");

            List<Sudoku> result = new List<Sudoku>();

            bool foundAnchor = false;
            int idOfAnchorCell = 0;
            int firstValue = 0; ;
            int secondValue = 0;
            foreach (var b in s.Blocks)
            {
                foreach (var c in b.Cells)
                {
                    if (c.PossibleValues.Count == 2 && c.Value == 0)
                    {
                        Debug.WriteLine("Cell ID " + c.Id + " has 2 possible values");
                        foundAnchor = true;
                        firstValue = c.PossibleValues.ElementAt(0);
                        secondValue = c.PossibleValues.ElementAt(1);
                        idOfAnchorCell = c.Id;
                        c.Value = firstValue;
                        result.Add(s);
                        break;
                    }
                    if (c.PossibleValues.Count == 0 && c.Value == 0)
                        throw new Exception();
                }
                if (foundAnchor)
                    break;
            }

            if (foundAnchor)
            {

                Sudoku child = (Sudoku)s.Clone();
                foreach (var b in child.Blocks)
                {
                    foreach (var c in b.Cells)
                    {
                        if (c.Id == idOfAnchorCell)
                        {
                            c.Value = secondValue;
                        }
                    }
                }
                result.Add(child);
            }

            return result;
        }



    }
}