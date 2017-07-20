using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeerflyPatches.Models;
using DeerflyPatches.ViewModels;
using Microsoft.AspNetCore.Http;
using DeerflyPatches.Modules;

namespace DeerflyPatches.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly DeerflyPatchesContext _context;

        public OrderDetailsController(DeerflyPatchesContext context)
        {
            _context = context;    
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderDetail.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail
                .SingleOrDefaultAsync(m => m.ID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PlacedInCart,Quantity,UnitPrice,Shipping,CheckedOut")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail.SingleOrDefaultAsync(m => m.ID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PlacedInCart,Quantity,UnitPrice,Shipping,CheckedOut")] OrderDetail orderDetail)
        {
            if (id != orderDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail
                .SingleOrDefaultAsync(m => m.ID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDetail.SingleOrDefaultAsync(m => m.ID == id);
            _context.OrderDetail.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetail.Any(e => e.ID == id);
        }


        [HttpPost, ActionName("AddOrderDetailToShoppingCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrderDetailToShoppingCart(int id)
        {
            // look up product entity
            Product product = await _context.Product.SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            // TODO: Check if product ID number is currently in cart, if so, simply increment quantity 

            // create new order detail entity
            var newOrderDetail = new OrderDetail()
            {
                Item = product,
                PlacedInCart = DateTime.Now,
                Quantity = 1,
                UnitPrice = product.Price,
                Shipping = product.Shipping
            };

            // Retrieve shopping cart from session
            ShoppingCart shoppingCart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("_shopping_cart");

            // Create new shopping cart if none is in session
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
            }

            // Add new order detail to session
            shoppingCart.Add(newOrderDetail);
            HttpContext.Session.SetObjectAsJson("_shopping_cart", shoppingCart);
            return this.JsonOk();
        }
    }
    
}
