using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChefsNDishes.Models;

namespace ChefsNDishes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;  
    }

    public IActionResult Index()
    {
        List<Chef> AllChefs = _context.Chefs.Include(e => e.AllDishes).ToList();
        ViewBag.AllChefs = AllChefs;
        
        return View();
    }

    [HttpGet("chefs/new")]
    public IActionResult AddChef()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateChef(Chef chefFromForm)
    {
        if (ModelState.IsValid)
        {
            _context.Add(chefFromForm);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("AddChef");
    }

    [HttpGet("dishes/new")]
    public IActionResult AddDish() {
        List<Chef> AllChefs = _context.Chefs.ToList();
        ViewBag.AllChefs = AllChefs;
        return View();
    }

    [HttpPost]
    public IActionResult CreateDish(Dish dishFromForm) {
        if (ModelState.IsValid) {
            _context.Add(dishFromForm);
            _context.SaveChanges();
            return RedirectToAction("Dishes");
        }

        List<Chef> AllChefs = _context.Chefs.ToList();
        ViewBag.AllChefs = AllChefs;
        
        return View("AddDish");
    }

    [HttpGet]
    public IActionResult Dishes() {
        List<Dish> AllDishes = _context.Dishes.Include(e => e.Chef).ToList();
        ViewBag.AllDishes = AllDishes;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
