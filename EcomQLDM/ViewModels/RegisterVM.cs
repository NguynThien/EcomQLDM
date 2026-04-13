using System.ComponentModel.DataAnnotations;

namespace EcomQLDM.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "20 letters max")]
        public string MaKh { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "50 letters max")]
        public string HoTen { get; set; }

        public bool GioiTinh { get; set; } = true;

        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [Display(Name = "Địa Chỉ")]
        [MaxLength(60, ErrorMessage = "60 letters max")]
        public string DiaChi { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [MaxLength(24, ErrorMessage = "24 numbers max")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Phone number not valid")]
        public string DienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Email not valid")]
        public string Email { get; set; }

        public string? Hinh { get; set; }
    }
}
