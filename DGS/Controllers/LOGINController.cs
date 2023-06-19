using DGS.Context;
using DGS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DGS.Controllers
{
    public class LOGINController : Controller
    {
        // GET: LOGIN
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult login()
        {
            if (string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return Redirect("/");
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult login(LOGIN log)
        {
            YETKILI_CTX ytx = new YETKILI_CTX();
            var a = ytx.yetkiliDondurByMail(log.EMAIL);
            ViewBag.User = log.EMAIL;
            ViewBag.password = log.PAROLA;
            //mail adresi tabloda var mı kontrolü yaptık
            if (a != null)
            {
                //Mail adresi parola ile uyuşuyor mu kontrolü yaptıkhh
                if (a.PAROLA == log.PAROLA)
                {
                    log.yeti = a;
                    FormsAuthentication.SetAuthCookie(log.EMAIL, true);
                    return RedirectToAction("Index","Home");
                }
            }
            return View();
        }
        public ActionResult cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }

        public ActionResult kisiResim()
        {
            YETKILI_CTX ytx = new YETKILI_CTX();
            var a = ytx.yetkiliDondurByMail(HttpContext.User.Identity.Name.ToString());
            return PartialView(a);
        }
    }
}