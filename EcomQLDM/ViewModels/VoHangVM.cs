namespace EcomQLDM.ViewModels
{
    public class VoHangVM
    {
        public int MaVH { get; set; }
        public string MaKH { get; set; }
        public int MaHH { get; set; }
        public int SoLuong { get; set; }
        public int HieuLuc { get; set; }
        public string TenHH { get; set; }
        public string MaNCC { get; set; }
        public double DonGia { get; set; }
        public string Hinh { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public string MoTaNgan1 { get; set; }
        public string MoTaNgan2 { get; set; }
        public string MoTaNgan3 { get; set; }
        public string MoTaNgan4 { get; set; }
    }
    public class ThanhTienVM
    {
        public string MaKH { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }

    }
}
