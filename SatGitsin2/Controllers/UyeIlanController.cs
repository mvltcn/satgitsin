using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SatGitsin2.Models;
using System.IO;
using System.Web.Helpers;

namespace SatGitsin2.Controllers
{
    public class UyeIlanController : Controller
    {
        private satgitsinDB db = new satgitsinDB();

        // GET: UyeIlan
        public ActionResult Index(int id)
        {         
            
            var ilan = db.Ilan.Where(i => i.UyeId == id);
            

            
            return View(ilan.ToList());
        }

       

        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAdi");

           
            return View();
        }

        // POST: AdminIlan/Create
        [HttpPost]
        public ActionResult Create(Ilan ilan, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 300);
                    img.Save("~/Uploads/İlanFoto/" + newfoto);
                    ilan.Foto = "/Uploads/İlanFoto/" + newfoto;
                }
                ilan.UyeId = Convert.ToInt32(Session["uyeid"]);
                ilan.Tarih = DateTime.Now;
                ilan.Gorulme = 0;
                db.Ilan.Add(ilan);
                db.SaveChanges();

                return RedirectToAction("Index",new { id = Session["uyeid"] });
            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAdi");
            return View(ilan);
        }

        // GET: AdminIlan/Edit/5
        public ActionResult Edit(int id)
        {
            var ilan = db.Ilan.Where(m => m.IlanId == id).SingleOrDefault();
            if (ilan == null)
            {
                return HttpNotFound();

            }

            if (Convert.ToInt32(Session["uyeid"]) != ilan.UyeId)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategori, "KategoriId", "KategoriAdi", ilan.IlanId);
            return View(ilan);
        }

        // POST: AdminIlan/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Ilan ilan, HttpPostedFileBase Foto)
        {

            try
            {
                var ilanduzenle = db.Ilan.Where(m => m.IlanId == id).SingleOrDefault();
                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(ilanduzenle.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(ilanduzenle.Foto));
                    }

                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 300);
                    img.Save("~/Uploads/İlanFoto/" + newfoto);
                    ilanduzenle.Foto = "/Uploads/İlanFoto/" + newfoto;


                }
                ilanduzenle.Baslik = ilan.Baslik;
                ilanduzenle.Ozellik = ilan.Ozellik;
                ilanduzenle.Fiyat = ilan.Fiyat;
                ilanduzenle.KategoriId = ilan.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Index",new { id = ilanduzenle.UyeId });


            }
            catch
            {
                return View(ilan);
            }
        }

        // GET: AdminIlan/Delete/5
        public ActionResult Delete(int id)
        {
            var ilan = db.Ilan.Where(m => m.IlanId == id).SingleOrDefault();
            if (ilan == null)
            {
                return HttpNotFound();
            }

            if (Convert.ToInt32(Session["uyeid"]) != ilan.UyeId)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        // POST: AdminIlan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var ilanSil = db.Ilan.Where(m => m.IlanId == id).SingleOrDefault();
                if (ilanSil == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(ilanSil.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(ilanSil.Foto));
                }
                db.Ilan.Remove(ilanSil);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Convert.ToInt32(Session["uyeid"]) });
            }
            catch
            {
                return View();
            }
        }
    }
}
