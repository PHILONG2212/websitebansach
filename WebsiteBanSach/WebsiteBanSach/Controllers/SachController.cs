﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbansach.Model;
using WebsiteBanSach.Models;

namespace webbansach.Controllers
{
    public class SachController : Controller
    {
        //
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: /Sach/
        public PartialViewResult SachMoiPartial()
        {
            var lstSachMoi = db.Saches.Take(3).ToList();
            return PartialView(lstSachMoi);
        }
        public ViewResult XemChiTiet(int MaSach = 0)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.TenChuDe = db.ChuDes.Single(n => n.MaChuDe==sach.MaChuDe).TenChuDe;
            ViewBag.NhaXuatBan = db.NhaXuatBans.Single(n => n.MaNXB == sach.MaNXB).TenNXB;

            return View(sach);
        }
    }
}
