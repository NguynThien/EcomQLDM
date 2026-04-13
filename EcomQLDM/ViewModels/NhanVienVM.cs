using System.ComponentModel.DataAnnotations;

namespace EcomQLDM.ViewModels
{
    public class NhanVienVM
    {
        [Display(Name = "Username")]
        public string MaNV { get; set; }

        [Display(Name = "Chức Vụ")]
        public string TenQH { get; set; }

        [Display(Name = "Họ Tên")]
        public string HoTen { get; set; }

        [Display(Name = "Giới Tính")]
        public string GioiTinh { get; set; }


        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Phone number not valid")]
        public string DienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Email not valid")]
        public string Email { get; set; }

        public string? Hinh { get; set; }
    }

    public class AddNhanVienVM
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "20 letters max")]
        public string MaNV { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "50 letters max")]
        public string HoTen { get; set; }

        [EmailAddress(ErrorMessage = "Email not valid")]
        public string Email { get; set; }

        public int MaQH { get; set; }
        public string TenQH { get; set; }
        public int HieuLuc { get; set; }

        
        [Display(Name = "Số Điện Thoại")]
        [MaxLength(24, ErrorMessage = "24 numbers max")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Phone number not valid")]
        public string DienThoai { get; set; }
        public string Hinh { get; set; }
        public string DiaChi { get; set; }
        public bool GioiTinh { get; set; } = true;
    }
}
