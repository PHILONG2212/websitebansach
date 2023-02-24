using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using webbansach.Model;
using WebsiteBanSach.Models;

namespace webbansach.Controllers
{
    public class NguoiDungController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        //
        // GET: /NguoiDung/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KhachHang kh)
        {
            //if(ModelState.IsValid)
            //{

            //}
            //chèn dữ liệu vào bảng khách hàng
            db.KhachHangs.Add(kh);
            //lưu dũ liệu csdl
            db.SaveChanges();
          //  return View(kh);
            ViewBag.ThongBao = "Chúc mừng bạn đăng ký  thành công! ";
            Thread.Sleep(2000);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string  sTaiKhoan = f["txtTaiKhoan"].ToString();
            string  sMatKhau = f.Get("txtMatKhau").ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (kh !=null)
            {
                ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công! ";
                Session["TaiKhoan"] = kh;
                return RedirectToAction("Index","Home");

            }
            ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không đúng! ";
                return View();
        }

        //dang xuat admin
        public ActionResult SignOut()
        {


            Session.Clear();
            return RedirectToAction("DangNhap");
        }
    }
}
