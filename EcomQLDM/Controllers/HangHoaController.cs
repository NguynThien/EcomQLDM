using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EcomQLDM.Data;
using EcomQLDM.Helpers;
using EcomQLDM.ViewModels;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System.Linq;

namespace EcomQLDM.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly TmdtdatabaseContext db;
        private readonly IMapper _mapper;


        public HangHoaController(TmdtdatabaseContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }
        
        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }
        public static string[] splitMoTa(string moTa)
        {
            string[] moTaSubs = moTa.Split('!');
            return moTaSubs;
        }
        public IActionResult Index(int? loai)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                hangHoas = hangHoas.Where(p => p.MaLoai == loai.Value);
            }

            var result = hangHoas
                .Select(p => new HangHoaVM
                {

                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = p.MoTaDonVi ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai
                });

            return View(result);
        }

        public IActionResult Home()
        {
            var hangHoas = db.HangHoas.AsQueryable();

            var ChiTietPn = db.ChiTietPns.AsQueryable();
            var ChiTietPx = db.ChiTietPxes.AsQueryable();

            var ketQuaHang = hangHoas.Select(p => new TonKhoVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                Hinh1 = splitHinh(p.Hinh)[0],
                Hinh2 = splitHinh(p.Hinh)[1],
                Hinh3 = splitHinh(p.Hinh)[2],
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai,
                SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                {
                    SoLuongNhap = g.SoLuong,
                }).Sum(g => g.SoLuongNhap),
                SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                {
                    SoLuongXuat = g.SoLuong,
                }).Sum(g => g.SoLuongXuat),
            });

            var loai1 = ketQuaHang
                .Where(p => p.TenLoai.Equals("Điều hòa") && (p.SoLuongHH - p.SoLuongXuat) > -1)
                .Take(4)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHH,
                    DonGia = p.DonGia,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = p.MoTaNgan ?? "",
                    TenLoai = p.TenLoai
                }).ToList();

            var loai2 = ketQuaHang
                .Where(p => p.TenLoai.Equals("Tủ lạnh") && (p.SoLuongHH - p.SoLuongXuat) > -1)
                .Take(4)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHH,
                    DonGia = p.DonGia,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = p.MoTaNgan ?? "",
                    TenLoai = p.TenLoai
                }).ToList();

            var loai3 = ketQuaHang
                .Where(p => p.TenLoai.Equals("Tivi") && (p.SoLuongHH - p.SoLuongXuat) > -1)
                .Take(4)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHH,
                    DonGia = p.DonGia,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = p.MoTaNgan ?? "",
                    TenLoai = p.TenLoai
                }).ToList();

            var loai4 = ketQuaHang
                .Where(p => p.TenLoai.Equals("Đồ dùng bếp") && (p.SoLuongHH - p.SoLuongXuat) > -1)
                .Take(4)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHH,
                    DonGia = p.DonGia,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = p.MoTaNgan ?? "",
                    TenLoai = p.TenLoai
                }).ToList();

            var loai5 = ketQuaHang
                .Where(p => p.TenLoai.Equals("Máy giặt") && (p.SoLuongHH - p.SoLuongXuat) > -1)
                .Take(4)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHH,
                    DonGia = p.DonGia,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = p.MoTaNgan ?? "",
                    TenLoai = p.TenLoai
                }).ToList();

            var model = new PhanLoaiHHVM
            {
                HhLoai1 = loai1,
                HhLoai2 = loai2,
                HhLoai3 = loai3,
                HhLoai4 = loai4,
                HhLoai5 = loai5,
            };

            return View(model);
        }
        
        public IActionResult Shop(int? loai, int? page)
        {
            var skipNum = 1;
            if (page.HasValue)
            {
                if (page == 1)
                {
                    skipNum = 1;
                }
                if (page == 2)
                {
                    skipNum = 2;
                }
                if (page == 3)
                {
                    skipNum = 3;
                }
            }
            var hangHoas = db.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                hangHoas = hangHoas.Where(p => p.MaLoai == loai.Value);
            }

            var ChiTietPn = db.ChiTietPns.AsQueryable();
            var ChiTietPx = db.ChiTietPxes.AsQueryable();

            var ketQuaHang = hangHoas.Select(p => new TonKhoVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                Hinh1 = splitHinh(p.Hinh)[0],
                Hinh2 = splitHinh(p.Hinh)[1],
                Hinh3 = splitHinh(p.Hinh)[2],
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai,
                SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                {
                    SoLuongNhap = g.SoLuong,
                }).Sum(g => g.SoLuongNhap),
                SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                {
                    SoLuongXuat = g.SoLuong,
                }).Sum(g => g.SoLuongXuat),
            });

            var hangs = ketQuaHang
                .Where(p => (p.SoLuongHH - p.SoLuongXuat) > 0)
                .Skip((skipNum - 1) * 12)
                .Take(12)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHH,
                    DonGia = p.DonGia,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = p.MoTaNgan ?? "",
                    TenLoai = p.TenLoai,
                    SoLuongHH = p.SoLuongHH - p.SoLuongXuat,
                });

            var p1 = "class=" + '\"' + "page-item active" + '\"';
            var p2 = "class=" + '\"' + "page-item active" + '\"';
            var p3 = "class=" + '\"' + "page-item active" + '\"';
            // class="page-item active"
            if (page.HasValue)
            {
                if (page == 1)
                {
                    p2 = "class=" + '\"' + "page-item" + '\"';
                    p3 = "class=" + '\"' + "page-item" + '\"';
                }
                if (page == 2)
                {
                    p1 = "class=" + '\"' + "page-item" + '\"';
                    p3 = "class=" + '\"' + "page-item" + '\"';
                }
                if (page == 3)
                {
                    p1 = "class=" + '\"' + "page-item" + '\"';
                    p2 = "class=" + '\"' + "page-item" + '\"';
                }
            }

            ViewBag.page1 = p1;
            ViewBag.page2 = p2;
            ViewBag.page3 = p3;
            return View(hangs);
        }

        public IActionResult ThuongHieu(int loai, string thuongHieu)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            
            var ChiTietPn = db.ChiTietPns.AsQueryable();
            var ChiTietPx = db.ChiTietPxes.AsQueryable();

            var ketQuaHang = hangHoas
                .Where(p => p.MaLoai == loai && p.MaNcc.Equals(thuongHieu))
                .Select(p => new TonKhoVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                Hinh1 = splitHinh(p.Hinh)[0],
                Hinh2 = splitHinh(p.Hinh)[1],
                Hinh3 = splitHinh(p.Hinh)[2],
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai,
                SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                {
                    SoLuongNhap = g.SoLuong,
                }).Sum(g => g.SoLuongNhap),
                SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                {
                    SoLuongXuat = g.SoLuong,
                }).Sum(g => g.SoLuongXuat),
            });

            var hangs = ketQuaHang
                .Where(p => (p.SoLuongHH - p.SoLuongXuat) > 0)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHH,
                    DonGia = p.DonGia,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = p.MoTaNgan ?? "",
                    TenLoai = p.TenLoai,
                    SoLuongHH = p.SoLuongHH - p.SoLuongXuat,
                });

            return View(hangs);
        }

        public IActionResult KichThuoc(int loai, string kichthuoc)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            var ChiTietPn = db.ChiTietPns.AsQueryable();
            var ChiTietPx = db.ChiTietPxes.AsQueryable();

            var ketQuaHang = hangHoas
                .Where(p => p.MaLoai == loai)
                .Select(p => new TonKhoVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = splitMoTa(p.MoTaDonVi)[3] ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai,
                    SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongNhap = g.SoLuong,
                    }).Sum(g => g.SoLuongNhap),
                    SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongXuat = g.SoLuong,
                    }).Sum(g => g.SoLuongXuat),
                });

            if (loai == 3) // Tivi
            {
                ketQuaHang = hangHoas
                .Where(p => p.MaLoai == loai && p.DacDiem.StartsWith(kichthuoc))
                .Select(p => new TonKhoVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = splitMoTa(p.MoTaDonVi)[3] ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai,
                    SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongNhap = g.SoLuong,
                    }).Sum(g => g.SoLuongNhap),
                    SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongXuat = g.SoLuong,
                    }).Sum(g => g.SoLuongXuat),
                });
            }
            if (loai == 5) // Máy Giặt
            {
                var intKichThuoc = int.Parse(kichthuoc);
                var kichthuocP1 = (intKichThuoc + 1).ToString();
                var kichthuocP2 = (intKichThuoc + 2).ToString();
                var kichthuocP3 = (intKichThuoc + 3).ToString();
                var kichthuocP4 = (intKichThuoc + 4).ToString();
                ketQuaHang = hangHoas
                .Where(p => p.MaLoai == loai && (p.DacDiem.StartsWith(kichthuoc) || p.DacDiem.StartsWith(kichthuocP1) || p.DacDiem.StartsWith(kichthuocP2) || p.DacDiem.StartsWith(kichthuocP3) || p.DacDiem.StartsWith(kichthuocP4)))
                .Select(p => new TonKhoVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = splitMoTa(p.MoTaDonVi)[3] ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai,
                    SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongNhap = g.SoLuong,
                    }).Sum(g => g.SoLuongNhap),
                    SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongXuat = g.SoLuong,
                    }).Sum(g => g.SoLuongXuat),
                });
            }
            if (loai == 1) // Máy Lạnh
            {
                ketQuaHang = hangHoas
                .Where(p => p.MaLoai == loai && p.DacDiem.StartsWith(kichthuoc))
                .Select(p => new TonKhoVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = splitMoTa(p.MoTaDonVi)[3] ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai,
                    SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongNhap = g.SoLuong,
                    }).Sum(g => g.SoLuongNhap),
                    SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongXuat = g.SoLuong,
                    }).Sum(g => g.SoLuongXuat),
                });
            }
            if (loai == 2) // Tủ Lạnh
            {
                var intKichThuoc = int.Parse(kichthuoc);
                var kichthuocP1 = (intKichThuoc + 1).ToString();
                ketQuaHang = hangHoas
                .Where(p => p.MaLoai == loai && (p.DacDiem.StartsWith(kichthuoc) || p.DacDiem.StartsWith(kichthuocP1)))
                .Select(p => new TonKhoVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = splitMoTa(p.MoTaDonVi)[3] ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai,
                    SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongNhap = g.SoLuong,
                    }).Sum(g => g.SoLuongNhap),
                    SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongXuat = g.SoLuong,
                    }).Sum(g => g.SoLuongXuat),
                });
            }
            if (loai == 6) // Máy Lọc Nước
            {
                ketQuaHang = hangHoas
                .Where(p => p.MaLoai == loai && p.DacDiem.StartsWith(kichthuoc))
                .Select(p => new TonKhoVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = splitMoTa(p.MoTaDonVi)[3] ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai,
                    SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongNhap = g.SoLuong,
                    }).Sum(g => g.SoLuongNhap),
                    SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongXuat = g.SoLuong,
                    }).Sum(g => g.SoLuongXuat),
                });
            }
            if (loai == 4) // Đồ Dùng Bếp
            {
                if (kichthuoc == "1")
                {
                    kichthuoc = "Lò vi sóng";
                }
                if (kichthuoc == "2")
                {
                    kichthuoc = "Nồi cơm điện";
                }
                if (kichthuoc == "3")
                {
                    kichthuoc = "Chiên không dầu";
                }
                ketQuaHang = hangHoas
                .Where(p => p.MaLoai == loai && p.DacDiem.Contains(kichthuoc))
                .Select(p => new TonKhoVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = splitMoTa(p.MoTaDonVi)[3] ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai,
                    SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongNhap = g.SoLuong,
                    }).Sum(g => g.SoLuongNhap),
                    SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                    {
                        SoLuongXuat = g.SoLuong,
                    }).Sum(g => g.SoLuongXuat),
                });
            }
            var hangs = ketQuaHang
                .Where(p => (p.SoLuongHH - p.SoLuongXuat) > 0)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHH,
                    DonGia = p.DonGia,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = p.MoTaNgan ?? "",
                    TenLoai = p.TenLoai,
                    SoLuongHH = p.SoLuongHH - p.SoLuongXuat,
                });

            return View(hangs);
        }

        // Start of Admin
        public static string[] splitString(string str)
        {
            string[] strSubs = str.Split('!');
            return strSubs;
        }
        //[Authorize]
        public IActionResult Dashboard(string? query)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (query != null)
            {
                hangHoas = hangHoas.Where(p => p.TenHh.Contains(query));
            }

            var hh = hangHoas.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                Hinh1 = splitHinh(p.Hinh)[0],
                Hinh2 = splitHinh(p.Hinh)[1],
                Hinh3 = splitHinh(p.Hinh)[2],
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            }).ToList();

            var id = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "NhanVienID").Value;
            var data = db.NhanViens.SingleOrDefault(p => p.MaNv == id);
            var quyenHang = db.QuyenHangs.SingleOrDefault(q => q.MaQh == data.MaQh);
            if (data == null)
            {
                TempData["Message"] = $"No result for product {id}";
                return Redirect("/404");
            }

            string hoTen = splitString(data.HoTen)[0];

            string gender = splitString(data.HoTen)[1];

            string dienThoai = splitString(data.HoTen)[2];

            string email = splitString(data.Email)[0];

            string image = splitString(data.Email)[1];

            string diaChi = splitString(data.Email)[2];

            var result = new NhanVienVM
            {
                Hinh = image ?? string.Empty,
                MaNV = data.MaNv,
                TenQH = quyenHang.TenQh,
                HoTen = hoTen,
                GioiTinh = gender,
                DiaChi = diaChi ?? string.Empty,
                Email = email ?? string.Empty,
                DienThoai = dienThoai ?? string.Empty
            };
            var list = db.NhanViens.AsQueryable();
            var nhanVien = list
                .Where(p => p.MaNv == data.MaNv)
                .Take(1)
                .Select(p => new NhanVienVM
                {
                    Hinh = image ?? string.Empty,
                    MaNV = data.MaNv,
                    TenQH = quyenHang.TenQh,
                    HoTen = hoTen,
                    GioiTinh = gender,
                    DiaChi = diaChi ?? string.Empty,
                    Email = email ?? string.Empty,
                    DienThoai = dienThoai ?? string.Empty
                }).ToList();
            var phanQuyen = new PhanQuyenVM
            {
                phanQuyens = nhanVien
            };
            if (data.MaQh == 1) // Admin
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlHangHoa = "enabled",
                    QlHoaDon = "enabled",
                    QlKhachHang = "enabled",
                    QlNhanVien = "enabled",
                    QlShipper = "enabled",
                    QlKho = "enabled",
                    QlDoanhThu = "enabled",
                    Hinh = image ?? string.Empty
                    , hangHoas = hh
                };
                phanQuyen = pq;
            }
            if (data.MaQh == 2) // Quan Ly
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlHangHoa = "enabled",
                    QlHoaDon = "enabled",
                    QlKhachHang = "enabled",
                    QlNhanVien = "enabled",
                    QlDoanhThu = "enabled",
                    Hinh = image ?? string.Empty
                    ,
                    hangHoas = hh
                };
                phanQuyen = pq;
            }
            if (data.MaQh == 3) // Nhan Vien Ban Hang
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlHoaDon = "enabled",
                    QlKhachHang = "enabled",
                    Hinh = image ?? string.Empty
                    ,
                    hangHoas = hh
                };
                phanQuyen = pq;
            }
            if (data.MaQh == 4) // Nhan Vien Kho
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlHangHoa = "enabled",
                    QlHoaDon = "enabled",
                    QlKho = "enabled",
                    Hinh = image ?? string.Empty
                    ,
                    hangHoas = hh
                };
                phanQuyen = pq;
            }
            if (data.MaQh == 5) // Nhan Vien Cham Soc Khach Hang
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlHoaDon = "enabled",
                    QlKhachHang = "enabled",
                    Hinh = image ?? string.Empty
                    ,
                    hangHoas = hh
                };
                phanQuyen = pq;
            }
            if (data.MaQh == 6) // Ke Toan
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlDoanhThu = "enabled",
                    Hinh = image ?? string.Empty
                    ,
                    hangHoas = hh
                };
                phanQuyen = pq;
            }
            

            return View(phanQuyen);
        }

        // End of Admin

        public IActionResult Search(string? query)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (query != null)
            {
                hangHoas = hangHoas.Where(p => p.TenHh.Contains(query));
            }

            var result = hangHoas.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                Hinh1 = splitHinh(p.Hinh)[0],
                Hinh2 = splitHinh(p.Hinh)[1],
                Hinh3 = splitHinh(p.Hinh)[2],
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });

            return View(result);
        }

        public IActionResult Detail(int id)
        {
            var data = db.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);

            if (data == null)
            {
                TempData["Message"] = $"No result for product {id}";
                return Redirect("/404");
            }
            
            string[] subs = data.MoTaDonVi.Split('!');
            string[] hinhSubs = data.Hinh.Split('!');

            var ChiTietPn = db.ChiTietPns.AsQueryable();
            var ChiTietPx = db.ChiTietPxes.AsQueryable();

            var SoLuongHH = ChiTietPn.Where(g => g.MaHh == id).Select(g => new SoluongHH
            {
                SoLuongNhap = g.SoLuong,
            }).Sum(g => g.SoLuongNhap);
            var SoLuongXuat = ChiTietPx.Where(g => g.MaHh == id).Select(g => new SoluongHH
            {
                SoLuongXuat = g.SoLuong,
            }).Sum(g => g.SoLuongXuat);

            var result = new ChiTietHangHoaVM
            {
                MaHh = data.MaHh,
                TenHH = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? string.Empty,
                Hinh = data.Hinh ?? string.Empty,
                MoTaNgan = data.MoTaDonVi ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,

                Hinh1 = hinhSubs[0],
                Hinh2 = hinhSubs[1],
                Hinh3 = hinhSubs[2],

                MoTaPhu1 = subs[0],
                MoTaPhu2 = subs[1],
                MoTaPhu3 = subs[2],
                MoTaPhu4 = subs[3],

                SoLuongTon = SoLuongHH - SoLuongXuat,
                DiemDanhGia = 5,
            };
            return View(result);

        }

        #region AddProduct
        [HttpGet]
        public IActionResult AddHH()
        {
            ViewData["TenLoai"] = new SelectList(db.Loais, "TenLoai", "TenLoai");
            ViewData["MaNcc"] = new SelectList(db.NhaCungCaps, "MaNcc", "MaNcc");
            return View();
        }

        [HttpPost]
        public IActionResult AddHH(AddHangHoaVM model, IFormFile Hinh, IFormFile Hinh1, IFormFile Hinh2, IFormFile Hinh3)
        {
            var data = db.Loais.SingleOrDefault(p => p.TenLoai == model.TenLoai);
            
            if (Hinh2 == null)
            {
                Hinh2 = Hinh;
            }
            if (Hinh3 == null)
            {
                Hinh3 = Hinh;
            }
            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string IDENTITY_INSERT = "INSERT INTO HangHoa (TenHH, MaLoai, DonGia, Hinh, NgaySX, GiamGia, SoLanXem, MoTa, MoTaDonVi, MaNCC, HieuLuc, DacDiem) VALUES (N'" + model.TenHH + "', " + data.MaLoai + ", " + model.DonGia + ", N'" + Hinh.FileName + "!" + Hinh2.FileName + "!" + Hinh3.FileName + "!" + "', CAST(N'2009-07-31T07:00:00.000' AS DateTime), 0, 0, N'" + model.MoTa + "', N'" + model.MoTaPhu1 + "!" + model.MoTaPhu2 + "!" + model.MoTaPhu3 + "!" + model.MoTaPhu4 + "', N'" + model.MaNcc + "', 1, N'" + model.MoTaPhu4 + "')";
            SqlCommand comd = new SqlCommand(IDENTITY_INSERT, conn);

            try
            {
                conn.Open();
                comd.ExecuteNonQuery();

                if (Hinh != null)
                {
                    model.Hinh = MyUtil.UploadHinh(Hinh, "HangHoa");
                }
                if (Hinh2 != null)
                {
                    model.Hinh2 = MyUtil.UploadHinh(Hinh2, "HangHoa");
                }
                if (Hinh3 != null)
                {
                    model.Hinh3 = MyUtil.UploadHinh(Hinh3, "HangHoa");
                }

                db.SaveChanges();
                conn.Close();
                return RedirectToAction("Dashboard", "HangHoa");
            }
            catch (Exception ex)
            {
                var mess = $"{ex.Message}";
            }


            return View();
        }
        #endregion

        #region UpdateProduct
        
        [Authorize]
        public IActionResult UpdateHH(int id)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_NHANVIENID).Value;

            var data = db.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);

            var Loai = db.Loais.SingleOrDefault(p => p.MaLoai == data.MaLoai);

            var hieuLuc = db.NhanViens.SingleOrDefault(nv => nv.MaNv == userId);

            if (hieuLuc != null)
            {
                if (data == null)
                {
                    TempData["Message"] = $"No result for product {id}";
                    return Redirect("/404");
                }

                string[] subs = data.MoTaDonVi.Split('!');
                string[] hinhSubs = data.Hinh.Split('!');

                var result = new AddHangHoaVM
                {
                    MaHh = data.MaHh,
                    TenHH = data.TenHh,
                    DonGia = data.DonGia ?? 0,
                    MaLoai = data.MaLoai,
                    TenLoai = Loai.TenLoai,
                    //Hinh = data.Hinh,

                    Hinh = hinhSubs[0],
                    Hinh2 = hinhSubs[1],
                    Hinh3 = hinhSubs[2],

                    MoTaPhu1 = subs[0],
                    MoTaPhu2 = subs[1],
                    MoTaPhu3 = subs[2],
                    MoTaPhu4 = subs[3],

                    MoTa = data.MoTa,
                    MaNcc = data.MaNcc,
                };
                ViewData["TenLoai"] = new SelectList(db.Loais, "TenLoai", "TenLoai");
                ViewData["MaNcc"] = new SelectList(db.NhaCungCaps, "MaNcc", "MaNcc");
                return View(result);
            }
            return Redirect("/404");

        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(AddHangHoaVM model, IFormFile Hinh, IFormFile Hinh2, IFormFile Hinh3)
        {
            var data = db.Loais.SingleOrDefault(p => p.TenLoai == model.TenLoai);
            var hangHoas = db.HangHoas.SingleOrDefault(p => p.MaHh == model.MaHh);
            string[] hinhSubs = hangHoas.Hinh.Split('!');
            var fileHinh = "";
            var fileHinh2 = "";
            var fileHinh3 = "";
            if (Hinh == null)
            {
                fileHinh = hinhSubs[0];
            }
            else { fileHinh = Hinh.FileName; }
            if (Hinh2 == null)
            {
                fileHinh2 = hinhSubs[1];
            }
            else { fileHinh2 = Hinh2.FileName; }
            if (Hinh3 == null)
            {
                fileHinh3 = hinhSubs[2];
            }
            else { fileHinh3 = Hinh3.FileName; }
            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string UPDATE = "UPDATE HangHoa SET TenHH = N'" + model.TenHH + "', MaLoai = " + data.MaLoai + ", DonGia = " + model.DonGia + ", Hinh = N'" + fileHinh + "!" + fileHinh2 + "!" + fileHinh3 + "!" + "', MoTa = N'" + model.MoTa + "', MaNCC = N'" + model.MaNcc + "', MoTaDonVi = N'" + model.MoTaPhu1 + "!" + model.MoTaPhu2 + "!" + model.MoTaPhu3 + "!" + model.MoTaPhu4 + "' WHERE MaHH = " + model.MaHh + "";
            SqlCommand comd = new SqlCommand(UPDATE, conn);

            try
            {
                conn.Open();
                comd.ExecuteNonQuery();

                if (Hinh != null)
                {
                    model.Hinh = MyUtil.UploadHinh(Hinh, "HangHoa");
                }
                if (Hinh2 != null)
                {
                    model.Hinh2 = MyUtil.UploadHinh(Hinh2, "HangHoa");
                }
                if (Hinh3 != null)
                {
                    model.Hinh3 = MyUtil.UploadHinh(Hinh3, "HangHoa");
                }

                db.SaveChanges();
                conn.Close();
                return RedirectToAction("Dashboard", "HangHoa");
            }
            catch (Exception ex)
            {
                var mess = $"{ex.Message}";
            }
            return View();
        }
        #endregion

        #region DeleteProduct
        [Authorize]
        public IActionResult DeleteHH(int id)
        {
            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);

            var userId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_NHANVIENID).Value;

            var data = db.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);

            var hieuLuc = db.NhanViens.SingleOrDefault(nv => nv.MaNv == userId);

            if (hieuLuc != null)
            {
                if (data == null)
                {
                    TempData["Message"] = $"No result for product {id}";
                    return Redirect("/404");
                }
                var result = new ChiTietHangHoaVM
                {
                    MaHh = data.MaHh
                };

                string IDENTITY_DELETE = "DELETE HangHoa WHERE MaHH = " + result.MaHh;
                SqlCommand comd = new SqlCommand(IDENTITY_DELETE, conn);
                conn.Open();
                comd.ExecuteNonQuery();
                db.SaveChanges();
                conn.Close();

                return RedirectToAction("Dashboard", "HangHoa");
            }
            return Redirect("/404");

        }
        #endregion


        public IActionResult DisableHangHoa(int id)
        {
            //DBConnection dbc = new DBConnection();
            //SqlConnection conn = new SqlConnection(dbc.ConnectionString);

            var userId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_NHANVIENID).Value;

            var data = db.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);

            var hieuLuc = db.NhanViens.SingleOrDefault(nv => nv.MaNv == userId);

            if (hieuLuc != null)
            {
                if (data == null)
                {
                    TempData["Message"] = $"No result for product {id}";
                    return Redirect("/404");
                }

                data.HieuLuc = 0;
                db.SaveChanges();

                //string IDENTITY_DELETE = "DELETE HangHoa WHERE MaHH = " + result.MaHh;
                //SqlCommand comd = new SqlCommand(IDENTITY_DELETE, conn);
                //conn.Open();
                //comd.ExecuteNonQuery();
                //db.SaveChanges();
                //conn.Close();

                return RedirectToAction("Dashboard", "HangHoa");
            }
            return Redirect("/404");

        }

        public IActionResult ThongKe(int? loai)
        {
            var hangHoas = db.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                hangHoas = hangHoas.Where(p => p.MaLoai == loai.Value);
            }

            var ChiTietPn = db.ChiTietPns.AsQueryable();
            var ChiTietPx = db.ChiTietPxes.AsQueryable();

            var ketQuaHang = hangHoas.Select(p => new TonKhoVM
            {
                MaHh = p.MaHh,
                TenHH = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                Hinh1 = splitHinh(p.Hinh)[0],
                Hinh2 = splitHinh(p.Hinh)[1],
                Hinh3 = splitHinh(p.Hinh)[2],
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai,
                SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                {
                    SoLuongNhap = g.SoLuong,
                }).Sum(g => g.SoLuongNhap),
                SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh).Select(g => new SoluongHH
                {
                    SoLuongXuat = g.SoLuong,
                }).Sum(g => g.SoLuongXuat),
            });

            var hangs = ketQuaHang
                .OrderByDescending(p => p.SoLuongXuat)
                .Take(10)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHH,
                    DonGia = p.DonGia,
                    Hinh = p.Hinh ?? "",
                    Hinh1 = splitHinh(p.Hinh)[0],
                    Hinh2 = splitHinh(p.Hinh)[1],
                    Hinh3 = splitHinh(p.Hinh)[2],
                    MoTaNgan = p.MoTaNgan ?? "",
                    TenLoai = p.TenLoai,
                    SoLuongHH = p.SoLuongXuat, // Số lượng bán ra
                });
            
            return View(hangs);
        }

        public IActionResult DoanhThu()
        {
            var hangHoas = db.HangHoas.AsQueryable();
            var hh = new List<ChiTietDHVM>();
            
            var doanhThuTheoNgay = db.ChiTietDhs
                .Where(ctdh => ctdh.MaTrangThai == 5)
                .GroupBy(ctdh => ctdh.NgayNhanDh.Date)
                .Select(g => new ChiTietDHVM
                {
                    NgayNhanDh = g.Key,
                    Dongia = g.Sum(g => g.MaHhNavigation.DonGia) ?? 0
                })
                .OrderBy(x => x.NgayNhanDh);


            hh.AddRange(doanhThuTheoNgay);
            return View(hh);
        }

        public IActionResult DoanhThuThang()
        {
            var hangHoas = db.HangHoas.AsQueryable();
            var hh = new List<ChiTietDHVM>();

            var doanhThuTheoThang = db.ChiTietDhs
            .Where(ctdh => ctdh.MaTrangThai == 5)
            .GroupBy(ctdh => new { ctdh.NgayNhanDh.Year, ctdh.NgayNhanDh.Month })
            .Select(g => new ChiTietDHVM
            {
                MaCtdh = g.Key.Year,
                MaHh = g.Key.Month,
                Dongia = g.Sum(g => g.MaHhNavigation.DonGia) ?? 0
            })
            .OrderBy(x => x.MaCtdh)
            .ThenBy(x => x.MaHh);


            hh.AddRange(doanhThuTheoThang);
            return View(hh);
        }

        public IActionResult Sales(int id)
        {
            var hangHoas = db.ChiTietPxes.AsQueryable();
            var results = hangHoas
                .Where(p => p.MaHh == id)
                .Select(p => new ChiTietPxVM
                {
                    MaCtpx = p.MaCtpx,
                    MaPx = p.MaPx,
                    MaHh = p.MaHh,
                    SoLuong = p.SoLuong,
                    NgayLapPhieu = p.MaPxNavigation.NgayLapPhieu,
                }).ToList();
            var grouped = results
                .OrderBy(p => p.NgayLapPhieu)
                .GroupBy(p => p.NgayLapPhieu)
                .Select(p => new ChiTietPxVM
                {
                    NgayLapPhieu = p.Key,
                    SoLuong = p.Sum(p => p.SoLuong)
                });
            return View(grouped);
        }

        public IActionResult TopKH()
        {
            var khachHangs = db.KhachHangs.AsQueryable();
            var donHangs = db.DonHangs.AsQueryable();
            var results = donHangs
                .Where(p => p.MaTrangThai == 5)
                .Select(p => new DonHang
                {
                    MaDh = p.MaDh,
                    HangHoa = p.HangHoa,
                    MaKh = p.MaKh,
                    MaTrangThai = p.MaTrangThai,
                    DiaChiLayHang = p.DiaChiLayHang,
                    DiaChiGiaoHang = p.DiaChiGiaoHang,
                    NgayLapDh = p.NgayLapDh,
                    GhiChu = p.GhiChu,

                });
            var grouped = results
                .GroupBy(p => p.MaKh)
                .Select(p => new XepHangKHVM
                {
                    MaKh = p.Key,
                    HoTen = khachHangs.SingleOrDefault(kh => kh.MaKh == p.Key).HoTen,
                    Hinh = khachHangs.SingleOrDefault(kh => kh.MaKh == p.Key).Hinh,
                    Email = khachHangs.SingleOrDefault(kh => kh.MaKh == p.Key).Email,
                    DienThoai = khachHangs.SingleOrDefault(kh => kh.MaKh == p.Key).DienThoai,
                    DiaChi = khachHangs.SingleOrDefault(kh => kh.MaKh == p.Key).DiaChi,
                    SoLuongMuaHang = p.Count()
                }).OrderByDescending(p => p.SoLuongMuaHang).ToList();

            var id = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "NhanVienID").Value;
            var data = db.NhanViens.SingleOrDefault(p => p.MaNv == id);
            var quyenHang = db.QuyenHangs.SingleOrDefault(q => q.MaQh == data.MaQh);
            if (data == null)
            {
                TempData["Message"] = $"No result for product {id}";
                return Redirect("/404");
            }

            string hoTen = splitString(data.HoTen)[0];

            string gender = splitString(data.HoTen)[1];

            string dienThoai = splitString(data.HoTen)[2];

            string email = splitString(data.Email)[0];

            string image = splitString(data.Email)[1];

            string diaChi = splitString(data.Email)[2];

            var result = new NhanVienVM
            {
                Hinh = image ?? string.Empty,
                MaNV = data.MaNv,
                TenQH = quyenHang.TenQh,
                HoTen = hoTen,
                GioiTinh = gender,
                DiaChi = diaChi ?? string.Empty,
                Email = email ?? string.Empty,
                DienThoai = dienThoai ?? string.Empty
            };
            var list = db.NhanViens.AsQueryable();
            var nhanVien = list
                .Where(p => p.MaNv == data.MaNv)
                .Take(1)
                .Select(p => new NhanVienVM
                {
                    Hinh = image ?? string.Empty,
                    MaNV = data.MaNv,
                    TenQH = quyenHang.TenQh,
                    HoTen = hoTen,
                    GioiTinh = gender,
                    DiaChi = diaChi ?? string.Empty,
                    Email = email ?? string.Empty,
                    DienThoai = dienThoai ?? string.Empty
                }).ToList();
            var phanQuyen = new PhanQuyenVM
            {
                phanQuyens = nhanVien
            };
            if (data.MaQh == 1) // Admin
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlHangHoa = "enabled",
                    QlHoaDon = "enabled",
                    QlKhachHang = "enabled",
                    QlNhanVien = "enabled",
                    QlShipper = "enabled",
                    QlKho = "enabled",
                    QlDoanhThu = "enabled",
                    Hinh = image ?? string.Empty,
                    xepHangKHs = grouped
                };
                phanQuyen = pq;
            }
            if (data.MaQh == 2) // Quan Ly
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlHangHoa = "enabled",
                    QlHoaDon = "enabled",
                    QlKhachHang = "enabled",
                    QlNhanVien = "enabled",
                    QlDoanhThu = "enabled",
                    Hinh = image ?? string.Empty,
                    xepHangKHs = grouped
                };
                phanQuyen = pq;
            }
            if (data.MaQh == 3) // Nhan Vien Ban Hang
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlHoaDon = "enabled",
                    QlKhachHang = "enabled",
                    Hinh = image ?? string.Empty,
                    xepHangKHs = grouped
                };
                phanQuyen = pq;
            }
            if (data.MaQh == 4) // Nhan Vien Kho
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlHangHoa = "enabled",
                    QlHoaDon = "enabled",
                    QlKho = "enabled",
                    Hinh = image ?? string.Empty,
                    xepHangKHs = grouped
                };
                phanQuyen = pq;
            }
            if (data.MaQh == 5) // Nhan Vien Cham Soc Khach Hang
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlHoaDon = "enabled",
                    QlKhachHang = "enabled",
                    Hinh = image ?? string.Empty,
                    xepHangKHs = grouped
                };
                phanQuyen = pq;
            }
            if (data.MaQh == 6) // Ke Toan
            {
                var pq = new PhanQuyenVM
                {
                    phanQuyens = nhanVien,
                    QlDoanhThu = "enabled",
                    Hinh = image ?? string.Empty,
                    xepHangKHs = grouped
                };
                phanQuyen = pq;
            }

            return View(phanQuyen);

        }

    }
}
