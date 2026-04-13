using EcomQLDM.Data;

namespace EcomQLDM.ViewModels
{
    public class HoaDonVM
    {
        public int MaHD { get; set; }
        public string MaKH { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayCan { get; set; }
        public DateTime NgayGiao { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string CachThanhToan { get; set; }
        public string CachVanChuyen { get; set; }
        public double PhiVanChuyen { get; set; }
        public int MaTrangThai { get; set; }
        public string MaNV { get; set; }
        public string GhiChu { get; set; }
    }

    public class LichSuHD
    {
        public List<KhachHangVM>? ThongTinKH { get; set; }
        public List<HoaDonVM>? DanhSachHD { get; set; }
        public List<ChiTietHd>? DanhSachCTHD { get; set; }
        public List<DonHangVM>? DanhSachDH { get; set; }
        public List<DonHangVM>? DanhSachDH2 { get; set; }
    }
}
