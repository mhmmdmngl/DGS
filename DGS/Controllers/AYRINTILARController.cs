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
    public class AYRINTILARController : Controller
    {
        // GET: AYRINTILAR
        public ActionResult Index()
        {
            return View();
        }
        [sessionKontrol]
        public ActionResult Ayrinti()
        {
            AYRINTILAR_CTX actx = new AYRINTILAR_CTX();
            DENEMELER_CTX dnctx = new DENEMELER_CTX();
            MUFREDAT_CTX mfctx = new MUFREDAT_CTX();

            var al = actx.ayrintiListesiDondur();
            foreach (var bahar in al)
            {
                var deneme = dnctx.denemeDondur(bahar.DENEMEID);
                bahar.dn = deneme;
                var konu = mfctx.konuDondur(bahar.KONUID);
                bahar.mf = konu;
            }
            return View(al);
        }
        [sessionKontrol]
        [HttpGet]
        public ActionResult AyrintiEkle(int denemeID)
        {
            AYRINTILAR_CTX actx = new AYRINTILAR_CTX();
            DENEMELER_CTX dctx = new DENEMELER_CTX();
            MUFREDAT_CTX mfctx = new MUFREDAT_CTX();
            SORULAR_CTX sctx = new SORULAR_CTX();
            var deneme = dctx.denemeDondur(denemeID);

            ViewBag.ayrintideneme = new SelectList(denemeHazırla(), "ID", "DENEMEADI");
            ViewBag.mufredat = new SelectList(mufredatHazırla(), "ID", "KONUADI");
            var muf = mfctx.mufredatDondur();
            foreach(var bahar in muf)
            {
                bahar.soru = new SORULAR();
            }
            return View(muf);
        }

        [HttpPost]
        public ActionResult AyrintiEkle(AYRINTILAR ae)
        {
            AYRINTILAR_CTX actx = new AYRINTILAR_CTX();
            var gelen = Request.Form["ayrintideneme"];
            var gelen1 = Request.Form["mufredat"];

            ae.DENEMEID = Convert.ToInt32(gelen);
            ae.KONUID = Convert.ToInt32(gelen);

            ae.yet = yetkiliDondur();
            ae.yetkiliID = yetkiliDondur().ID;
            var deneme = actx.ayrintiEkle(ae);

            return RedirectToAction("Ayrinti");
        }
        [sessionKontrol]
        [HttpGet]
        public ActionResult AyrintiDuzenle(int denemeID)
        {
            AYRINTILAR_CTX actx = new AYRINTILAR_CTX();
            DENEMELER_CTX dctx = new DENEMELER_CTX();
            MUFREDAT_CTX mfct = new MUFREDAT_CTX();
            var ayrintiList = actx.ayrintiDondurbyDenemeID(denemeID);
            foreach(var bahar in ayrintiList)
            {
                var den = dctx.denemeDondur(denemeID);
                if(den != null)
                    bahar.dn = den;
                var muf = mfct.konuDondur(bahar.KONUID);
                if (muf != null)
                    bahar.mf = muf;
            }
  

            ViewBag.ayrintideneme = new SelectList(denemeHazırla(), "ID", "DENEMEADI");
            ViewBag.mufredat = new SelectList(mufredatHazırla(), "ID", "KONUADI");

            return View(ayrintiList);
        }

        [HttpPost]
        public ActionResult AyrintiDuzenle(List<AYRINTILAR> ad)
        {
            AYRINTILAR_CTX actx = new AYRINTILAR_CTX();
            //var gelen = Request.Form["ayrintideneme"];
            //var gelen1 = Request.Form["mufredat"];

            //ad.DENEMEID = Convert.ToInt32(gelen);
            //ad.KONUID = Convert.ToInt32(gelen1);
            foreach(var a in ad)
            {
                actx.ayrintiDuzenle(a);
            }
            return RedirectToAction("Deneme_Sonuc", "DENEME_SONUC", new { area = "" });
        }

        [HttpGet]
        public ActionResult AyrintiSil(int id)
        {
            AYRINTILAR_CTX actx = new AYRINTILAR_CTX();
            actx.ayrintiSil(id);
            return RedirectToAction("Ayrinti");
        }

        public class deneme
        {
            public int ID { set; get; }
            public string DENEMEADI { set; get; }
        }

        //dropdown list'de görüntülenecek konuların listesi burada hazırlanıyor.
        public List<deneme> denemeHazırla()
        {
            DENEMELER_CTX dnctx = new DENEMELER_CTX();
            List<deneme> dnListe = new List<deneme>();
            foreach (var x in dnctx.denemeListesiDondur(yetkiliDondur().ID))
            {
                deneme bl = new deneme();
                bl.ID = x.ID;
                bl.DENEMEADI = x.DENEMEADI;
                dnListe.Add(bl);
            }
            return dnListe;
        }

        public class konu
        {
            public int ID { set; get; }
            public string KONUADI { set; get; }
        }

        //dropdown list'de görüntülenecek konuların listesi burada hazırlanıyor.
        public List<konu> mufredatHazırla()
        {
            MUFREDAT_CTX mfctx = new MUFREDAT_CTX();
            List<konu> kListe = new List<konu>();
            foreach (var x in mfctx.mufredatDondur())
            {
                konu bl = new konu();
                bl.ID = x.ID;
                bl.KONUADI = x.KONUADI;
                kListe.Add(bl);
            }
            return kListe;
        }

        public YETKILI yetkiliDondur()
        {
            YETKILI_CTX ytx = new YETKILI_CTX();
            var a = ytx.yetkiliDondurByMail(HttpContext.User.Identity.Name.ToString());
            return a;
        }
    }
}