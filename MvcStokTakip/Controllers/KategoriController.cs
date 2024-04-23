using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStokTakip.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db= new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLKATEGORILER.ToList(); //bu kod aslında asp.net tarafında bir select çağrısıdır tolist hazır bir fonksiyondur.
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa, 6);

            return View(degerler);
        }

        [HttpPost] //sayfa post edilince
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();

            return Redirect("/Kategori/Index");
        }
        
        [HttpGet] //sayfa gelince
        public ActionResult YeniKategori()
        {
            return View();  
        }

        public ActionResult Sil(int id)
        {
            var bul = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(bul);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View(ktgr);
        }

        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}