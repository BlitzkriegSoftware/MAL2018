﻿using Microsoft.AspNetCore.Mvc;

namespace customerwebapi.Controllers
{
    /// <summary>
    /// Home
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Index => Swagger
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Redirect("~/swagger");
        }

    }
}