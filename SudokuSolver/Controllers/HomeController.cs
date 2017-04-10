﻿using SudokuSolver.Models;
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
        public static List<Sudoku> sudokuForks = new List<Sudoku>();
        int iterations;

        public ActionResult Index()
        {
            sudokuForks.Add(new Sudoku());
            return View();
        }

        public ActionResult LockNumbers(string id)
        {
            //for testing purposes - complex
            //id = "8,,,,,3,,7,,,,,6,,,,9,,,,,,,,2,,,,5,,,,,,,,,,7,,4,5,1,,,,,,7,,,,3,,,,1,,,8,,9,,,,,5,,,,,,,6,8,,1,,4,,";
            //for testing purposes - medium
            id = "4,,,,,7,,,,1,,,,,,2,3,,,,,,3,,5,,,8,,,,1,,,,,,,,,,9,8,,,7,,,,,,1,,2,,,3,,6,,,,,7,,2,,,,9,,3,,,,2,4,,6,1,";
            //for testing purposes -easy
            //id = "9,5,,,,,,,8,,1,7,2,,9,4,,,,2,4,5,,,,,9,,,,,,7,,,,3,7,8,,,,6,9,1,,,,1,,,,,,8,,,,,4,7,3,,,,3,7,,5,9,4,,9,,,,,,,5,1";

            sudokuForks.ElementAt(0).LockNumbers(id);

            return Json(new { Data = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Solve()
        {
            string result = sudokuForks.ElementAt(0).Solve();

            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }


        


    }
}