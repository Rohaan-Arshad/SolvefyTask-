using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Solvefy_Task.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solvefy_Task.Controllers
{
    public class UsernewController : Controller
    {
        private readonly solvefyContext context;

        public UsernewController(solvefyContext context)
        {
            this.context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index1()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.mySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else if (HttpContext.Session.GetString("UserSession") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var products = await context.Products.ToListAsync();
            return View(products);
        }

        // Add product to cart
        public IActionResult AddToCart(int productId)
        {
            var product = context.Products.Find(productId);

            if (product != null)
            {
                var cartItemsJson = HttpContext.Session.GetString("CartItems");
                var cartItems = new List<Product>();

                if (cartItemsJson != null)
                {
                    cartItems = JsonConvert.DeserializeObject<List<Product>>(cartItemsJson);
                }

                cartItems.Add(product);
                HttpContext.Session.SetString("CartItems", JsonConvert.SerializeObject(cartItems));
            }

            return RedirectToAction("Index1");
        }
        public IActionResult ViewCart()
        {
            var cartItemsJson = HttpContext.Session.GetString("CartItems");
            var cartItems = new List<Product>();

            if (cartItemsJson != null)
            {
                cartItems = JsonConvert.DeserializeObject<List<Product>>(cartItemsJson);
            }

            return View(cartItems);
        }
        public IActionResult RemoveFromCart(int productId)
        {
            var cartItemsJson = HttpContext.Session.GetString("CartItems");
            var cartItems = JsonConvert.DeserializeObject<List<Product>>(cartItemsJson);

            var productToRemove = cartItems.FirstOrDefault(p => p.Id == productId);
            if (productToRemove != null)
            {
                cartItems.Remove(productToRemove);
                HttpContext.Session.SetString("CartItems", JsonConvert.SerializeObject(cartItems));
            }

            return RedirectToAction("ViewCart");
        }



        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
            }

            return RedirectToAction("Login", "Login");
        }
    }
}
