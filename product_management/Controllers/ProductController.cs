using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using product_management.Data;
using product_management.Models;

namespace product_management.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDbContext _dbContext;
        private readonly IDistributedCache distributedCache;
        public ProductController(ProductDbContext dbContext, IDistributedCache distributedCache)
        {
            _dbContext = dbContext;
            this.distributedCache = distributedCache;    
        }

        public IActionResult Index()
        {
            //IEnumerable<Product> products = _dbContext.Products.ToList();
            //return View(products);
            var products = new List<Product>();
            if (string.IsNullOrEmpty(distributedCache.GetString("products")))
            {
                products = _dbContext.Products.ToList();
                var productsString = JsonConvert.SerializeObject(products);
                distributedCache.SetString("products", productsString);
            }
            else
            {
                var productsFromCache = distributedCache.GetString("products");
                products = JsonConvert.DeserializeObject<List<Product>>(productsFromCache);
            }
            IEnumerable<Product> productssent = products;
            return View(productssent);

        }
        [HttpPost]
       public IActionResult Index(string search)
       {
            Console.WriteLine("Hello");
            Console.WriteLine(search);
            IEnumerable<Product> products = _dbContext.Products.Where(d => d.Name.Contains(search)).ToList();    
            return View(products);
        }

     

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                distributedCache.Remove("products");
                // Cập nhật cache bằng danh sách sản phẩm mới
                var products = _dbContext.Products.ToList();
                var productsString = JsonConvert.SerializeObject(products);
                distributedCache.SetString("products", productsString);
                return RedirectToAction("Index");
            }
            return View(product);
        }
        
        public IActionResult Update(int? id)
        {
            var product = _dbContext.Products.Find(id);
            if (id == null || product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Products.Update(product);
                _dbContext.SaveChanges();
                distributedCache.Remove("products");
                // Cập nhật cache bằng danh sách sản phẩm mới
                var products = _dbContext.Products.ToList();
                var productsString = JsonConvert.SerializeObject(products);
                distributedCache.SetString("products", productsString);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public JsonResult GetById(int id)
        {
            var product = _dbContext.Products.Find(id);
            var data = new { name = product.Name };
            return Json(data);
        }
        public IActionResult GetAllProduct()
        {
            var products = _dbContext.Products.ToList();
            return PartialView("ProductTable", products);
        }
        public ActionResult Delete(int id)
        {

            var product = _dbContext.Products.Find(id);
            _dbContext.Remove(product);
            _dbContext.SaveChanges();
            distributedCache.Remove("products");
            // Cập nhật cache bằng danh sách sản phẩm mới
            var products = _dbContext.Products.ToList();
            var productsString = JsonConvert.SerializeObject(products);
            distributedCache.SetString("products", productsString);
            return Json(new { success = true });
        }
    }
}
