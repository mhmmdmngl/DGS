using DGS.Context;
using DGS.Filter;
using DGS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DGS.Controllers
{
    public class DENEMELERController : Controller
    {
        // GET: DENEMELER
        public ActionResult Index()
        {
            return View();
        }
        [sessionKontrol]
        public ActionResult Denemeler()
        {
            DENEMELER_CTX dctx = new DENEMELER_CTX();
            var dn = dctx.denemeListesiDondur(yetkiliDondur().ID); 
            return View(dn);
        }
        [sessionKontrol]
        [HttpGet]
        public ActionResult DenemeEkle()
        {
            DENEMELER dn = new DENEMELER();
            return View(dn);
        }

        [HttpPost]
        public ActionResult DenemeEkle(DENEMELER de)
        {
            DENEMELER_CTX dctx = new DENEMELER_CTX();
            de.yet = YetkiliDondur();
            de.YETKILIID = de.yet.ID;

            dctx.denemeEkle(de);
            return RedirectToAction("Denemeler");
        }
        [sessionKontrol]
        [HttpGet]
        public ActionResult DenemeDuzenle(int Id)
        {
            DENEMELER_CTX dctx = new DENEMELER_CTX();
            var deneme = dctx.denemeDondur(Id);
            return View(deneme);
        }

        [HttpPost]
        public ActionResult DenemeDuzenle(DENEMELER dd)
        {
            DENEMELER_CTX dctx = new DENEMELER_CTX();
            dctx.denemeDuzenle(dd);
            return RedirectToAction("Denemeler");
        }
  
        [HttpGet]
        public ActionResult DenemeSil(int id)
        {
            DENEMELER_CTX dctx = new DENEMELER_CTX();
            dctx.denemeSil(id);
            return RedirectToAction("Denemeler");
        }

        public YETKILI YetkiliDondur()
        {
            YETKILI_CTX ytx = new YETKILI_CTX();
            return ytx.yetkiliDondurByMail(HttpContext.User.Identity.Name.ToString());
        }

        public YETKILI yetkiliDondur()
        {
            YETKILI_CTX ytx = new YETKILI_CTX();
            var a = ytx.yetkiliDondurByMail(HttpContext.User.Identity.Name.ToString());
            return a;
        }
    }

}