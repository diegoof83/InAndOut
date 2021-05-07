using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Item> items = _dbContext.Items;
            return View(items);
        }

        //GET_Create
        public IActionResult Create()
        {
            return View();
        }

        //POST_Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item newItem)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Items.Add(newItem);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newItem);
        }
    }
}
