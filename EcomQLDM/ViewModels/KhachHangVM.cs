using System.ComponentModel.DataAnnotations;

namespace EcomQLDM.ViewModels
{
    public class KhachHangVM
    {
        [Display(Name = "Username")]
        public string MaKh { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Họ Tên")]
        public string HoTen { get; set; }

        [Display(Name = "Giới Tính")]
        public string GioiTinh { get; set; }

        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        public DateOnly NgaySinh { get; set; }

        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Phone number not valid")]
        public string DienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Email not valid")]
        public string Email { get; set; }

        public string? Hinh { get; set; }

    }
    public class ResetPasswordVM
    {
        [Display(Name = "Username")]
        public string MaKh { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhập Lại Password")]
        [Required(ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }

        public string hidden { get; set; } = "hidden";
    }

    public class XepHangKHVM
    {
        [Display(Name = "Username")]
        public string MaKh { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Họ Tên")]
        public string HoTen { get; set; }

        [Display(Name = "Giới Tính")]
        public string GioiTinh { get; set; }

        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        public DateOnly NgaySinh { get; set; }

        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Phone number not valid")]
        public string DienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Email not valid")]
        public string Email { get; set; }

        public string? Hinh { get; set; }
        public int SoLuongMuaHang { get; set; }

    }
}
