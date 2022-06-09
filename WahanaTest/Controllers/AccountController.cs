using Penjualan.Helper;
using Penjualan.Service;
using Penjualan.Service.Interface;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Penjualan.Controllers
{
    public class AccountController : Controller
    {
        //AdvanceUMSEntities db = new AdvanceUMSEntities();
        private readonly GeneralHelper dHelper = new GeneralHelper();

        private readonly ISetting setting;
        private readonly IUsers user;

        public AccountController() : this(new SetttingService(), new UserService())
        {
        }

        public AccountController(ISetting setting, IUsers user)
        {
            this.setting = setting;
            this.user = user;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Login()
        {
            var data = await setting.Get();
            if (setting != null)
            {
                ViewBag.Name = data.nama;
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel objLogin)
        {
            HttpCookie cookie1 = new HttpCookie("User");
            //var password = dHelper.Encrypt(objLogin.Password);
            //var userLogin = db.tblUsers.FirstOrDefault(s =>
            //    s.Email == objLogin.Email || s.UserName == objLogin.Email && s.Password == password);
            var userLogin = await user.login(objLogin.Email, objLogin.Password);
            if (userLogin != null)
            {
                cookie1.Values.Add("username", userLogin.username.ToString());
                cookie1.Expires = DateTime.Now.AddHours(5);
                Response.Cookies.Add(cookie1);
                return Redirect("~/admin/dashboard");
            }
            else
            {
                ViewBag.Message = "<div class=\"alert alert-danger fade in\">Invalid Username and password.</div>.";
                return View();
            }
        }

        //public ActionResult ForgotPassword()
        //{
        //    var setting = db.tblSettings.FirstOrDefault();
        //    if (setting != null)
        //    {
        //        ViewBag.SiteLogo = setting.LogoPath;
        //        ViewBag.Name = setting.Name;
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ForgotPassword(LoginViewModel objLogin)
        //{
        //    var userLogin = db.tblUsers.FirstOrDefault(s => s.Email == objLogin.Email || s.UserName == objLogin.Email);
        //    if (userLogin != null)
        //    {
        //        var returnValue = dMailHelper.SendMail(userLogin.Email, "Subject", "Body");
        //        if (returnValue == "Successful")
        //            ViewBag.Message =
        //                "<div class=\"alert alert-success fade in\">Password sent to your email address.</div>.";
        //        else
        //            ViewBag.Message =
        //                "<div class=\"alert alert-danger fade in\">" + returnValue + "</div>.";
        //        return View();
        //    }
        //    else
        //    {
        //        ViewBag.Message = "<div class=\"alert alert-danger fade in\">Invalid Username and password.</div>.";
        //        return View();
        //    }
        //}

        //Logout user
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            Response.Cookies["User"].Expires = DateTime.MinValue;
            Response.Cookies.Clear();
            HttpCookie cookie1 = new HttpCookie("User");
            cookie1.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie1);
            return RedirectToAction("Login", "Account");
        }
    }
}