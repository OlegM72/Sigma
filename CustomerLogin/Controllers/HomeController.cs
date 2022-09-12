using CustomerLogin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace CustomerLogin.Controllers
{
    // [Authorize(Roles = "CUSTOMER")] // ... "ADMIN,RECEPTIONIST,COACH")] // do not allow this page without authorization; allow for customer role
                                       // but in this case Login page is authomatically called (don't know where this default behaviour is in the code)
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Customer")] // ... "ADMIN,RECEPTIONIST,COACH")] // do not allow this page without authorization; allow for customer role 
        // [Authorize] // do not allow this page without authorization; allow for ANY role 
        public IActionResult Privacy()
        {
            return View();
            /* Authorize(Roles = "Customer") also can be done this way:
             * 
            var user = HttpContext.User.Identity; // var current_User = userManager.GetUserAsync(HttpContext.User);
            if (user.IsAuthenticated) // if user logged in and is CUSTOMER (if Auth. requires Customer, see Program.cs)
                return View(); // Show Privacy page
            else
                // return View("Denied"); // just show Denied page (with links to login and register pages) but don't change the URL in the address string
                return RedirectToAction("Denied", "Home"); // in this case we MOVE to the Denied page
            */
        }

        [AllowAnonymous]
        public IActionResult Denied()
        {
           
           // return RedirectToAction("Index", "Home");  // we can move to Home page here
           return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}