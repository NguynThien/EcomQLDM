namespace EcomQLDM.ViewModels
{
    public class ShipperVM
    {
        public string MaShipper { get; set; }

        public string MatKhau { get; set; }

        public string TenShipper { get; set; }

        public string GioiTinh { get; set; }

        public string Email { get; set; }

        public string DienThoai { get; set; }

        public int HieuLuc { get; set; }

        public int? SoDonGiao { get; set; }

        public double? DiemDanhGia { get; set; }
        public bool GioiTinhBool { get; set; } = true;
    }
}
