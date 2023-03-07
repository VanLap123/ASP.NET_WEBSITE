using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WEBGROUP_GCC0903.Data;
using WEBGROUP_GCC0903.Models;

namespace WEBGROUP_GCC0903.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ApplicationDbContext _db;

        public CartController(ILogger<CartController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db=db;

        }

        
            /// add proruct to card
        [Route ("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart ([FromRoute] int productid) {

            var product = _db.Products
                .Where (p => p.pro_id == productid)
                .FirstOrDefault ();
            if (product == null)
                return NotFound ("There are not product");
            // process put into cart
            var cart = GetCartItems ();
            var cartitem = cart.Find (p => p.product.pro_id == productid);
            if (cartitem != null) {
                // already exists, increase by 1
                cartitem.quantity++;
            } else {
                //  add new
                cart.Add (new CartItem () { quantity = 1, product = product });
            }

    // save cart into Session
    SaveCartSession (cart);
    // come to cart page
    return RedirectToAction (nameof (Cart));
    }


    /// remove item in cart
    [Route ("/removecart/{productid:int}", Name = "removecart")]
    public IActionResult RemoveCart ([FromRoute] int productid) {
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.product.pro_id == productid);
        if (cartitem != null) {
            
            cart.Remove(cartitem);
        }

        SaveCartSession (cart);
        return RedirectToAction (nameof (Cart));
    }


    /// Cập nhật
    [Route ("/updatecart", Name = "updatecart")]
    [HttpPost]
    public IActionResult UpdateCart ([FromForm] int productid, [FromForm] int quantity) {
        // update quantity of cart
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.product.pro_id == productid);
        if (cartitem != null) {
          
            cartitem.quantity = quantity;
        }
        SaveCartSession (cart);
        
        return Ok();
    }


    // display cartcart
    [Route ("/cart", Name = "cart")]
    public IActionResult Cart () {
        return View (GetCartItems());
    }



    [Route("/checkout")]
    public IActionResult CheckOut()
    {
        // processing when order product
       return View(GetCartItems());
    }
    
     [Route("/checkout")]
     [HttpPost]
        public IActionResult CheckOut(string country,string first_name,string last_name,string address,string city,string phone_number,string email_address)
        {
            try{
            Order order = new Order()
            {
                country = country,
                cus_first_name = first_name,
                cus_last_name = last_name,
                cus_address = address,
                cus_city = city,
                cus_phone = phone_number,
                cus_email=email_address,
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
            
            }catch(Exception ex){

            }
            return Redirect("~/Home/Index");
        }
        
    
    
    // save json string of cart
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

    // remove cart from session
    void ClearCart () {
        var session = HttpContext.Session;
        session.Remove (CARTKEY);
    }

    // save Cart (list of CartItem) into session
    void SaveCartSession (List<CartItem> ls) {
        var session = HttpContext.Session;
        string jsoncart = JsonConvert.SerializeObject (ls);
        session.SetString (CARTKEY, jsoncart);
    }


       
    }
}
