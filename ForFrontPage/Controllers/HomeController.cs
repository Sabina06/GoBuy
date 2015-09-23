using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ForFrontPage.Models;
using System.Data.Entity;
namespace ForFrontPage.Controllers
{
    public class HomeController : Controller
    {
        private ForFrontPageEntities db = new ForFrontPageEntities();
        public ActionResult Index()
        {
            return View("DataUsage");
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
        
       
        public ActionResult getProduct()
        {
            using (ForFrontPageEntities context = new ForFrontPageEntities()) {
                //context.ContextOptions.LazyLoadingEnabled = false;
          //  this._context.ContextOptions.LazyLoadingEnabled = false;
            var movies = new List<object>();
            var product = db.Member.ToList(); 

            //movies.Add(new { EnName = "Ghostbusters", Genre = "Comedy", Year = 1984 });
            //movies.Add(new { EnName = "Gone with Wind", Genre = "Drama", Year = 1939 });
            //movies.Add(new { EnName = "Star Wars", Genre = "Science Fiction", Year = 1977 });

            return Json(product, "application/json", JsonRequestBehavior.AllowGet);
           
            //return Content(JsonConvert.SerializeObject(product), "application/json");
}
        }
    }
}