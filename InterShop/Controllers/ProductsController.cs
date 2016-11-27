using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InterShop;
using InterShop.Models;
using PagedList.Mvc;
using PagedList;
using Microsoft.AspNet.Identity;

namespace InterShop.Controllers
{
    public class ProductsController : Controller
    {
        SQLEXPRESSEntities db = new SQLEXPRESSEntities();
        // public List<Cart> carts;
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }



        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            //HttpCookie cookie = new HttpCookie("role");
            //Response.Cookies.Add(cookie);
            //Response.Cookies["role"].Value =
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Products/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,Name,Description,Category,Price")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }

            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View("Edit", products);
        }

        // POST: Products/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductID,Name,Description,Category,Price")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("IndexPage");
            }
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Products products = await db.Products.FindAsync(id);
            db.Products.Remove(products);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexPage");
        }

        public ActionResult IndexPage(int? page)
        {

            if (User.Identity.GetUserName() == "Admin@admin.com")
            {
                return View("AdminShowProducts", db.Products);
            }
            else {

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                var list = new List<Products>(db.Products);
                return View(list.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult AddToBasket(int id,int qvantity)
        {
            

            Cart c = new Cart();

            c.ClientEmail = User.Identity.GetUserName();
            c.ProductID = id;
            c.ProductName = db.Products.Find(id).Name;
            c.Price = db.Products.Find(id).Price;
            c.DatePurchased = DateTime.Now.Date;
            c.Qvantity = qvantity;
            db.Cart.Add(c);        
            db.SaveChanges();
            return RedirectToAction("IndexPage");
        }
        
        public ActionResult ViewBasket()
        {
            string id = User.Identity.GetUserName();
            var cart = db.Cart.Where(c => c.ClientEmail == id);
            return View("ViewBasket", cart);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
