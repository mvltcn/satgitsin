using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SatGitsin2.Models;
using PagedList;
using PagedList.Mvc;

namespace SatGitsin2.Controllers
{
    public class HomeController : Controller
    {
        satgitsinDB db = new satgitsinDB();
        public ActionResult Index(int Page=1)
        {
            var ilan = db.Ilan.OrderByDescending(m => m.IlanId).ToPagedList(Page,12);
            return View(ilan);
        }
        public ActionResult IlanDetay(int id)
        {
            var ilan = db.Ilan.Where(m => m.IlanId == id).SingleOrDefault();

            if (ilan==null)
            {
                return HttpNotFound();
            }
            ilan.Gorulme = ilan.Gorulme + 1;
            db.SaveChanges();
            return View(ilan);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Arama (string Ara= null,int Page= 1)
        {
            var aranan = db.Ilan.OrderByDescending(m => m.IlanId).Where(m => m.Baslik.Contains(Ara)).ToPagedList(Page, 12);
            return View(aranan);
        }

        public ActionResult KategoriPartial()
        {
            ViewBag.Message = "Your contact page.";

            return View(db.Kategori.ToList());
        }
        public ActionResult IlanKategori(int id,int Page=1)
        {
            var ilanKategori = db.Ilan.OrderByDescending(m => m.IlanId).Where(k => k.KategoriId == id).ToPagedList(Page, 12);



            return View(ilanKategori);
        }
    }
}