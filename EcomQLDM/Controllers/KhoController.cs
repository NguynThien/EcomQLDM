using AutoMapper;
using EcomQLDM.Data;
using EcomQLDM.Helpers;
using EcomQLDM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcomQLDM.Controllers
{
    public class KhoController : Controller
    {
        private readonly TmdtdatabaseContext db;
        private readonly IMapper _mapper;

        public KhoController(TmdtdatabaseContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        #region DanhSachKho
        public IActionResult DanhSachKho()
        {
            var khoHangs = db.Khos.AsQueryable();
            
            var result = khoHangs
                .Select(p => new KhoVM
                {
                    MaKho = p.MaKho,
                    TenKho = p.TenKho,
                    DiaChi = p.DiaChi,
                    HoTenQuanDoc = p.HoTenQuanDoc,
                    DienThoai = p.DienThoai,
                    Email = p.Email ?? ""
                });

            return View(result);
        }
        #endregion

        #region SearchMaKho
        public IActionResult SearchMaKho(string? query)
        {
            var khoHangs = db.Khos.AsQueryable();
            if (query != null)
            {
                khoHangs = khoHangs.Where(p => p.MaKho.Contains(query));
            }

            var result = khoHangs
                .Select(p => new KhoVM
                {
                    MaKho = p.MaKho,
                    TenKho = p.TenKho,
                    DiaChi = p.DiaChi,
                    HoTenQuanDoc = p.HoTenQuanDoc,
                    DienThoai = p.DienThoai,
                    Email = p.Email ?? ""
                });

            return View(result);
        }
        #endregion

        #region SearchTenKho
        public IActionResult SearchTenKho(string? query)
        {
            var khoHangs = db.Khos.AsQueryable();
            if (query != null)
            {
                khoHangs = khoHangs.Where(p => p.TenKho.Contains(query));
            }

            var result = khoHangs
                .Select(p => new KhoVM
                {
                    MaKho = p.MaKho,
                    TenKho = p.TenKho,
                    DiaChi = p.DiaChi,
                    HoTenQuanDoc = p.HoTenQuanDoc,
                    DienThoai = p.DienThoai,
                    Email = p.Email ?? ""
                });

            return View(result);
        }
        #endregion

        #region SearchTenQD
        public IActionResult SearchTenQD(string? query)
        {
            var khoHangs = db.Khos.AsQueryable();
            if (query != null)
            {
                khoHangs = khoHangs.Where(p => p.HoTenQuanDoc.Contains(query));
            }

            var result = khoHangs
                .Select(p => new KhoVM
                {
                    MaKho = p.MaKho,
                    TenKho = p.TenKho,
                    DiaChi = p.DiaChi,
                    HoTenQuanDoc = p.HoTenQuanDoc,
                    DienThoai = p.DienThoai,
                    Email = p.Email ?? ""
                });

            return View(result);
        }
        #endregion

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }

        #region ChiTietKho
        public IActionResult ChiTietKho(string id)
        {

            var data = db.Khos
                .SingleOrDefault(p => p.MaKho == id);

            if (data == null)
            {
                TempData["Message"] = $"No result for Kho {id}";
                return Redirect("/404");
            }
            var khos = db.Khos.AsQueryable();
            var ttKho = khos
                .Where(p => p.MaKho == data.MaKho)
                .Select(p => new KhoVM
                {
                    MaKho = data.MaKho,
                    TenKho = data.TenKho,
                    DiaChi = data.DiaChi,
                    HoTenQuanDoc = data.HoTenQuanDoc,
                    DienThoai = data.DienThoai,
                    Email = data.Email ?? ""
                }).ToList();

            var phieuNhaps = db.PhieuNhaps.AsQueryable();
            var dsPN = phieuNhaps
                .Where(p => p.MaKho == data.MaKho)
                .Select(p => new PhieuNhapVM
                {
                    MaPN = p.MaPn,
                    MaKho = p.MaKho,
                    NgayLapPhieu = p.NgayLapPhieu,
                    NgayNhapHang = p.NgayNhapHang
                }).ToList();

            var phieuXuats = db.PhieuXuats.AsQueryable();
            var dsPX = phieuXuats
                .Where(p => p.MaKho == data.MaKho)
                .Select(p => new PhieuXuatVM
                {
                    MaPX = p.MaPx,
                    MaKho = p.MaKho,
                    NgayLapPhieu = p.NgayLapPhieu,
                    NgayNhapHang = p.NgayXuatHang
                }).ToList();

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
                SoLuongHH = ChiTietPn.Where(g => g.MaHh == p.MaHh && g.MaPnNavigation.MaKho == data.MaKho).Select(g => new SoluongHH
                {
                    SoLuongNhap = g.SoLuong,
                }).Sum(g => g.SoLuongNhap),
                SoLuongXuat = ChiTietPx.Where(g => g.MaHh == p.MaHh && g.MaPxNavigation.MaKho == data.MaKho).Select(g => new SoluongHH
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
                }).ToList();


            var result = new QLKhoVM
            {
                ThongTinKho = ttKho,
                DanhSachPN = dsPN,
                DanhSachPX = dsPX,
                DanhSachHH = hangs
            };

            return View(result);

        }
        #endregion


        #region ThemKhoHang
        [HttpGet]
        public IActionResult ThemKhoHang()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ThemKhoHang(KhoVM model)
        {
            var kho = new Kho
            {
                MaKho = model.MaKho,
                TenKho = model.TenKho,
                DiaChi = model.DiaChi,
                HoTenQuanDoc = model.HoTenQuanDoc,
                DienThoai = model.DienThoai,
                Email = model.Email ?? ""
            };

            db.Add(kho);
            db.SaveChanges();

            return RedirectToAction("DanhSachKho", "Kho");
        }
        #endregion

        #region UpdateKho
        public IActionResult UpdateKho(string id)
        {

            var data = db.Khos.SingleOrDefault(p => p.MaKho == id);

            if (data == null)
            {
                TempData["Message"] = $"No result for Kho {id}";
                return Redirect("/404");
            }

            var result = new KhoVM
            {
                MaKho = data.MaKho,
                TenKho = data.TenKho,
                DiaChi = data.DiaChi,
                HoTenQuanDoc = data.HoTenQuanDoc,
                DienThoai = data.DienThoai,
                Email = data.Email ?? ""
            };
            return View(result);

        }

        [HttpGet]
        public IActionResult CapNhatKhoHang()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CapNhatKhoHang(KhoVM model)
        {
            var kho = new Kho
            {
                MaKho = model.MaKho,
                TenKho = model.TenKho,
                DiaChi = model.DiaChi,
                HoTenQuanDoc = model.HoTenQuanDoc,
                DienThoai = model.DienThoai,
                Email = model.Email ?? ""
            };

            db.Update(kho);
            db.SaveChanges();

            return RedirectToAction("DanhSachKho", "Kho");
        }
        #endregion

        #region DanhSachPhieuNhap
        public IActionResult DanhSachPhieuNhap()
        {
            var phieuNhaps = db.PhieuNhaps.AsQueryable();

            var result = phieuNhaps
                .Select(p => new PhieuNhapVM
                {
                    MaPN = p.MaPn,
                    MaKho = p.MaKho,
                    NgayLapPhieu = p.NgayLapPhieu,
                    NgayNhapHang = p.NgayNhapHang
                });

            return View(result);
        }

        #endregion

        #region SearchPhieuNhap
        public IActionResult SearchPhieuNhap(int? query)
        {
            var phieuNhaps = db.PhieuNhaps.AsQueryable();
            if (query != null)
            {
                phieuNhaps = phieuNhaps.Where(p => p.MaPn == query);
            }

            var result = phieuNhaps
                .Select(p => new PhieuNhapVM
                {
                    MaPN = p.MaPn,
                    MaKho = p.MaKho,
                    NgayLapPhieu = p.NgayLapPhieu,
                    NgayNhapHang = p.NgayNhapHang
                });

            return View(result);
        }
        #endregion

        #region SearchPNKho
        public IActionResult SearchPNKho(string? query)
        {
            var phieuNhaps = db.PhieuNhaps.AsQueryable();
            if (query != null)
            {
                phieuNhaps = phieuNhaps.Where(p => p.MaKho.Contains(query));
            }

            var result = phieuNhaps
                .Select(p => new PhieuNhapVM
                {
                    MaPN = p.MaPn,
                    MaKho = p.MaKho,
                    NgayLapPhieu = p.NgayLapPhieu,
                    NgayNhapHang = p.NgayNhapHang
                });

            return View(result);
        }
        #endregion

        #region DetailPN
        public IActionResult DetailPN(string id)
        {

            var data = db.Khos
                .SingleOrDefault(p => p.MaKho == id);

            if (data == null)
            {
                TempData["Message"] = $"No result for Kho {id}";
                return Redirect("/404");
            }
            var khos = db.Khos.AsQueryable();
            var ttKho = khos
                .Where(p => p.MaKho == data.MaKho)
                .Select(p => new KhoVM
                {
                    MaKho = data.MaKho,
                    TenKho = data.TenKho,
                    DiaChi = data.DiaChi,
                    HoTenQuanDoc = data.HoTenQuanDoc,
                    DienThoai = data.DienThoai,
                    Email = data.Email ?? ""
                }).ToList();

            var phieuNhaps = db.PhieuNhaps.AsQueryable();
            var dsPN = phieuNhaps
                .Where(p => p.MaKho == data.MaKho)
                .Select(p => new PhieuNhapVM
                {
                    MaPN = p.MaPn,
                    MaKho = p.MaKho,
                    NgayLapPhieu = p.NgayLapPhieu,
                    NgayNhapHang = p.NgayNhapHang
                }).ToList();

            var phieuXuats = db.PhieuXuats.AsQueryable();
            var dsPX = phieuXuats
                .Where(p => p.MaKho == data.MaKho)
                .Select(p => new PhieuXuatVM
                {
                    MaPX = p.MaPx,
                    MaKho = p.MaKho,
                    NgayLapPhieu = p.NgayLapPhieu,
                    NgayNhapHang = p.NgayXuatHang
                }).ToList();

            var result = new QLKhoVM
            {
                ThongTinKho = ttKho,
                DanhSachPN = dsPN,
                DanhSachPX = dsPX
            };

            return View(result);

        }
        #endregion

        #region ThemPhieuNhap
        [HttpGet]
        public IActionResult ThemPhieuNhap()
        {
            ViewData["MaKho"] = new SelectList(db.Khos, "MaKho", "MaKho");
            return View();
        }

        [HttpPost]
        public IActionResult ThemPhieuNhap(PhieuNhapVM model)
        {
            var pn = new PhieuNhap
            {
                MaKho = model.MaKho,
                NgayLapPhieu = DateOnly.FromDateTime(DateTime.Now),
                NgayNhapHang = DateOnly.FromDateTime(DateTime.Now)
            };

            db.Add(pn);
            db.SaveChanges();

            return RedirectToAction("DanhSachPhieuNhap", "Kho");
        }
        #endregion

        #region NhapHang
        [HttpGet]
        public IActionResult NhapHang(string maKho)
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

            var hangs = ketQuaHang
                .OrderByDescending(x => x.MaHh)
                .Take(8)
                .OrderBy(x => x.MaHh)
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
                }).ToList();

            ViewData["HangHoa"] = hangs;
            ViewData["TenHh"] = new SelectList(db.HangHoas, "TenHh", "TenHh");
            ViewBag.MaKho = maKho;
            return View();
        }
        
        [HttpPost]
        public IActionResult NhapHang(ChiTietPNVM model, string maKho)
        {
            var pn = new PhieuNhap
            {
                MaKho = maKho,
                NgayLapPhieu = DateOnly.FromDateTime(DateTime.Now),
                NgayNhapHang = DateOnly.FromDateTime(DateTime.Now)
            };

            db.Add(pn);
            db.SaveChanges();
            var phieuNhap = db.PhieuNhaps.OrderBy(pn => pn.MaPn).Last();
            var data = db.HangHoas.SingleOrDefault(p => p.TenHh == model.TenHH);
            var ctpn = new ChiTietPn
            {
                MaPn = phieuNhap.MaPn,
                MaHh = data.MaHh,
                SoLuong = model.SoLuong
            };

            db.Add(ctpn);
            db.SaveChanges();

            return RedirectToAction("NhapThemHang", new { id = phieuNhap.MaPn });
        }

        [HttpGet]
        public IActionResult NhapThemHang(int id)
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

            var hangs = ketQuaHang
                .OrderByDescending(x => x.MaHh)
                .Take(8)
                .OrderBy(x => x.MaHh)
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
                }).ToList();

            ViewData["HangHoa"] = hangs;
            ViewData["TenHh"] = new SelectList(db.HangHoas, "TenHh", "TenHh");
            ViewBag.MaPN = id;
            return View();
        }

        [HttpPost]
        public IActionResult NhapThemHang(ChiTietPNVM model, int maPN)
        {
            var data = db.HangHoas.SingleOrDefault(p => p.TenHh == model.TenHH);
            var ctpn = new ChiTietPn
            {
                MaPn = maPN,
                MaHh = data.MaHh,
                SoLuong = model.SoLuong
            };

            db.Add(ctpn);
            db.SaveChanges();

            return RedirectToAction("NhapThemHang", new { id = maPN });
        }

        #endregion

        #region ChiTietPN
        public IActionResult ChiTietPN(int id)
        {
            var phieuNhaps = db.PhieuNhaps.SingleOrDefault(p => p.MaPn == id);
            var ctPhieuNhaps = db.ChiTietPns.AsQueryable();
            var dsctPN = ctPhieuNhaps
                .Where(p => p.MaPn == id)
                .Select(p => new ChiTietPn
                {
                    MaCtpn = p.MaCtpn,
                    MaPn = p.MaPn,
                    MaHh = p.MaHh,
                    MaHhNavigation = p.MaHhNavigation,
                    SoLuong = p.SoLuong
                }).ToList();

            var tenK = db.Khos.SingleOrDefault(p => p.MaKho == phieuNhaps.MaKho).TenKho;
            var htqD = db.Khos.SingleOrDefault(p => p.MaKho == phieuNhaps.MaKho).HoTenQuanDoc;

            var pn = new ChiTietPNVM
            {
                //TenKho = phieuNhaps.MakHoNavigation.TenKho,
                //HoTenQuanDoc = phieuNhaps.MakHoNavigation.HoTenQuanDoc,
                TenKho = tenK,
                HoTenQuanDoc = htqD,
                MaPN = id,
                DSctpn = dsctPN
            };
            return View(pn);

        }
        #endregion

        #region NhapHang
        [HttpGet]
        public IActionResult XuatHang(string maKho)
        {
            ViewData["TenHh"] = new SelectList(db.HangHoas, "TenHh", "TenHh");
            ViewBag.MaKho = maKho;
            return View();
        }

        [HttpPost]
        public IActionResult XuatHang(ChiTietPXVM model, string maKho)
        {
            var px = new PhieuXuat
            {
                MaKho = maKho,
                NgayLapPhieu = DateOnly.FromDateTime(DateTime.Now),
                NgayXuatHang = DateOnly.FromDateTime(DateTime.Now)
            };

            db.Add(px);
            db.SaveChanges();
            var phieuXuat = db.PhieuXuats.OrderBy(px => px.MaPx).Last();
            var data = db.HangHoas.SingleOrDefault(p => p.TenHh == model.TenHH);
            var ctpx = new ChiTietPx
            {
                MaPx = phieuXuat.MaPx,
                MaHh = data.MaHh,
                SoLuong = model.SoLuong
            };

            db.Add(ctpx);
            db.SaveChanges();

            return RedirectToAction("XuatThemHang", new { id = phieuXuat.MaPx });
        }

        [HttpGet]
        public IActionResult XuatThemHang(int id)
        {
            ViewData["TenHh"] = new SelectList(db.HangHoas, "TenHh", "TenHh");
            ViewBag.MaPX = id;
            return View();
        }

        [HttpPost]
        public IActionResult XuatThemHang(ChiTietPXVM model, int maPX)
        {
            var data = db.HangHoas.SingleOrDefault(p => p.TenHh == model.TenHH);
            var ctpx = new ChiTietPx
            {
                MaPx = maPX,
                MaHh = data.MaHh,
                SoLuong = model.SoLuong
            };

            db.Add(ctpx);
            db.SaveChanges();

            return RedirectToAction("XuatThemHang", new { id = maPX });
        }

        #endregion

        #region ChiTietPN
        public IActionResult ChiTietPX(int id)
        {
            var phieuXuats = db.PhieuXuats.SingleOrDefault(p => p.MaPx == id);
            var ctPhieuXuats = db.ChiTietPxes.AsQueryable();
            var dsctPX = ctPhieuXuats
                .Where(p => p.MaPx == id)
                .Select(p => new ChiTietPx
                {
                    MaCtpx = p.MaCtpx,
                    MaPx = p.MaPx,
                    MaHh = p.MaHh,
                    MaHhNavigation = p.MaHhNavigation,
                    SoLuong = p.SoLuong
                }).ToList();

            var tenK = db.Khos.SingleOrDefault(p => p.MaKho == phieuXuats.MaKho).TenKho;
            var htqD = db.Khos.SingleOrDefault(p => p.MaKho == phieuXuats.MaKho).HoTenQuanDoc;

            var px = new ChiTietPXVM
            {
                TenKho = tenK,
                HoTenQuanDoc = htqD,
                MaPX = id,
                DSctpx = dsctPX
            };
            return View(px);

        }
        #endregion
    }
}
