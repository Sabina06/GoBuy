using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoBuy.Models;

namespace GoBuy.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
  
        public ActionResult CategoryMenu()
        {
            GoBuyEntities db = new GoBuyEntities();
            var categories = db.Category.ToList();
            return PartialView(categories);
        }
      
    }
}