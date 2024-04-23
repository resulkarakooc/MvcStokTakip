using MvcStokTakip.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcStokTakip.Controllers
{
    public class SatıslarController : Controller //bu satırı açıkla
    {
        // GET: Satıslar
        MvcDbStokEntities db = new MvcDbStokEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(TBLSATISLAR p) { 
        
            db.TBLSATISLAR.Add(p);
            db.SaveChanges();
            return View();
        }

    }
}