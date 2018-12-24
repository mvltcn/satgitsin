﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SatGitsin2.Models;

namespace SatGitsin2.Controllers
{
    public class UyeController : Controller
    {
        satgitsinDB db = new satgitsinDB();
        // GET: Uye
        public ActionResult Index(int id)
        {
            var uye = db.Uye.Where(u => u.UyeId == id).SingleOrDefault();
            if (Convert.ToInt32(Session["uyeid"])!=uye.UyeId)
            {
                return HttpNotFound();
            }
            return View(uye);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Uye uye)
        {

            var login = db.Uye.Where(m => m.KullaniciAdi == uye.KullaniciAdi).SingleOrDefault();
           
             if (login.KullaniciAdi==uye.KullaniciAdi  && login.Sifre==uye.Sifre)
            {
                Session["uyeid"] = login.UyeId;
                Session["kullaniciadi"] = login.KullaniciAdi;
                Session["yetkiid"] = login.YetkiId;
                return RedirectToAction("Index", "Home");
            }
            else
            {
            ViewBag.Uyari = "Kullanıcı Adı ve Şifrenizi Kontrol edin";
            return View();

            }
        }
        public ActionResult Logout()
        {
            Session["uyeid"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Uye uye, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto!=null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uye.Foto = "/Uploads/UyeFoto/" + newfoto;
                    uye.YetkiId = 2;
                    db.Uye.Add(uye);
                    db.SaveChanges();
                    Session["uyeid"] = uye.UyeId;
                    Session["kullaniciadi"] = uye.KullaniciAdi;
                    return RedirectToAction("Index","Home"); 
                }
                else
                {
                    ModelState.AddModelError("Fotoğraf", "Fotoğraf Ekleyiniz");
                }
            }
            return View(uye);
        }
        public ActionResult Edit(int id)
        {
            var uye = db.Uye.Where(u => u.UyeId == id).SingleOrDefault();
            if (Convert.ToInt32(Session["uyeid"])!=uye.UyeId)
            {
                return HttpNotFound();
            }
            return View(uye);

        }
        [HttpPost]
        public ActionResult Edit(Uye uye, int id,HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                var uyeguncelle = db.Uye.Where(u => u.UyeId == id).SingleOrDefault();
                if (Foto!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(uye.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(uyeguncelle.Foto));
                    }

                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uyeguncelle.Foto = "/Uploads/UyeFoto/" + newfoto;


                }
                uyeguncelle.AdSoyad = uye.AdSoyad;
                uyeguncelle.KullaniciAdi = uye.KullaniciAdi;
                uyeguncelle.Email = uye.Email;
                uyeguncelle.Sifre = uye.Sifre;
                db.SaveChanges();
                Session["kullaniciadi"] = uye.KullaniciAdi;
                return RedirectToAction("Index","Home",new { id = uyeguncelle.UyeId });
            }

            return View(uye);

        }
        public ActionResult UyeProfil (int id)
        {
            var uye = db.Uye.Where(m => m.UyeId == id).SingleOrDefault();
            return View(uye);
        }
    }
}