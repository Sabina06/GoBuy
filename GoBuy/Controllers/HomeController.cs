using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoBuy.Models;
namespace GoBuy.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            GoBuyEntities db = new GoBuyEntities();
            var categories = db.Category.ToList();
            return View(categories);
        }
         //no
        public ActionResult Browse(int categoryId)
        {
            GoBuyEntities db = new GoBuyEntities();
            var categroy = db.Category.Include("Product").
            Single(x => x.CategoryId == categoryId);
            return View(categroy);
           

        }
    }
}