using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoBuy.Models;
namespace GoBuy.Controllers
{
    public class AccountController : Controller
    {
        private GoBuyEntities db = new GoBuyEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        // [HttpPost, ActionName("Login")]
        //public ActionResult Login()
        //{
        //     //要做驗證
        //    Member m = db.Member.Select(m=>m.EnName);
        //    return View();
        //}

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