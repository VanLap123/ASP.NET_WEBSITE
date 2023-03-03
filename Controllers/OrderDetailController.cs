using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEBGROUP_GCC0903.Data;
using WEBGROUP_GCC0903.Models;

namespace WEBGROUP_GCC0903.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderDetail
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

       

        // GET: OrderDetail/Create
        public IActionResult Create()
        {
            ViewData["order_id"] = new SelectList(_context.Orders, "order_id", "order_id");
            ViewData["pro_id"] = new SelectList(_context.Products, "pro_id", "pro_id");
            return View();
        }

        // POST: OrderDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("order_id,quantity,pro_id")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["order_id"] = new SelectList(_context.Orders, "order_id", "order_id", orderDetail.order_id);
            ViewData["pro_id"] = new SelectList(_context.Products, "pro_id", "pro_id", orderDetail.pro_id);
            return View(orderDetail);
        }

       

      

        private bool OrderDetailExists(int id)
        {
          return (_context.OrderDetails?.Any(e => e.order_id == id)).GetValueOrDefault();
        }
    }
}
