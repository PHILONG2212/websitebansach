using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebsiteBanSach.Models
{
        [MetadataTypeAttribute(typeof(KhachHangMetadata))]
    public partial class KhachHang
    {
        internal sealed class KhachHangMetadata
    {
            [Display(Name = "Mã khách hàng")] //dùng đặt tên cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
        public int MaKH { get; set; }
            [Display(Name = "Họ tên")] //dùng đặt tên cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
        public string HoTen { get; set; }
            [Display(Name = "Tài khoản")] //dùng đặt tên cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
        public string TaiKhoan { get; set; }
            [Display(Name = "Mật khẩu")] //dùng đặt tên cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
        public string MatKhau { get; set; }
            [Display(Name = "Email")] //dùng đặt tên cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
        public string Email { get; set; }

            [Display(Name = "Địa chỉ")] //dùng đặt tên cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
        public string DiaChi { get; set; }
            [Display(Name = "Điện thoại")] //dùng đặt tên cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
        public string DienThoai { get; set; }
            [Display(Name = "Giới tính")] //dùng đặt tên cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
        public string GioiTinh { get; set; }
            [Display(Name = "Ngày sinh")] //dùng đặt tên cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]//định nghĩa ngày tháng năm
        public Nullable<System.DateTime> NgaySinh { get; set; }
    
    }
    }
}