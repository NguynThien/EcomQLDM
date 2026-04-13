using System.ComponentModel.DataAnnotations;

namespace EcomQLDM.ViewModels
{
    public class HangHoaVM
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
    public class AddHangHoaVM
    {
        [Display(Name = "Mã Hàng Hóa")]
        [Required(ErrorMessage = "*")]
        public int MaHh { get; set; }

        [Display(Name = "Tên Hàng Hóa")]
        [Required(ErrorMessage = "*")]
        public string TenHH { get; set; }

        public string TenAlias { get; set; }

        [Display(Name = "Mã Loại")]
        [Required(ErrorMessage = "*")]
        public int MaLoai { get; set; }
        [Display(Name = "Loại")]
        [Required(ErrorMessage = "*")]
        public string TenLoai { get; set; }

        public string? MoTaDonVi { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "*")]
        public double DonGia { get; set; }

        public string? Hinh { get; set; }
        public string? Hinh2 { get; set; }
        public string? Hinh3 { get; set; }

        [Display(Name = "Ngày SX")]
        [DataType(DataType.Date)]
        public DateTime NgaySx { get; set; }

        public double GiamGia { get; set; }
        public int SoLanXem { get; set; }

        [Display(Name = "Mô Tả")]
        public string? MoTa { get; set; }

        [Display(Name = "Xuất Xứ")]
        public string? MoTaPhu1 { get; set; }

        [Display(Name = "Năm Ra Mắt")]
        public string? MoTaPhu2 { get; set; }

        [Display(Name = "Loại")]
        public string? MoTaPhu3 { get; set; }

        [Display(Name = "Kích Thước")]
        public string? MoTaPhu4 { get; set; }

        public string MaNcc { get; set; }

    }

    public class ChiTietHangHoaVM
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
        public string ChiTiet { get; set; }
        public int DiemDanhGia { get; set; }
        public int SoLuongTon { get; set; }


        public string MoTaPhu1 { get; set; }
        public string MoTaPhu2 { get; set; }
        public string MoTaPhu3 { get; set; }
        public string MoTaPhu4 { get; set; }

    }

    public class DeleteHangHoaVM
    {
        [Display(Name = "Car ID")]
        [Required(ErrorMessage = "*")]
        public int MaHh { get; set; }

    }

    public class PhanLoaiHHVM
    {
        public List<HangHoaVM>? HhLoai1 { get; set; }
        public List<HangHoaVM>? HhLoai2 { get; set; }
        public List<HangHoaVM>? HhLoai3 { get; set; }
        public List<HangHoaVM>? HhLoai4 { get; set; }
        public List<HangHoaVM>? HhLoai5 { get; set; }
        public List<HangHoaVM>? HhLoai6 { get; set; }
        public List<HangHoaVM>? HhLoai7 { get; set; }
        public List<HangHoaVM>? HhLoai8 { get; set; }

        public List<HangHoaVM> hh2Data { get; set; }

    }

    public class DoanhThuVM
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

        public double MyProperty { get; set; }
    }
}
