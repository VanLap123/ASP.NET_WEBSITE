using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEBGROUP_GCC0903.Data;
using WEBGROUP_GCC0903.Models;

namespace WEBGROUP_GCC0903.Controllers
{
    // [Authorize]
    [Authorize(Roles="Admin")]
    public class CategoriesController : Controller
    {
    private readonly ApplicationDbContext _db;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Categories
        // public async Task<IActionResult> Index()
        // {
        //       return _context.Category != null ? 
        //                   View(await _context.Category.ToListAsync()) :
        //                   Problem("Entity set 'ApplicationDbContext.Category'  is null.");
        // }

        public IActionResult Index()
        {
            IEnumerable<Category> CL = _db.Categories.ToList();
            return View(CL);
               // lay danh sach khach hang tu dbcontext
            
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        // GET: Categories/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null || _db.Categories == null)
            {
                return NotFound();
            }

            Category category =  _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.cat_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                category.cat_id = id;
                _db.Categories.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        
        // GET: Categories/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null || _db.Categories == null)
            {
                return NotFound();
            }

            Category obj =_db.Categories.Find(id);
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Categories/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public IActionResult DeleteConfirmed(int id)
        // {
        //     if (_db.Category == null)
        //     {
        //         return Problem("Entity set 'ApplicationDbContext.Category'  is null.");
        //     }
        //     if (category != null)
        //     {
        //         _db.Category.Remove(category);
        //     }
            
        //      _db.SaveChangesAsync();
        //     return RedirectToAction("Index");
        // }

        private bool CategoryExists(int id)
        {
          return (_db.Categories?.Any(e => e.cat_id == id)).GetValueOrDefault();
        }
    }
}
