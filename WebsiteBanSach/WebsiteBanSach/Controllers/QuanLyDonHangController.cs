using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbansach.Model;
using WebsiteBanSach.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data;

namespace WebsiteBanSach.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        //
        // GET: /QuanLyDonHang/

        QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        public ActionResult Index(int? page)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.DonHangs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(pageNumber, pageSize));
        }
        //chỉnh sửa khach hang
        [HttpGet]

        public ActionResult ChinhSua(int MaDonHang)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }

            //lấy ra đối tượng theo ma
            DonHang donhang = db.DonHangs.Single(n => n.MaDonHang == MaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // đưa dữ liệu vào
            //ViewBag.HoTen = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.HoTen), "HoTen", "HoTen", khachhang.HoTen);
            //ViewBag.DiaChi = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.DiaChi), "DiaChi", "DiaChi", khachhang.DiaChi);

            return View(donhang);
        }

        [HttpPost]
        // [ValidateInput(false)]
        public ActionResult ChinhSua(DonHang donhang, FormCollection f)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            // Sach sach1 = db.Saches.SingleOrDefault(n => n.MaSach == sach.MaSach);
            // sach1.MoTa = sach.MoTa;
            // sach1.MoTa = f.Get("abc").ToString();
            // sach.MoTa = f["abc"].ToString();
            if (ModelState.IsValid)
            {

                //thực hiện cập nhật trong model
                db.Entry(donhang).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            //ViewBag.HoTen = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.HoTen), "HoTen", "HoTen", khachhang.HoTen);
            //ViewBag.DiaChi = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.DiaChi), "DiaChi", "DiaChi", khachhang.DiaChi);

            //ViewBag.MaKH = khachhang.MaKH;

            return RedirectToAction("Index");


        }

        //xóa sản phẩm
        [HttpGet]
        public ActionResult Xoa(int MaDonHang)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            DonHang donhang = db.DonHangs.Single(n => n.MaDonHang == MaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(donhang);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaDonHang)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            DonHang donhang  = db.DonHangs.Single(n => n.MaDonHang == MaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DonHangs.Remove(donhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
