using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SatGitsin1.Models;
using System.Web.Helpers;
using System.IO;

namespace SatGitsin1.Controllers
{
    public class AdminSatisController : Controller
    {
        satgitsinDB db = new satgitsinDB(); 
        // GET: AdminSatis
        public ActionResult Index()
        {
            var satiss = db.Satis.ToList();
            return View(satiss);
        }

        // GET: AdminSatis/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminSatis/Create
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");
            return View();
        }

        // POST: AdminSatis/Create
        [HttpPost]
        public ActionResult Create(Satis satis, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/SatisFoto/" + newfoto);
                    satis.Foto = "/Uploads/SatisFoto/" + newfoto;
                    
                }
                db.Satis.Add(satis);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
                return View(satis);
            
        }

        // GET: AdminSatis/Edit/5
        public ActionResult Edit(int id)
        {
            var satis = db.Satis.Where(m => m.SatisId == id).SingleOrDefault();
            if (satis == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi", satis.KategoriId);
            return View(satis);
            
        }

        // POST: AdminSatis/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase Foto,Satis satis)
        {
            try
            {
                var duzenle = db.Satis.Where(m => m.SatisId == id).SingleOrDefault();

                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(duzenle.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(duzenle.Foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/SatisFoto/" + newfoto);
                    duzenle.Foto = "/Uploads/SatisFoto/" + newfoto;
                }
                    duzenle.Baslik = satis.Baslik; 
                    duzenle.Ozellik = satis.Ozellik;
                    duzenle.KategoriId = satis.KategoriId;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                
            }
            catch
            {
                ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi", satis.KategoriId);

                return View(satis);
            }
        }

        // GET: AdminSatis/Delete/5
        public ActionResult Delete(int id)
        {
            var satis = db.Satis.Where(m => m.SatisId == id).SingleOrDefault();
            if (satis==null)
            {
                return HttpNotFound();
            }
            return View(satis);
        }

        // POST: AdminSatis/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var satis = db.Satis.Where(m => m.SatisId == id).SingleOrDefault();
                if (satis == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(satis.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(satis.Foto));
                }

                db.Satis.Remove(satis);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
