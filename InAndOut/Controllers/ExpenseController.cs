using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
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
            IEnumerable<Expense> objList = _dbContext.Expenses;

            foreach (var item in objList)
            {
                item.Category = this.GetCategory(item.ExpenseCategoryId);
            }

            return View(objList);
        }

        //GET_CREATE
        public IActionResult Create()
        {
            return View(this.CreateVM());
        }

        //POST_CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Expenses.Add(obj.Expense);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            return this.Get(id);
        }

        //POST_DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ExpenseVM model)
        {
            int? id = model.Expense.Id;
            if ((id == null) || (id == 0))
            {
                return NotFound();
            }

            Expense obj = _dbContext.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _dbContext.Expenses.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-UPDATE
        public IActionResult Update(int? id)
        {
            return this.Get(id);
        }

        //POST_UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Expenses.Update(obj.Expense);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        private IActionResult Get(int? id)
        {
            if ((id == null) || (id == 0))
            {
                return NotFound();
            }

            ExpenseVM obj = this.CreateVM();
            obj.Expense = _dbContext.Expenses.Find(id);

            if (obj.Expense == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        private ExpenseCategory GetCategory(int id)
        {            
            return _dbContext.ExpenseCategories.FirstOrDefault(x => x.Id.Equals(id));
        }

        private ExpenseVM CreateVM()
        {
            ExpenseVM objVM = new ExpenseVM()
            {
                Expense = new Expense(),
                CategoryDropDown = _dbContext.ExpenseCategories.
                                                Select(x => new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.Id.ToString()
                                                })
            };

            return objVM;
        }
    }
}
