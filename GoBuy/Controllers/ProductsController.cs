using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoBuy.Models;
using GoBuy.ViewModel;
using System.Text;
using System.Collections;
namespace GoBuy.Controllers
{
    public class ProductsController : Controller
    {
        private GoBuyEntities db = new GoBuyEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Product.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var categories = db.Category;
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var category in categories)
            {
                items.Add(new SelectListItem()
                {
                    Text = category.CategoryName,
                    Value = category.CategoryId.ToString()

                });
            }
            ViewBag.CategoryItems = items;

            return View();
            
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }


        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            var categories = db.Category;

            //Dictionary<string, string> dict = new Dictionary<string, string>();
            //foreach (var category in categories)
            //{
            //    dict.Add(category.CategoryName, category.CategoryName);
            //}
            //ViewData["CategoryDDL"] = DropDownListHelper.GetDropdownList(
            //    "CategoryDDL", dict, new { id = "CategoryDDL" }, collection["CotegiryDDL"] ?? null,
            //    true, "請選擇分類"
            //    );
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);

            if (product == null)
            {

                return HttpNotFound();
            }


            SelectList selectList = new SelectList(categories, "CategoryId", "CategoryName", product.CategoryId);
            ProductEditViewModel viewmodel = new ProductEditViewModel()
            {
                Product = product,
                CategoryList = selectList
            };
            ViewBag.CategorySelectList = selectList;
            
            return View("Edit2", viewmodel);
            //    return View("Edit", product);
           
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");


            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "ProductId,Name,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [ChildActionOnly]

        public ActionResult CategoryMenu()
        {
            GoBuyEntities db = new GoBuyEntities();
            var categories = db.Category.ToList();
            return PartialView(categories);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Browse(int categoryId)
        {
            GoBuyEntities db = new GoBuyEntities();
            var categroy = db.Category.Include("Product").
            Single(x => x.CategoryId == categoryId);
            return View(categroy);


        }

    }
}
