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
    public class SORULARController : Controller
    {
        // GET: SORULAR
        public ActionResult Index()
        {
            return View();
        }
        [sessionKontrol]
        public ActionResult Sorular()
        {
            SORULAR_CTX sctx = new SORULAR_CTX();
            MUFREDAT_CTX mctx = new MUFREDAT_CTX();
            // Burası sonradan eklendi... Amacı Sorular modelinde yer alan KONUID ile ilişkili Mufredat modelini bularak mf nesnesine atamak. Böylece Sorular listesi oluşturulduğunda Konu Adı bilgisine rahatlıkla ulaşabileceğiz.
            var sr = sctx.soruBankasiDondur(yetkiliDondur().ID); // Burada esas sorular listesini oluşturduk.
            foreach (var bahar in sr) //Sorular listesindeki herbir soru için döngüye giriyoruz.
            {
                var konu = mctx.konuDondur(bahar.KONUID); //KonuID bilgisinden Müfredat modelini döndürüyoruz.
                if (konu != null)//Müfredat modeli her ihtimale karşı boş olabilir diye kontrol ediyoruz.
                {
                    bahar.mf = konu; //burada esas atama işlemini yapıyoruz. Böylece herbir KonuID ile ilişkili Müfredat nesnesini atamış oluyoruz. Kolayca erişim sağlanmış oluyor.
                }
            }
            // Sonradan eklenen bölümün sonu *************************************************************************************
            return View(sr);
        }
        [sessionKontrol]
        [HttpGet]
        public ActionResult TestEkle()
        {
            SORULAR sr = new SORULAR();
            // Sonradan eklenen bölüm. Konu listesini dropdown menüye aktarmak 
            ViewBag.mufredat = new SelectList(listeHazırla(), "ID", "KONUADI");
            // **************** SONRADAN EKLENEN BÖLÜM SONU ****************
            return View(sr);
        }

        [HttpPost]
        public ActionResult TestEkle(SORULAR te)
        {
            SORULAR_CTX sctx = new SORULAR_CTX();
            te.yet = yetkiliDondur();
            te.YETKILIID = yetkiliDondur().ID;

            var gelen = Request.Form["mufredat"];
            te.KONUID = Convert.ToInt32(gelen);

            sctx.testEkle(te);
            return RedirectToAction("Sorular");
        }

        [HttpGet]
        public ActionResult TestDuzenle(int id)
        {
            SORULAR_CTX sctx = new SORULAR_CTX();
            var bahar = sctx.testDondur(id);
            // Sonradan eklenen bölüm. Konu listesini dropdown menüye aktarmak 
            ViewBag.mufredat = new SelectList(listeHazırla(), "ID", "KONUADI");
            // **************** SONRADAN EKLENEN BÖLÜM SONU ****************
            if (bahar != null)
                return View(bahar);
            return RedirectToAction("Sorular");
        }

        [HttpPost]
        public ActionResult TestDuzenle(SORULAR td)
        {
            SORULAR_CTX sctx = new SORULAR_CTX();
            var gelen = Request.Form["mufredat"];
            td.KONUID = Convert.ToInt32(gelen);
            sctx.testDuzenle(td);
            return RedirectToAction("Sorular");
        }

        [HttpGet]
        public ActionResult TestSil(int id)
        {
            SORULAR_CTX sctx = new SORULAR_CTX();
            sctx.testSil(id);
            return RedirectToAction("Sorular");
        }

        //Müfredat modelinde yer alan bilgilerden yalnızca iki tanesi lazım olduğundan yeni bir class oluşturuyoruz.
        public class mufredat
        {
            public int ID { set; get; }
            public string KONUADI { set; get; }
        }

        //dropdown list'de görüntülenecek konuların listesi burada hazırlanıyor.
        public List<mufredat> listeHazırla()
        {
            MUFREDAT_CTX mfctx = new MUFREDAT_CTX();
            List<mufredat> mfListe = new List<mufredat>();
            foreach (var x in mfctx.mufredatDondur())
            {
                mufredat bl = new mufredat();
                bl.ID = x.ID;
                bl.KONUADI = x.KONUADI;
                mfListe.Add(bl);
            }
            return mfListe;
        }

        public YETKILI yetkiliDondur()
        {
            YETKILI_CTX ytx = new YETKILI_CTX();
            var a = ytx.yetkiliDondurByMail(HttpContext.User.Identity.Name.ToString());
            return a;
        }

    }
}