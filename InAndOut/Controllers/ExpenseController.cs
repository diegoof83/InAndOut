using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ExpenseController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Expense> expenses = _dbContext.Expenses;
            return View(expenses);
        }

        //GET_Create
        public IActionResult Create()
        {
            return View();
        }

        //POST_Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Expense newExpense)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Expenses.Add(newExpense);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newExpense);
        }
    }
}
