using System.ComponentModel.DataAnnotations;

namespace EcomQLDM.ViewModels
{
    public class PhanQuyen
    {
        [Display(Name = "MaQH")]
        public string MaQH { get; set; }

        [Display(Name = "Chức Vụ")]
        public string TenQH { get; set; }

        [Display(Name = "Họ Tên")]
        public string HoTen { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Phone number not valid")]
        public string DienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Email not valid")]
        public string Email { get; set; }

        public string? Hinh { get; set; }
    }
    public class PhanQuyenVM
    {
        public List<NhanVienVM>? phanQuyens { get; set; }
        public string QlHangHoa { get; set; } = "disabled";
        public string QlHoaDon { get; set; } = "disabled";
        public string QlKhachHang { get; set; } = "disabled";
        public string QlNhanVien { get; set; } = "disabled";
        public string QlShipper { get; set; } = "disabled";
        public string QlKho { get; set; } = "disabled";
        public string QlDoanhThu { get; set; } = "disabled";
        public string? Hinh { get; set; }
        public List<HangHoaVM>? hangHoas { get; set; }
        public List<XepHangKHVM>? xepHangKHs { get; set; }
    }
}
