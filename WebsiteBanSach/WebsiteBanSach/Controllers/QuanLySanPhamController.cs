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
using System.Net;



namespace WebsiteBanSach.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        //
        // GET: /QuanLySanPham/
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
    
        public ActionResult Index(int? page)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.Saches.ToList().OrderBy(n=>n.MaSach).ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            //đưa du lieu vào dropdownlist
           ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
      

        public ActionResult ThemMoi(Sach sach , HttpPostedFileBase fileUpload )
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            
            // đưa dữ liệu vào
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n=>n.TenChuDe), "MaChuDe", "TenChuDe");
            //kiểm tra đường dẫn ảnh bìa 
            if(fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }
     //thêm cơ sơ dữ liêu
            if(ModelState.IsValid)
            {
                //lưu ten file
                var fileName = Path.GetFileName(fileUpload.FileName);
                //lưu đường dẫn file
                var path = Path.Combine(Server.MapPath("~/HinhAnhSP"), fileName);
                //kiểm tra hình ảnh đã tồn tại chưa
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                   
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sach.AnhBia = fileUpload.FileName;

                db.Saches.Add(sach);
                db.SaveChanges();
            }

            return View();
        }




        //chỉnh sửa sản phẩm 
        [HttpGet]
       
        public ActionResult ChinhSua(int MaSach)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            //lấy ra đối tượng theo ma
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // đưa dữ liệu vào
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB",sach.MaNXB);
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe",sach.MaChuDe);
            return View(sach);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(Sach sach,FormCollection f)
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
                db.Entry(sach).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaSach = sach.MaSach;
            
            return RedirectToAction("Index");
        }
        //hiện thị sản phẩm
        public ActionResult HienThi(int MaSach)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            //lấy ra đối tượng theo ma
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

           
            return View(sach);
        }
        //xóa sản phẩm
        [HttpGet]
        public ActionResult Xoa(int? MaSach)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            //Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            //if (sach == null)
            //{
            //    Response.StatusCode = 404;
            //    return null;
            //}
            if(MaSach == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(MaSach);
            if(sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }
        [HttpPost,ActionName("Xoa")]
        [ValidateAntiForgeryToken]
        public ActionResult XacNhanXoa(int MaSach)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Admin", "Admin");
            }
            //Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            //    if(sach==null)
            //    {
            //        Response.StatusCode=404;
            //        return null;
            //    }
            Sach sach = db.Saches.Find(MaSach);
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
     


    }
}
