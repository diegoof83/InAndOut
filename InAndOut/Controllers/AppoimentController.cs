﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class AppoimentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}