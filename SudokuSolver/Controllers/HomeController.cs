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
        List<Sudoku> alts = new List<Sudoku>();

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

            //id = ",5,,,,,,,,,,6,,,,2,4,7,4,,,9,7,,6,,,,,,2,4,1,,,6,,9,3,,,,1,2,,1,,,3,8,9,,,,,,5,,8,4,,,3,3,6,2,,,,5,,,,,,,,,,9,";

            sudoku.LockNumbers(id);

            return Json(new { Data = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Solve()
        {
            string result = result = sudoku.Solve();
            
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }

        

        private void Debugging(Sudoku s)
        {
            Debug.WriteLine("I am being called ...");

            foreach (var b in s.Blocks)
            {
                foreach (var c in b.Cells)
                {
                    if (c.PossibleValues.Count == 2 && c.Value == 0)
                    {
                        Debug.WriteLine("Cell ID " + c.Id + " has 2 possible values");
                    }

                }
            }
        }

    }
}