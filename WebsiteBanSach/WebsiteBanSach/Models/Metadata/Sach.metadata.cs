using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện class metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBanSach.Models
{
    [MetadataTypeAttribute(typeof(SachMetadata))]
    public partial class Sach
    {
        internal sealed class SachMetadata
        {
            [Display (Name="Mã sách")] //dùng đặt tên cột
            public int MaSach { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
            [Display(Name = "Tên sách")] //dùng đặt tên cột
            public string TenSach { get; set; }
            
            [Display(Name = "Giá bán")] //dùng đặt tên cột
            public Nullable<decimal> GiaBan { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
            [Display(Name = "Mô tả")] //dùng đặt tên cột
            public string MoTa { get; set; }
            
         
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}",ApplyFormatInEditMode = true)]//định nghĩa ngày tháng năm
            [Display(Name = "Ngày cập nhật")] //dùng đặt tên cột
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
            [Display(Name = "Ảnh Bìa")] //dùng đặt tên cột

            public string AnhBia { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
            [Display(Name = "Số lượng tồn")] //dùng đặt tên cột
            public Nullable<int> SoLuongTon { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
            [Display(Name = "Mã nhà xuất bản")] //dùng đặt tên cột
            public Nullable<int> MaNXB { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
            [Display(Name = "Chủ đề ")] //dùng đặt tên cột
            public Nullable<int> MaChuDe { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")] // kiểm tra rỗng
            [Display(Name = "Mới")] //dùng đặt tên cột
            public Nullable<int> Moi { get; set; }
        }
    }
}