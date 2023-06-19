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
    public class MUFREDATController : Controller
    {
        // GET: MUFREDAT
        public ActionResult Index()
        {
            return View();
        }
        [sessionKontrol]
        public ActionResult Mufredat()
        {
            MUFREDAT_CTX mctx = new MUFREDAT_CTX();
            var mf = mctx.mufredatDondur();
            return View(mf);
        }
        [sessionKontrol]
        [HttpGet]
        public ActionResult KonuEkle()
        {
            MUFREDAT mf = new MUFREDAT();
            return View(mf);
        }

        [HttpPost]
        public ActionResult KonuEkle(MUFREDAT ke)
        {
            MUFREDAT_CTX mctx = new MUFREDAT_CTX();
            mctx.konuEkle(ke);
            return RedirectToAction("Mufredat");
        }

        [HttpGet]
        public ActionResult KonuDuzenle(int id)
        {
            MUFREDAT_CTX mctx = new MUFREDAT_CTX();
            var bahar = mctx.konuDondur(id);
            if(bahar != null)
                return View(bahar);
            return RedirectToAction("Mufredat");
        }

        [HttpPost]
        public ActionResult KonuDuzenle(MUFREDAT kd)
        {
            MUFREDAT_CTX mctx = new MUFREDAT_CTX();
            mctx.konuduzenle(kd);
            return RedirectToAction("Mufredat");
        }

        [HttpGet]
        public ActionResult KonuSil(int id)
        {
            MUFREDAT_CTX mctx = new MUFREDAT_CTX();
            mctx.konusil(id);
            return RedirectToAction("Mufredat");
        }
    }
}