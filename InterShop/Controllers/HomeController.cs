using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterShop.Models;
using PagedList.Mvc;
using PagedList;

namespace InterShop.Controllers
{
    public class HomeController : Controller
    {
        SQLEXPRESSEntities db = new SQLEXPRESSEntities();

        public ActionResult Index()
        {
            return View();
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
        public ActionResult AdminShowProducts()
        {
          
            return View("AdminShowProducts",db.Products);
        }

        public ActionResult IndexPage(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var list = new List<Products>(db.Products);           
            return View(list.ToPagedList(pageNumber, pageSize));
        }
    }
}