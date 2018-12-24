using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SatGitsin2.Models;

namespace SatGitsin2.Controllers
{
    public class AdminController : Controller
    {
        satgitsinDB db = new satgitsinDB();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.IlanSayisi = db.Ilan.Count();
            ViewBag.UyeSayisi = db.Uye.Count();
            ViewBag.KategoriSayisi = db.Kategori.Count();
            return View();
        }
    }
}