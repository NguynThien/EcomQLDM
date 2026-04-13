using AutoMapper;
using EcomQLDM.Data;
using EcomQLDM.Helpers;
using EcomQLDM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcomQLDM.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly TmdtdatabaseContext db;
        private readonly IMapper _mapper;

        public HoaDonController(TmdtdatabaseContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        public IActionResult Index(int? trangthai)
        {
            var hoaDons = db.HoaDons.AsQueryable();
            if (trangthai.HasValue)
            {
                hoaDons = hoaDons.Where(p => p.MaTrangThai == trangthai.Value);
            }

            var result = hoaDons
                .Select(p => new HoaDonVM
                {
                    MaHD = p.MaHd,
                    MaKH = p.MaKh,
                    NgayDat = p.NgayDat,
                    NgayCan = p.NgayCan ?? DateTime.Now,
                    NgayGiao = p.NgayGiao ?? DateTime.Now,
                    HoTen = p.HoTen,
                    DiaChi = p.DiaChi,
                    CachThanhToan = p.CachThanhToan,
                    CachVanChuyen = p.CachVanChuyen,
                    PhiVanChuyen = p.PhiVanChuyen,
                    MaTrangThai = p.MaTrangThai,
                    MaNV = p.MaNv ?? "",
                    GhiChu = p.GhiChu ?? "",
                });

            return View(result);
        }
        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }
        public IActionResult History()
        {
            var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID).Value;

            var hoaDons = db.HoaDons.AsQueryable();
            var hd = hoaDons
                .Where(p => p.MaKh == customerId)
                .Select(p => new HoaDonVM
                {
                    MaHD = p.MaHd,
                    MaKH = p.MaKh,
                    NgayDat = p.NgayDat,
                    NgayCan = p.NgayCan ?? DateTime.Now,
                    NgayGiao = p.NgayGiao ?? DateTime.Now,
                    HoTen = p.HoTen,
                    DiaChi = p.DiaChi,
                    CachThanhToan = p.CachThanhToan,
                    CachVanChuyen = p.CachVanChuyen,
                    PhiVanChuyen = p.PhiVanChuyen,
                    MaTrangThai = p.MaTrangThai,
                    MaNV = p.MaNv ?? "",
                    GhiChu = p.GhiChu ?? "",
                }).ToList();

            var data = db.KhachHangs.SingleOrDefault(p => p.MaKh == customerId);
            string gender;
            if (data.GioiTinh == true)
            {
                gender = "Nam";
            }
            else
            {
                gender = "Nữ";
            }
            string image;
            if (data.Hinh == null || data.Hinh == "")
            {
                image = "unknown_avatar.jpg";
            }
            else
            {
                image = data.Hinh;
            }
            var khachHangs = db.KhachHangs.AsQueryable();
            var kh = khachHangs
                .Where(p => p.MaKh == customerId)
                .Select(p => new KhachHangVM
                {
                    Hinh = image ?? string.Empty,
                    MaKh = data.MaKh,
                    HoTen = data.HoTen,
                    GioiTinh = gender,
                    NgaySinh = DateOnly.FromDateTime(data.NgaySinh),
                    DiaChi = data.DiaChi ?? string.Empty,
                    Email = data.Email ?? string.Empty,
                    DienThoai = data.DienThoai ?? string.Empty
                }).ToList();

            var chiTietHds = db.ChiTietHds.AsQueryable();
            var cthd = new List<ChiTietHd>();
            foreach (var item in hd)
            {
                var models = chiTietHds
                    .Where(p => p.MaHd == item.MaHD)
                    .Select(p => new ChiTietHd
                    {
                        MaHd = item.MaHD,
                        SoLuong = p.SoLuong,
                        DonGia = p.DonGia,
                        MaHh = p.MaHh,
                        GiamGia = p.GiamGia,
                        MaHhNavigation = p.MaHhNavigation,
                    }).ToList();
                cthd.AddRange(models);
            }

            var hangHoa = db.HangHoas.AsQueryable();
            var donHang = db.DonHangs.AsQueryable();
            var dh = donHang
                .Where(p => p.MaKh == customerId && (p.MaTrangThai == 1 || p.MaTrangThai == 2))
                .Select(p => new DonHangVM
                {
                    MaDh = p.MaDh,
                    HangHoa = p.HangHoa,
                    TenHang = hangHoa.SingleOrDefault(q => q.MaHh == p.HangHoa).TenHh,
                    Hinh = splitHinh(hangHoa.SingleOrDefault(q => q.MaHh == p.HangHoa).Hinh)[0],
                    MaKh = p.MaKh,
                    MaTrangThai = p.MaTrangThai,
                    TrangThai = p.MaTrangThaiNavigation.TenTrangThai,
                    DiaChiLayHang = p.DiaChiLayHang,
                    DiaChiGiaoHang = data.DiaChi ?? string.Empty,
                    NgayLapDh = p.NgayLapDh,
                    GhiChu = p.GhiChu ?? ""
                }).ToList();

            var dh2 = donHang
                .Where(p => p.MaKh == customerId && (p.MaTrangThai != 1 && p.MaTrangThai != 2))
                .Select(p => new DonHangVM
                {
                    MaDh = p.MaDh,
                    HangHoa = p.HangHoa,
                    TenHang = hangHoa.SingleOrDefault(q => q.MaHh == p.HangHoa).TenHh,
                    Hinh = splitHinh(hangHoa.SingleOrDefault(q => q.MaHh == p.HangHoa).Hinh)[0],
                    MaKh = p.MaKh,
                    MaTrangThai = p.MaTrangThai,
                    TrangThai = p.MaTrangThaiNavigation.TenTrangThai,
                    DiaChiLayHang = p.DiaChiLayHang,
                    DiaChiGiaoHang = data.DiaChi ?? string.Empty,
                    NgayLapDh = p.NgayLapDh,
                    GhiChu = p.GhiChu ?? ""
                }).ToList();

            var result = new LichSuHD
            {
                ThongTinKH = kh,
                DanhSachHD = hd,
                DanhSachCTHD = cthd,
                DanhSachDH = dh,
                DanhSachDH2 = dh2
            };
            return View(result);
        }

        public IActionResult HuyDonHang(int id)
        {
            
            var data = db.DonHangs.SingleOrDefault(p => p.MaDh == id);

            if (data == null)
            {
                TempData["Message"] = $"No result for product {id}";
                return Redirect("/404");
            }

            data.MaTrangThai = 6;
            db.SaveChanges();

            return RedirectToAction("History", "HoaDon");

        }

    }
}
