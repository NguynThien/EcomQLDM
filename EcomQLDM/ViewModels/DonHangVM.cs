namespace EcomQLDM.ViewModels
{
    public class DonHangVM
    {
        public int MaDh { get; set; }
        public int HangHoa { get; set; }
        public string TenHang { get; set; }
        public string Hinh { get; set; }
        public string MaKh { get; set; }
        public int MaTrangThai { get; set; }
        public string TrangThai { get; set; }
        public string DiaChiLayHang { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public DateTime NgayLapDh { get; set; }
        public string GhiChu { get; set; }
    }
    public class ListDHVM
    {
        public List<ShipperVM> shipperVMs { get; set; }
        public List<DonHangVM> donHangVMs { get; set; }

    }
    public class ChiTietDHVM
    {
        public int MaCtdh { get; set; }

        public int MaDh { get; set; }

        public int MaTrangThai { get; set; }

        public int MaHh { get; set; }

        public string MaShipper { get; set; } = null!;

        public DateTime NgayCapNhat { get; set; }

        public DateTime NgayNhanDh { get; set; }

        public string DiaChi { get; set; } = null!;

        public string? GhiChu { get; set; }
        public string Hinh { get; set; }
        public string TenHang { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayDatHang { get; set; }
        public double Dongia { get; set; }
        public string NguoiNhan { get; set; }
        public string NguoiGiao { get; set; }
    }
}
