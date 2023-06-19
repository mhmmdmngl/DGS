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
    public class DENEME_SONUCController : Controller
    {
        // GET: DENEME_SONUC
        public ActionResult Index()
        {
            return View();
        }
        [sessionKontrol]
        public ActionResult Deneme_Sonuc()
        {
            DENEME_SONUC_CTX dsctx = new DENEME_SONUC_CTX();
            DENEMELER_CTX dctx = new DENEMELER_CTX();
            var ds = dsctx.tumsonucDondur(yetkiliDondur().ID);
            foreach(var bahar in ds)
            {
                var deneme = dctx.denemeDondur(bahar.DENEMEID);
                bahar.dn = deneme;
            }
            return View(ds);
        }
        [sessionKontrol]
        [HttpGet]
        public ActionResult SonucEkle()
        {
            DENEME_SONUC se = new DENEME_SONUC();
            ViewBag.deneme = new SelectList(denemeHazırla(), "ID", "DENEMEADI");
            return View(se);
        }

        [HttpPost]
        public ActionResult SonucEkle(DENEME_SONUC se)
        {
            DENEME_SONUC_CTX dsctx = new DENEME_SONUC_CTX();
            var gelen = Request.Form["deneme"];
            se.DENEMEID = Convert.ToInt32(gelen);
            se.yetki = yetkiliDondur();
            se.YETKILIID = yetkiliDondur().ID;
            var deneme = dsctx.sonucEkle(se);

            MUFREDAT_CTX mctx = new MUFREDAT_CTX();
            AYRINTILAR_CTX actx = new AYRINTILAR_CTX();
            var muf = mctx.mufredatDondur();
            foreach(var bah in muf)
            {
                AYRINTILAR ay = new AYRINTILAR();
                ay.DENEMEID = se.DENEMEID;
                var muf1 = mctx.konuDondur(bah.ID);

                ay.KONUID = muf1.ID;
                ay.yet = yetkiliDondur();
                ay.yetkiliID = yetkiliDondur().ID;
                actx.ayrintiEkle(ay);
            }
            
            return RedirectToAction("Deneme_Sonuc");
        }
        [sessionKontrol]
        [HttpGet]
        public ActionResult SonucDuzenle()
        {
            DENEME_SONUC ds = new DENEME_SONUC();
            ViewBag.deneme = new SelectList(denemeHazırla(), "ID", "DENEMEADI");
            return View(ds);
        }

        [HttpPost]
        public ActionResult SonucDuzenle(DENEME_SONUC ds)
        {
            DENEME_SONUC_CTX dsctx = new DENEME_SONUC_CTX();
            var gelen = Request.Form["deneme"];
            ds.DENEMEID = Convert.ToInt32(gelen);
            dsctx.sonucDuzenle(ds);
            return RedirectToAction("Deneme_Sonuc");
        }

        [HttpGet]
        public ActionResult SonucSil(int id)
        {
            DENEME_SONUC_CTX dsctx = new DENEME_SONUC_CTX();
            dsctx.sonucSil(id);
            return RedirectToAction("Deneme_Sonuc");
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

        public YETKILI yetkiliDondur()
        {
            YETKILI_CTX ytx = new YETKILI_CTX();
            var a = ytx.yetkiliDondurByMail(HttpContext.User.Identity.Name.ToString());
            return a;
        }
    }
}