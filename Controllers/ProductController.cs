using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WEBGROUP_GCC0903.Data;
using WEBGROUP_GCC0903.Models;
using WEBGROUP_GCC0903.Service;

namespace WEBGROUP_GCC0903.Controllers
{
    [Authorize(Roles="Admin")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IFileService _fileService;
       

        public ProductController(ILogger<ProductController> logger, ApplicationDbContext db, IFileService fileService)
        {
            _logger = logger;
            _db = db;
            _fileService = fileService;
        }

        // GET: Product
        public IActionResult Index()
        {
            // lay danh sach khach hang tu dbcontext
            IEnumerable<Product> ds = _db.Products.ToList();
            return View(ds);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Products == null)
            {
                return NotFound();
            }

            var product = await _db.Products
                .Include(p => p.category)
                .FirstOrDefaultAsync(m => m.pro_id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["cat_id"] = new SelectList(_db.Categories, "cat_id", "cat_name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var result = _fileService.SaveImage(product.ImageFile);
                product.pro_image = result.Item2;
                _db.Add(product);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["cat_id"] = new SelectList(_db.Categories, "cat_id", "cat_id", product.cat_id);
            return View(product);
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _db.Products == null)
            {
                return NotFound();
            }

            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["cat_id"] = new SelectList(_db.Categories, "cat_id", "cat_id", product.cat_id);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.pro_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ImageFile != null)
                    {
                        var result = _fileService.SaveImage(product.ImageFile);
                        if (result.Item1 == 1)
                        {
                            var oldImage = product.pro_image;
                            product.pro_image = result.Item2;
                            _fileService.DeleteImage(oldImage);
                        }
                    }
                    _db.Update(product);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.pro_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["cat_id"] = new SelectList(_db.Categories, "cat_id", "cat_id", product.cat_id);
            return View(product);
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int id)
        {
            Product product = _db.Products.Find(id);
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        private bool ProductExists(int id)
        {
            return (_db.Products?.Any(e => e.pro_id == id)).GetValueOrDefault();
        }



        /// Thêm sản phẩm vào cart
        [Route ("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart ([FromRoute] int productid) {

            var product = _db.Products
                .Where (p => p.pro_id == productid)
                .FirstOrDefault ();
            if (product == null)
                return NotFound ("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems ();
            var cartitem = cart.Find (p => p.product.pro_id == productid);
            if (cartitem != null) {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            } else {
                //  Thêm mới
                cart.Add (new CartItem () { quantity = 1, product = product });
            }

    // Lưu cart vào Session
    SaveCartSession (cart);
    // Chuyển đến trang hiện thị Cart
    return RedirectToAction (nameof (Cart));
    }


    /// xóa item trong cart
    [Route ("/removecart/{productid:int}", Name = "removecart")]
    public IActionResult RemoveCart ([FromRoute] int productid) {
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.product.pro_id == productid);
        if (cartitem != null) {
            // Đã tồn tại, tăng thêm 1
            cart.Remove(cartitem);
        }

        SaveCartSession (cart);
        return RedirectToAction (nameof (Cart));
    }


    /// Cập nhật
    [Route ("/updatecart", Name = "updatecart")]
    [HttpPost]
    public IActionResult UpdateCart ([FromForm] int productid, [FromForm] int quantity) {
        // Cập nhật Cart thay đổi số lượng quantity ...
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.product.pro_id == productid);
        if (cartitem != null) {
            // Đã tồn tại, tăng thêm 1
            cartitem.quantity = quantity;
        }
        SaveCartSession (cart);
        // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
        return Ok();
    }


    // Hiện thị giỏ hàng
    [Route ("/cart", Name = "cart")]
    public IActionResult Cart () {
        return View (GetCartItems());
    }



    [Route("/checkout")]
    public IActionResult CheckOut()
    {
        // Xử lý khi đặt hàng
       return View(GetCartItems());
    }
    
     [Route("/checkout")]
        [HttpPost]
        public IActionResult CheckOut(string CustName, string Telephone, string Address)
        {
            Order order = new Order()
            {
                cus_name = CustName,
                deliveryLocal = Address,
                cus_phone = Telephone,
                OrderDate = DateTime.Now,
            };
            _db.Orders.Add(order);
            _db.SaveChanges();
            int orderId = order.order_id;
            foreach (var cartItem in GetCartItems())
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    order_id = orderId,
                    pro_id = cartItem.product.pro_id,
                    quantity = cartItem.quantity,
                };
                _db.OrderDetails.Add(orderDetail);
                _db.SaveChanges();
            }
            ClearCart();
            return View("Index");
        }
        
    
    
    // Key lưu chuỗi json của Cart
    public const string CARTKEY = "cart";

    // Lấy cart từ Session (danh sách CartItem)
    List<CartItem> GetCartItems () {

        var session = HttpContext.Session;
        string jsoncart = session.GetString (CARTKEY);
        if (jsoncart != null) {
            return JsonConvert.DeserializeObject<List<CartItem>> (jsoncart);
        }
        return new List<CartItem> ();
    }

    // Xóa cart khỏi session
    void ClearCart () {
        var session = HttpContext.Session;
        session.Remove (CARTKEY);
    }

    // Lưu Cart (Danh sách CartItem) vào session
    void SaveCartSession (List<CartItem> ls) {
        var session = HttpContext.Session;
        string jsoncart = JsonConvert.SerializeObject (ls);
        session.SetString (CARTKEY, jsoncart);
    }

    }
}
