using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeerflyPatches.Models;
using Microsoft.AspNetCore.Http;
using DeerflyPatches.Modules;
using DeerflyPatches.ViewModels;

namespace DeerflyPatches.Controllers
{
    public class AddressesController : Controller
    {
        private readonly DeerflyPatchesContext _context;

        public AddressesController(DeerflyPatchesContext context)
        {
            _context = context;    
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Address.ToListAsync());
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .SingleOrDefaultAsync(m => m.ID == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Recipient,Address1,Address2,City,State,Zip,Country,Phone")] Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address.SingleOrDefaultAsync(m => m.ID == id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Recipient,Address1,Address2,City,State,Zip,Country,Phone")] Address address)
        {
            if (id != address.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.ID))
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
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .SingleOrDefaultAsync(m => m.ID == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.Address.SingleOrDefaultAsync(m => m.ID == id);
            _context.Address.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.ID == id);
        }

        [HttpPost, ActionName("UpdateShippingAddress")]
        public IActionResult UpdateShippingAddress(IFormCollection data)
        {

            Address shippingAddress = new Address();
            shippingAddress.Recipient = Request.Form["ship-to"];
            shippingAddress.Address1 = Request.Form["address1"];
            shippingAddress.Address2 = Request.Form["address2"];
            shippingAddress.City = Request.Form["city"];
            shippingAddress.State = Request.Form["state"];
            shippingAddress.Zip = Request.Form["zip"];
            shippingAddress.Country = Request.Form["country"];
            shippingAddress.Phone = Request.Form["phone"];

            // Get shopping cart from session
            ShoppingCart shoppingCart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("_shopping_cart");
            shoppingCart.GetOrder().ShipTo = shippingAddress;
            HttpContext.Session.SetObjectAsJson("_shopping_cart", shoppingCart);

            // TODO: Save address to model


            return this.JsonOk();
        }
    }
}
