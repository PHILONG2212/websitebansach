using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebsiteBanSach.Models;


namespace webbansach.Model
{
    public class GioHang
    {
       // private int iMaSP;

        //public int IMaSP
      //  {
        //    get { return iMaSP; }
          //  set { iMaSP = value; }
        //}
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
   
        public int iMaSach { get; set; }
        public string sTenSanh { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }

        public string fileUpload { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
           


        }
        //ham tạo ra giỏ sách
        public GioHang(int MaSach)
        {
            iMaSach = MaSach;
            Sach sach = db.Saches.Single(n => n.MaSach == iMaSach);
            sTenSanh = sach.TenSach;
            sAnhBia = sach.AnhBia;
            dDonGia = double.Parse(sach.GiaBan.ToString());
            iSoLuong = 1;
        }

        

       
    }
}