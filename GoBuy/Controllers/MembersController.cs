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
namespace GoBuy.Controllers
{
    public class MembersController : Controller
    {
        private GoBuyEntities db = new GoBuyEntities();

        // GET: Members
        public ActionResult Index()
        {
            IndexViewModel index = new IndexViewModel();
            index.member = db.Member.ToList();
            return View(index);
            //return RedirectToAction("MemberList");

        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Memno,EnName,Account,Password")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Member.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (member.EnName == null)
                ModelState.AddModelError("Member", "請輸入");

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Memno,EnName,Account,Password")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Member.Find(id);
            db.Member.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult generateStateList(int CountryID)
        {
            StringBuilder sb = new StringBuilder();

            var cc = from sss in db.City
                     where sss.CountryID == CountryID
                     select sss;

            foreach (var item in cc)
            {
                sb.Append("<option value='" + item.CityID + "'>" + item.CityName + "</option>");
            }


            return Content(sb.ToString(), "text/html");
        }
       
        [ChildActionOnly]
        public ActionResult GenerateCountry()
        {
            var countries = db.Country;
            //country
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var country in countries)
            {
                items.Add(new SelectListItem()
                {
                    Text = country.CountryName,
                    Value = country.CountryID.ToString()

                });
            }
            ViewBag.CountryItems = items;
            //city
            var cities = db.City;
            List<SelectListItem> items2 = new List<SelectListItem>();
            foreach (var city in cities)
            {
                items2.Add(new SelectListItem()
                {
                    Text = city.CityName,
                    Value = city.CityID.ToString()

                });
            }
            ViewBag.CityItems =  items2;


            return PartialView();
        }

      [HttpGet]
        public ActionResult ShowSecondDropDownList(string FirstLevel)
        {        
            
            //------------
            //city
            //List<SelectListItem> items2 = new List<SelectListItem>();
            //var cities = db.City;
            //foreach (var city in cities.)
            //{
            //    items2.Add(new SelectListItem()
            //    {
            //        Text = city.CityName,
            //        Value = city.CityID.ToString()

            //    });
            //}
            //var items2 = (from c in db.City
            //             where c.CountryID.ToString() == FirstLevel
            //             select new SelectListItem { Text = c.CityName, Value = c.CityID.ToString() }).ToList();

            //ViewBag.CityItems = items2;
            //return PartialView("GenerateCountry");
          //------------
          StringBuilder sb = new StringBuilder();
          var items2 = from c in db.City where c.CountryID.ToString() ==FirstLevel select c;
          foreach (var cities in items2)
          {
              sb.AppendFormat("<option value='{0}'>{1}</option>",cities.CityID,cities.CityName);   
          }
          return Content(sb.ToString(),"text/html");
        
        }
    }
}
