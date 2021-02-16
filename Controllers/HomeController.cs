using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tweetcabulary.Models;

namespace Tweetcabulary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Usage()
        {
            return View();
        }

        public IActionResult BadEntry()
        {
            return View();
        }

        [HttpPost("UserHandle")]
        public IActionResult UserAnalysis(string userHandle, [FromServices]ITwitAPI twitService, [FromServices]ISpellCheck spellService)
        {
            UserAnalysis u = new UserAnalysis(userHandle, twitService, spellService);
            if(u.IsValid())
            {
                return View(u);
            } 
            else
            {
                return View("BadEntry");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
