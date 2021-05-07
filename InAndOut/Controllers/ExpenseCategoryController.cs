using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ExpenseCategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET
        public IActionResult Index()
        {
            IEnumerable<ExpenseCategory> categories = _dbContext.ExpenseCategories;
            return View(categories);
        }

        //GET_CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST_CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseCategory newExpenseCategory)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ExpenseCategories.Add(newExpenseCategory);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newExpenseCategory);
        }

        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            return this.Get(id);
        }

        //POST_DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if ((id == null) || (id == 0))
            {
                return NotFound();
            }

            ExpenseCategory category = _dbContext.ExpenseCategories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _dbContext.ExpenseCategories.Remove(category);
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
        public IActionResult Update(ExpenseCategory category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ExpenseCategories.Update(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        private IActionResult Get(int? id)
        {
            if ((id == null) || (id == 0))
            {
                return NotFound();
            }

            ExpenseCategory category = _dbContext.ExpenseCategories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
    }
}
