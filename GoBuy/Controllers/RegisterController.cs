using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoBuy.ViewModel;
using GoBuy.Models;
using System.Web.Security;
namespace GoBuy.Controllers
{
    public class RegisterController : Controller
    {
        private GoBuyEntities db = new GoBuyEntities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Memno,EnName,Account,Password,Email")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.roleID = 1;
                member.Enable = "Y";
                member.RegDate = DateTime.Now;
                db.Member.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }


        public ActionResult Login()
        { return View(); }
        [HttpPost]
        public ActionResult Login(RegisterViewModel RegisterModel)
        {
            string account = RegisterModel.Account;
            string password = RegisterModel.Password;
            if (account == "" || password == "")
            {
                ModelState.AddModelError("", "請輸入正確的帳號或密碼!");
                return View();
            }


            var systemuser = db.Member

                .FirstOrDefault(x => x.Account == RegisterModel.Account);

            if (systemuser == null)
            {
                ModelState.AddModelError("", "請輸入正確的帳號或密碼!");
                return View();
            }
            //var password = CryptographyPassword(RegisterModel.Password, BaseController.PasswordSalt);

            //var password = CryptographyPassword(RegisterModel.Password, "");
            if (systemuser.Password.Equals(password))
            {
                this.LoginProcess(systemuser, RegisterModel.Remember);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "請輸入正確的帳號或密碼!");
                return View();
            }
        }
        protected string CryptographyPassword(string password, string salt)
        {
            // string cryptographyPassword =  FormsAuthentication.Decrypt(password);
            string cryptographyPassword = password;
            // FormsAuthentication.HashPasswordForStoringInConfigFile(password + salt, "sha1");

            return cryptographyPassword;
        }
        private void LoginProcess(Member user, bool isRemeber)
        {
            var now = DateTime.Now;
            string roles = user.SystemRoles.ToString();

            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: user.Memno.ToString(),
                issueDate: now,
                expiration: now.AddMinutes(30),
                isPersistent: isRemeber,
                userData: roles,
                cookiePath: FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            //清除所有的 session
            Session.RemoveAll();

            //建立一個同名的 Cookie 來覆蓋原本的 Cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            //建立 ASP.NET 的 Session Cookie 同樣是為了覆蓋
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            return RedirectToAction("Index", "Home");
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