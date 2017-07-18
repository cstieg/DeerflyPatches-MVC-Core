using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeerflyPatches.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using DeerflyPatches.ViewModels;

namespace DeerflyPatches.Controllers
{
    public class ProductsController : Controller
    {
        private IHostingEnvironment _env;
        private readonly DeerflyPatchesContext _context;
        private string[] validImageTypes = new string[]
        {
            "image/gif",
            "image/jpeg",
            "image/png"
        };

        public ProductsController(IHostingEnvironment env, DeerflyPatchesContext context)
        {
            _context = context;
            _env = env;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            // Pass ViewModel instead of Model
            return View(new ViewModels.ProductVM());
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Price,Shipping,ImageUpload,Category")] ViewModels.ProductVM product)
        {
            // validate image upload
            if (product.ImageUpload == null || product.ImageUpload.Length == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!validImageTypes.Contains(product.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                // Copy ViewModel info to Model
                var newProduct = new Product()
                {
                    ID = product.ID,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Shipping = product.Shipping,
                    Category = product.Category
                };

                // Save image to disk and store filepath in model
                if (product.ImageUpload != null && product.ImageUpload.Length != 0)
                {
                    var imageDir = "/images";
                    var imagePath = Path.Combine(_env.WebRootPath, "images", product.ImageUpload.FileName);
                    //var imageUrl = Path.Combine(imageDir, product.ImageUpload.FileName);
                    var imageUrl = imageDir + "/" + product.ImageUpload.FileName;
                    product.ImageUpload.CopyTo(new FileStream(imagePath, FileMode.Create));
                    newProduct.ImageURL = imageUrl;
                }

                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            // Copy Model info to ViewModel
            var productVM = new ProductVM()
            {
                ID = product.ID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Shipping = product.Shipping,
                Category = product.Category
            };

            return View(productVM);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Price,Shipping,ImageUpload,Category")] ProductVM product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            // validate image upload
            if (product.ImageUpload == null || product.ImageUpload.Length == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!validImageTypes.Contains(product.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }


            if (ModelState.IsValid)
            {
                // Copy ViewModel info to Model
                var editedProduct =_context.Product.Find(id);
                editedProduct.Name = product.Name;
                editedProduct.Description = product.Description;
                editedProduct.Price = product.Price;
                editedProduct.Shipping = product.Shipping;
                editedProduct.Category = product.Category;

                // Save image to disk and store filepath in model
                if (product.ImageUpload != null && product.ImageUpload.Length != 0)
                {
                    var imageDir = "/images";
                    var imagePath = Path.Combine(_env.WebRootPath, "images", product.ImageUpload.FileName);
                    //var imageUrl = Path.Combine(imageDir, product.ImageUpload.FileName);
                    var imageUrl = imageDir + "/" + product.ImageUpload.FileName;
                    product.ImageUpload.CopyTo(new FileStream(imagePath, FileMode.Create));
                    editedProduct.ImageURL = imageUrl;
                }

                try
                {
                    _context.Update(editedProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.SingleOrDefaultAsync(m => m.ID == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ID == id);
        }
    }
}
