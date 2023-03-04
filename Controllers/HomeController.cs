using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBGROUP_GCC0903.Data;
using WEBGROUP_GCC0903.Models;

namespace WEBGROUP_GCC0903.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index(string SearchString = "")
    {
        try{
            if (SearchString != "")
            {
                var Products = _db.Products.Include(s => s.category).Where(x => x.pro_name.ToUpper().Contains(SearchString.ToUpper()));
                return View(Products.ToList());
            }
        }
        catch(Exception ex){
            
        }
       
    
        IEnumerable<Product> lstPro = _db.Products.ToList();
        return View(lstPro);
    }
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
