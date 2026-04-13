using EcomQLDM.Data;
using System.ComponentModel.DataAnnotations;

namespace EcomQLDM.ViewModels
{
    public class KhoVM
    {
        [Display(Name = "Mã Kho")]
        public string MaKho { get; set; }

        [Display(Name = "Tên Kho")]
        public string TenKho { get; set; }

        [Display(Name = "Dịa Chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Họ Tên Quản Đốc")]
        public string HoTenQuanDoc { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Phone number not valid")]
        public string DienThoai { get; set; }

        public string Email { get; set; }
    }

    public class PhieuNhapVM
    {
        [Display(Name = "Mã Phiếu Nhập")]
        public int MaPN { get; set; }

        [Display(Name = "Mã Kho")]
        public string MaKho { get; set; }

        [Display(Name = "Ngày Lập Phiếu")]
        [DataType(DataType.Date)]
        public DateOnly NgayLapPhieu { get; set; }

        [Display(Name = "Ngày Nhập Hàng")]
        [DataType(DataType.Date)]
        public DateOnly NgayNhapHang { get; set; }
    }

    public class ChiTietPNVM
    {
        [Display(Name = "Mã Chi Tiết Phiếu Nhập")]
        public int MaCTPN { get; set; }

        [Display(Name = "Mã Phiếu Nhập")]
        public int MaPN { get; set; }

        [Display(Name = "Mã Hàng")]
        public int MaHH { get; set; }

        [Display(Name = "Tên Hàng")]
        public string TenHH { get; set; }

        [Display(Name = "Số Lượng")]
        public int SoLuong { get; set; }

        [Display(Name = "Mã Kho")]
        public string MaKho { get; set; }

        [Display(Name = "Tên Kho")]
        public string TenKho { get; set; }

        [Display(Name = "Người lập Phiếu")]
        public string HoTenQuanDoc { get; set; }

        public List<ChiTietPn>? DSctpn { get; set; }
    }

    public class PhieuXuatVM
    {
        [Display(Name = "Mã Phiếu Xuất")]
        public int MaPX { get; set; }

        [Display(Name = "Mã Kho")]
        public string MaKho { get; set; }

        [Display(Name = "Ngày Lập Phiếu")]
        [DataType(DataType.Date)]
        public DateOnly NgayLapPhieu { get; set; }

        [Display(Name = "Ngày Nhập Hàng")]
        [DataType(DataType.Date)]
        public DateOnly NgayNhapHang { get; set; }
    }

    public class ChiTietPXVM
    {
        [Display(Name = "Mã Chi Tiết Phiếu Xuất")]
        public int MaCTPX { get; set; }

        [Display(Name = "Mã Phiếu Xuất")]
        public int MaPX { get; set; }

        [Display(Name = "Mã Hàng")]
        public int MaHH { get; set; }

        [Display(Name = "Tên Hàng")]
        public string TenHH { get; set; }

        [Display(Name = "Số Lượng")]
        public int SoLuong { get; set; }

        [Display(Name = "Mã Kho")]
        public string MaKho { get; set; }

        [Display(Name = "Tên Kho")]
        public string TenKho { get; set; }

        [Display(Name = "Người lập Phiếu")]
        public string HoTenQuanDoc { get; set; }

        public List<ChiTietPx>? DSctpx { get; set; }
    }
    public partial class ChiTietPxVM
    {
        public int MaCtpx { get; set; }

        public int MaPx { get; set; }

        public int MaHh { get; set; }

        public int SoLuong { get; set; }

        public DateOnly NgayLapPhieu { get; set; }

    }

    public class QLKhoVM
    {
        public List<KhoVM>? ThongTinKho { get; set; }
        public List<PhieuNhapVM>? DanhSachPN { get; set; }
        public List<PhieuXuatVM>? DanhSachPX { get; set; }
        public List<HangHoaVM>? DanhSachHH { get; set; }

    }
    public class SoluongHH
    {
        public int SoLuongNhap { get; set; }
        public int SoLuongXuat { get; set; }
    }
    public class TonKhoVM
    {
        public int MaHh { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public string Hinh1 { get; set; }
        public string Hinh2 { get; set; }
        public string Hinh3 { get; set; }
        public double DonGia { get; set; }
        public string MoTaNgan { get; set; }
        public string TenLoai { get; set; }
        public int SoLuongHH { get; set; }
        public int SoLuongXuat { get; set; }
        public List<SoluongHH>? SoLuong { get; set; }
    }
    
}
