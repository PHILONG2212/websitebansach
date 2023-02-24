using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbansach.Model;
using WebsiteBanSach.Models;

namespace webbansach.Controllers
{

    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        #region GioHang
        public List<GioHang> LayGioHang()
        {

            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;

            if (lstGioHang == null)
            {
                // kiểm tra đăng nhập
                
                // nếu giỏ hàng kh tồn tại thì tiến hành khởi tạo lại nó(session giỏ hàng)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //thêm giỏ hàng
        public ActionResult ThemGioHang(int iMaSach, string strURL)
        {
            
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // LẤY  RA TỪ SESSION GIỎ HÀNG
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sách có tồn tại kh
            GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSach);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSach);
                //add sản phẩm mới thêm vào list
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //cập nhật giỏ hàng 
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
           
            // Kiểm tra masp
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            // nếu get sai masp sẽ trả lại trang lỗi 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sp có tồn tại trong session
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            //nếu tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        //xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            // nếu get sai masp sẽ trả lại trang lỗi 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sp có tồn tại trong session
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            //nếu tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == iMaSP);
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");

        }
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        //tính tổng số lượng và tổng tiền
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);

            }
            return iTongSoLuong;
        }
        //tính tổn tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);

            }
            return dTongTien;
        }
        //tao partial giỏ hàng  
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //xây dựng 1 view cho ng dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        #endregion





        #region DatHang
        // xây dựng chức năng đặt hàng
        public ActionResult DatHang()
        {

            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            //kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            //thêm đơn hàng
            DonHang ddh = new DonHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            db.DonHangs.Add(ddh);
            db.SaveChanges();

            //THÊM CHI TIET DON HANG
            foreach (var item in gh)
            {
                ChiTietDonHang ctDH = new ChiTietDonHang();
                ctDH.MaDonHang = ddh.MaDonHang;
                ctDH.MaSach = item.iMaSach;
                ctDH.SoLuong = item.iSoLuong;
                ctDH.DonGia = (decimal)item.dDonGia;
                db.ChiTietDonHangs.Add(ctDH);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        #endregion

    }
}
