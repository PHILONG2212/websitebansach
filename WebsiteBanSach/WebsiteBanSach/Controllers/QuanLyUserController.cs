using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data;


namespace WebsiteBanSach.Controllers
{
    public class QuanLyUserController : Controller
    {
        //
        // GET: /QuanLyUser/
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? page)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.KhachHangs.ToList().OrderBy(n => n.MaKH).ToPagedList(pageNumber, pageSize));
        }
        //chỉnh sửa khach hang
        [HttpGet]

        public ActionResult ChinhSua(int MaKH)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            //lấy ra đối tượng theo ma
            KhachHang khachhang = db.KhachHangs.Single(n => n.MaKH == MaKH);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // đưa dữ liệu vào
            //ViewBag.HoTen = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.HoTen), "HoTen", "HoTen", khachhang.HoTen);
            //ViewBag.DiaChi = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.DiaChi), "DiaChi", "DiaChi", khachhang.DiaChi);
           
            return View(khachhang);
        }

        [HttpPost]
       // [ValidateInput(false)]
        public ActionResult ChinhSua(KhachHang khachhang, FormCollection f)
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
                db.Entry(khachhang).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            //ViewBag.HoTen = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.HoTen), "HoTen", "HoTen", khachhang.HoTen);
            //ViewBag.DiaChi = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.DiaChi), "DiaChi", "DiaChi", khachhang.DiaChi);
           
            //ViewBag.MaKH = khachhang.MaKH;

            return RedirectToAction("Index");


        }

        //xóa sản phẩm
        [HttpGet]
        public ActionResult Xoa(int MaKH)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            KhachHang khachhang = db.KhachHangs.Single(n => n.MaKH == MaKH);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(khachhang);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaKH)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            KhachHang khachhang = db.KhachHangs.Single(n => n.MaKH == MaKH);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KhachHangs.Remove(khachhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
     
    }
}
