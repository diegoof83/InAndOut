using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        //GET
        public IActionResult Index()
        {
            IEnumerable<Expense> expenses = _dbContext.Expenses;
            return View(expenses);
        }

        //GET_CREATE
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoryDropDown = _dbContext.ExpenseCategories.
                Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

            ViewBag.CategoryDropDown = categoryDropDown;

            return View();
        }

        //POST_CREATE
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

        //GET-DELETE
        public IActionResult Delete(int? Id)
        {
            return this.Get(Id);
        }

        //POST_DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            if ((Id == null) || (Id == 0))
            {
                return NotFound();
            }

            Expense expense = _dbContext.Expenses.Find(Id);
            if (expense == null)
            {
                return NotFound();
            }

            _dbContext.Expenses.Remove(expense);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-UPDATE
        public IActionResult Update(int? Id)
        {
            return this.Get(Id);
        }

        //POST_UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Expenses.Update(expense);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        private IActionResult Get(int? Id)
        {
            if ((Id == null) || (Id == 0))
            {
                return NotFound();
            }

            Expense expense = _dbContext.Expenses.Find(Id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }
    }
}
