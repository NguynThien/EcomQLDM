using AutoMapper;
using EcomQLDM.Data;
using EcomQLDM.Helpers;
using EcomQLDM.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Data.SqlClient;

namespace EcomQLDM.Controllers
{
    public class ShipperController : Controller
    {

        private readonly TmdtdatabaseContext db;
        private readonly IMapper _mapper;


        public ShipperController(TmdtdatabaseContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        public static string[] splitString(string str)
        {
            string[] strSubs = str.Split('!');
            return strSubs;
        }

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }

        #region DanhSachShipper
        public IActionResult DanhSachShipper()
        {
            var shipper = db.Shippers.AsQueryable();
            var result = shipper
                .Select(p => new ShipperVM
                {
                    MaShipper = p.MaShipper,
                    TenShipper = p.TenShipper,
                    GioiTinh = p.GioiTinh,
                    Email = p.Email,
                    DienThoai = p.DienThoai,
                    SoDonGiao = p.SoDonGiao,
                    DiemDanhGia = p.DiemDanhGia
                });
            return View(result);
        }

        #endregion

        #region DonHang
        public IActionResult DonHang()
        {
            var hangHoa = db.HangHoas.AsQueryable();
            var donHang = db.DonHangs.AsQueryable();
            var shippers = db.Shippers.AsQueryable();
            var dh = donHang
                .OrderByDescending(p => p.NgayLapDh)
                .Where(p => p.MaTrangThai == 1)
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
                    DiaChiGiaoHang = p.DiaChiGiaoHang,
                    NgayLapDh = p.NgayLapDh,
                    GhiChu = p.GhiChu ?? ""
                }).ToList();
            var maShipper = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "ShipperID").Value;
            var shipper = shippers
                .Where(p => p.MaShipper == maShipper)
                .Select(p => new ShipperVM
                {
                    MaShipper = p.MaShipper,
                    TenShipper = p.TenShipper,
                    GioiTinh = p.GioiTinh,
                    Email = p.Email,
                    DienThoai = p.DienThoai,
                    SoDonGiao = p.SoDonGiao,
                    DiemDanhGia = p.DiemDanhGia
                }).ToList();

            var listDHVM = new ListDHVM
            {
                shipperVMs = shipper,
                donHangVMs = dh,
            };

            return View(listDHVM);
        }

        #endregion

        #region ChiTietDonHang
        public IActionResult ChiTietDonHang(int id)
        {
            var maShipper = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "ShipperID").Value;
            
            var chiTietDHs = db.ChiTietDhs.AsQueryable();
            var ctdh = chiTietDHs.SingleOrDefault(p => p.MaDh == id);
            var donHang = db.DonHangs.AsQueryable();
            var dh = donHang.SingleOrDefault(p => p.MaDh == id);
            var hangHoa = db.HangHoas.AsQueryable();
            var hh = hangHoa.SingleOrDefault(p => p.MaHh == ctdh.MaHh);
            var khachHang = db.KhachHangs.AsQueryable();
            var kh = khachHang.SingleOrDefault(p => p.MaKh == dh.MaKh);
            var shipper = db.Shippers.AsQueryable();
            var sh = shipper.SingleOrDefault(p => p.MaShipper == maShipper);
            var result = new ChiTietDHVM
            {
                MaCtdh = ctdh.MaCtdh,
                MaDh = ctdh.MaDh,
                MaTrangThai = ctdh.MaTrangThai,
                MaHh = ctdh.MaHh,
                MaShipper = ctdh.MaShipper,
                NgayCapNhat = ctdh.NgayCapNhat,
                NgayNhanDh = ctdh.NgayNhanDh,
                DiaChi = ctdh.DiaChi,
                GhiChu = "",
                Hinh = splitHinh(hh.Hinh)[0],
                TenHang = hh.TenHh,
                NgayDatHang = dh.NgayLapDh,
                Dongia = hh.DonGia ?? 0,
                NguoiNhan = kh.HoTen,
                NguoiGiao = sh.TenShipper,
            };
            return View(result);
        }

        #endregion

        #region NhanDonHang
        public IActionResult NhanDonHang(int id)
        {
            
            var hangHoa = db.HangHoas.AsQueryable();
            var donHang = db.DonHangs.AsQueryable();
            var maShipper = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "ShipperID").Value;

            var ctdhs = db.ChiTietDhs.AsQueryable();
            var chiTietDonHang = ctdhs.SingleOrDefault(dh => dh.MaDh == id);
            if (chiTietDonHang != null)
            {
                DBConnection dbc = new DBConnection();
                SqlConnection conn = new SqlConnection(dbc.ConnectionString);
                string UPDATE = "UPDATE ChiTietDH SET MaShipper = N'" + maShipper + "', MaTrangThai = 4, NgayCapNhat = GETDATE(), NgayNhanDH = GETDATE() WHERE MaDH = " + id + "";
                string UPDATESHIPPER = "UPDATE Shipper SET SoDonGiao = SoDonGiao + 1 WHERE MaShipper = N'" + maShipper + "'";
                SqlCommand comd = new SqlCommand(UPDATE, conn);
                SqlCommand comdS = new SqlCommand(UPDATESHIPPER, conn);
                conn.Open();
                comd.ExecuteNonQuery();
                comdS.ExecuteNonQuery();
                db.SaveChanges();
                conn.Close();
            }
            else {
                DBConnection dbc = new DBConnection();
                SqlConnection conn = new SqlConnection(dbc.ConnectionString);
                string UPDATE = "UPDATE DonHang SET MaTrangThai = 4 WHERE MaDH = " + id + "";
                string UPDATESHIPPER = "UPDATE Shipper SET SoDonGiao = SoDonGiao + 1 WHERE MaShipper = N'" + maShipper + "'";
                SqlCommand comd = new SqlCommand(UPDATE, conn);
                SqlCommand comdS = new SqlCommand(UPDATESHIPPER, conn);
                conn.Open();
                comd.ExecuteNonQuery();
                comdS.ExecuteNonQuery();
                db.SaveChanges();
                conn.Close();


                var dh = donHang
                    .Where(p => p.MaDh == id)
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
                        DiaChiGiaoHang = p.DiaChiGiaoHang,
                        NgayLapDh = p.NgayLapDh,
                        GhiChu = p.GhiChu ?? ""
                    });

                var data = dh.SingleOrDefault(p => p.MaDh == id);

                var ctdh = new ChiTietDh
                {
                    MaDh = data.MaDh,
                    MaTrangThai = 4,
                    MaHh = data.HangHoa,
                    MaShipper = maShipper,
                    NgayCapNhat = DateTime.Now,
                    NgayNhanDh = DateTime.Now,
                    DiaChi = data.DiaChiGiaoHang,
                    GhiChu = ""
                };
                db.Add(ctdh);
                db.SaveChanges();

            }

            return RedirectToAction("DonHang", "Shipper");
        }
        #endregion

        #region GiaoHang
        public IActionResult GiaoHang(int id)
        {

            var hangHoa = db.HangHoas.AsQueryable();
            var donHang = db.DonHangs.AsQueryable();
            var maShipper = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "ShipperID").Value;

            var ctdhs = db.ChiTietDhs.AsQueryable();
            var chiTietDonHang = ctdhs.SingleOrDefault(dh => dh.MaDh == id);
            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string UPDATE = "UPDATE DonHang SET MaTrangThai = 5 WHERE MaDH = " + id + "";
            string UPDATECT = "UPDATE ChiTietDH SET MaTrangThai = 5 WHERE MaDH = " + id + "";
            SqlCommand comd = new SqlCommand(UPDATE, conn);
            SqlCommand comdCT = new SqlCommand(UPDATECT, conn);
            conn.Open();
            comd.ExecuteNonQuery();
            comdCT.ExecuteNonQuery();
            db.SaveChanges();
            conn.Close();

            return RedirectToAction("DonHang", "Shipper");
        }
        #endregion

        #region HuyGiao
        public IActionResult HuyGiao(int id)
        {

            var hangHoa = db.HangHoas.AsQueryable();
            var donHang = db.DonHangs.AsQueryable();
            var maShipper = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "ShipperID").Value;

            var ctdhs = db.ChiTietDhs.AsQueryable();
            var chiTietDonHang = ctdhs.SingleOrDefault(dh => dh.MaDh == id);
            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string UPDATE = "UPDATE DonHang SET MaTrangThai = 1 WHERE MaDH = " + id + "";
            string UPDATECT = "UPDATE ChiTietDH SET MaTrangThai = 1 WHERE MaDH = " + id + "";
            SqlCommand comd = new SqlCommand(UPDATE, conn);
            SqlCommand comdCT = new SqlCommand(UPDATECT, conn);
            conn.Open();
            comd.ExecuteNonQuery();
            comdCT.ExecuteNonQuery();
            db.SaveChanges();
            conn.Close();

            return RedirectToAction("DonHang", "Shipper");
        }
        #endregion

        #region HuyHang
        public IActionResult HuyHang(int id)
        {

            var hangHoa = db.HangHoas.AsQueryable();
            var donHang = db.DonHangs.AsQueryable();
            var maShipper = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "ShipperID").Value;

            var ctdhs = db.ChiTietDhs.AsQueryable();
            var chiTietDonHang = ctdhs.SingleOrDefault(dh => dh.MaDh == id);
            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string UPDATE = "UPDATE DonHang SET MaTrangThai = 6 WHERE MaDH = " + id + "";
            string UPDATECT = "UPDATE ChiTietDH SET MaTrangThai = 6 WHERE MaDH = " + id + "";
            SqlCommand comd = new SqlCommand(UPDATE, conn);
            SqlCommand comdCT = new SqlCommand(UPDATECT, conn);
            conn.Open();
            comd.ExecuteNonQuery();
            comdCT.ExecuteNonQuery();
            db.SaveChanges();
            conn.Close();

            return RedirectToAction("DonHang", "Shipper");
        }
        #endregion

        #region TraHang
        public IActionResult TraHang(int id)
        {

            var hangHoa = db.HangHoas.AsQueryable();
            var donHang = db.DonHangs.AsQueryable();
            var maShipper = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "ShipperID").Value;

            var ctdhs = db.ChiTietDhs.AsQueryable();
            var chiTietDonHang = ctdhs.SingleOrDefault(dh => dh.MaDh == id);
            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string UPDATE = "UPDATE DonHang SET MaTrangThai = 7 WHERE MaDH = " + id + "";
            string UPDATECT = "UPDATE ChiTietDH SET MaTrangThai = 7 WHERE MaDH = " + id + "";
            SqlCommand comd = new SqlCommand(UPDATE, conn);
            SqlCommand comdCT = new SqlCommand(UPDATECT, conn);
            conn.Open();
            comd.ExecuteNonQuery();
            comdCT.ExecuteNonQuery();
            db.SaveChanges();
            conn.Close();

            return RedirectToAction("DonHang", "Shipper");
        }
        #endregion

        #region DonDaNhan
        public IActionResult DonDaNhan()
        {
            var maShipper = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "ShipperID").Value;
            var hangHoa = db.HangHoas.AsQueryable();
            var chiTietDHs = db.ChiTietDhs.AsQueryable();
            var ctdhs = chiTietDHs
                .Where(p => p.MaTrangThai == 4 && p.MaShipper == maShipper)
                .Select(p => new ChiTietDHVM
                {
                    MaCtdh = p.MaCtdh,
                    MaDh = p.MaDh,
                    MaTrangThai = p.MaTrangThai,
                    MaHh = p.MaHh,
                    MaShipper = p.MaShipper,
                    NgayCapNhat = p.NgayCapNhat,
                    NgayNhanDh = p.NgayNhanDh,
                    DiaChi = p.DiaChi,
                    GhiChu = "",
                    Hinh = splitHinh(p.MaHhNavigation.Hinh)[0],
                    TenHang = p.MaHhNavigation.TenHh,
                    TrangThai = p.MaTrangThaiNavigation.TenTrangThai
                });
            return View(ctdhs);
        }

        #endregion

        #region DonDaGiao
        public IActionResult DonDaGiao()
        {
            var maShipper = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "ShipperID").Value;
            var hangHoa = db.HangHoas.AsQueryable();
            var chiTietDHs = db.ChiTietDhs.AsQueryable();
            var ctdhs = chiTietDHs
                .Where(p => p.MaTrangThai == 5 && p.MaShipper == maShipper)
                .Select(p => new ChiTietDHVM
                {
                    MaCtdh = p.MaCtdh,
                    MaDh = p.MaDh,
                    MaTrangThai = p.MaTrangThai,
                    MaHh = p.MaHh,
                    MaShipper = p.MaShipper,
                    NgayCapNhat = p.NgayCapNhat,
                    NgayNhanDh = p.NgayNhanDh,
                    DiaChi = p.DiaChi,
                    GhiChu = "",
                    Hinh = splitHinh(p.MaHhNavigation.Hinh)[0],
                    TenHang = p.MaHhNavigation.TenHh,
                    TrangThai = p.MaTrangThaiNavigation.TenTrangThai
                });
            return View(ctdhs);
        }

        #endregion

        #region Register
        [HttpGet]
        public IActionResult DangKyShipper()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKyShipper(ShipperVM model, IFormFile Hinh)
        {

            try
            {
                var gioiTinh = "";
                if (model.GioiTinhBool == true)
                {
                    gioiTinh = "Nam";
                }
                else {gioiTinh = "Nữ"; }
                if (Hinh != null)
                {
                    MyUtil.UploadHinh(Hinh, "NhanVien");
                    model.Email = model.Email + "!" + Hinh.FileName;
                }
                var shipper = new Shipper
                {
                    MaShipper = model.MaShipper,
                    MatKhau = model.MatKhau.ToMd5Hash(model.MaShipper),
                    TenShipper = model.TenShipper,
                    GioiTinh = gioiTinh,
                    Email = model.Email,
                    DienThoai = model.DienThoai,
                    HieuLuc = 1,
                    SoDonGiao = 0,
                    DiemDanhGia = 0
                };

                db.Add(shipper);
                db.SaveChanges();
                return RedirectToAction("DangNhap", "Shipper");
            }
            catch (Exception ex)
            {
                var mess = $"{ex.Message}";
            }

            return View();
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var shipper = db.Shippers.SingleOrDefault(sh => sh.MaShipper == model.UserName);

                if (shipper == null)
                {
                    ModelState.AddModelError("error", "Please check your password and username and try again.");
                }
                else
                {
                    if (shipper.HieuLuc == 0)
                    {
                        ModelState.AddModelError("error", "This user was locked");
                    }
                    else
                    {
                        if (shipper.MatKhau != model.Password.ToMd5Hash(shipper.MaShipper))
                        {
                            ModelState.AddModelError("error", "Please check your password and username and try again.");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, shipper.Email),
                                new Claim(ClaimTypes.Name, shipper.TenShipper),
                                new Claim("Username", shipper.MaShipper),
                                new Claim("Email", shipper.Email),
                                new Claim(MySetting.CLAIM_SHIPPERID, shipper.MaShipper),

                                // claim - dynamic role
                                new Claim(ClaimTypes.Role, "Shipper")
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            await HttpContext.SignInAsync(claimsPrincipal);

                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return Redirect("/Shipper/DonHang");
                            }

                        }
                    }
                }
            }
            return View();
        }
        #endregion

        #region DangXuat
        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }
        #endregion

    }
}
