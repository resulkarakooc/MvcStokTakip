using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;
namespace MvcStokTakip.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
           
            var degerler = db.TBLURUNLER.ToList(); // hazır method olan tolist ile select işlemi 
            return View(degerler);
        }
        [HttpGet]
        public ActionResult yeniurun()
        {
            // Veritabanından tüm kategorileri getir ve SelectListItem tipinde listeye dönüştür
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()  // Linq sorgusu kullanıldı 
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,  // SelectListItem'ın metin kısmı
                                                 Value = i.KATEGORIID.ToString() // SelectListItem'ın değer kısmı
                                             }).ToList();
            ViewBag.dgr = degerler; // View tarafında bu değerlere erişmek için ViewBag kullanılır
            return View(); // yeniurun.cshtml görünümünü döndürür
        }

        [HttpPost]
        public ActionResult yeniurun(TBLURUNLER p1)
        {
            // HTTP POST isteği ile gelen ürün bilgisini alır

            // İlgili kategoriyi veritabanından bul
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            // Ürünün kategorisini bulunan kategori ile değiştir
            p1.TBLKATEGORILER = ktg;
            // Ürünü veritabanına ekle
            db.TBLURUNLER.Add(p1);
            // Değişiklikleri kaydet
            db.SaveChanges();
            // Ürün listesi sayfasına yönlendir
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var bul = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(bul);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var deger = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()  // Linq sorgusu kullanıldı 
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View(deger);
        }

        [HttpPost]
        public ActionResult Guncelle(TBLURUNLER p2)
        {
            var urun = db.TBLURUNLER.Find(p2.URUNID);
            urun.URUNAD = p2.URUNAD;
            urun.MARKA = p2.MARKA;
            urun.STOK = p2.STOK;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p2.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;

            db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}