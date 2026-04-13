using AutoMapper;
using EcomQLDM.Data;
using EcomQLDM.Helpers;
using EcomQLDM.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EcomQLDM.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly TmdtdatabaseContext db;
        private readonly IMapper _mapper;

        public NhanVienController(TmdtdatabaseContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        #region Register
        [HttpGet]
        public IActionResult AddNhanVien()
        {
            ViewData["TenQh"] = new SelectList(db.QuyenHangs, "TenQh", "TenQh");
            return View();
        }

        [HttpPost]
        public IActionResult AddNhanVien(AddNhanVienVM model, IFormFile Hinh)
        {
            
            try
            {
                var gioiTinh = "";
                if (model.GioiTinh == true)
                {
                    gioiTinh = "Nam";
                }
                else { gioiTinh = "Nữ"; }
                if (Hinh != null)
                {
                    MyUtil.UploadHinh(Hinh, "NhanVien");
                    model.Email = model.Email + "!" + Hinh.FileName;
                }
                var data = db.QuyenHangs.SingleOrDefault(p => p.TenQh == model.TenQH);
                var nhanVien = new NhanVien{
                    MaNv = model.MaNV,
                    HoTen = model.HoTen + "!" + gioiTinh + "!" + model.DienThoai,
                    Email = model.Email + "!" + model.DiaChi,
                    MatKhau = model.MatKhau.ToMd5Hash(model.MaNV),
                    MaQh = data.MaQh,
                    HieuLuc = 1,
                };
                    
                db.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index", "HangHoa");
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
                var nhanVien = db.NhanViens.SingleOrDefault(nv => nv.MaNv == model.UserName);

                if (nhanVien == null)
                {
                    ModelState.AddModelError("error", "Please check your password and username and try again.");
                }
                else
                {
                    if (nhanVien.HieuLuc == 0)
                    {
                        ModelState.AddModelError("error", "This user was locked");
                    }
                    else
                    {
                        if (nhanVien.MatKhau != model.Password.ToMd5Hash(nhanVien.MaNv))
                        {
                            ModelState.AddModelError("error", "Please check your password and username and try again.");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, nhanVien.Email),
                                new Claim(ClaimTypes.Name, nhanVien.HoTen),
                                new Claim("Username", nhanVien.MaNv),
                                new Claim("Email", nhanVien.Email),
                                new Claim(MySetting.CLAIM_NHANVIENID, nhanVien.MaNv),

                                // claim - dynamic role
                                new Claim(ClaimTypes.Role, "NhanVien")
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
                                return Redirect("/NhanVien/PhanQuyen");
                            }

                        }
                    }
                }
            }
            return View();
        }
        #endregion
        public static string[] splitString(string str)
        {
            string[] strSubs = str.Split('!');
            return strSubs;
        }
        #region Profile
        [Authorize]
        public IActionResult Profile(string id)
        {
            id = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "NhanVienID").Value;
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

            //return View();
            return View(result);
        }

        #endregion

        #region PhanQuyen
        [Authorize]
        public IActionResult PhanQuyen(string id)
        {
            id = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "NhanVienID").Value;
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
                };
                phanQuyen = pq;
            }
            //return View();
            return View(phanQuyen);
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

        #region DanhSachNhanVien
        public IActionResult DanhSachNhanVien()
        {
            
            var nhanvien = db.NhanViens.AsQueryable();
            var result = nhanvien
                .Select(p => new NhanVienVM
                {
                    Hinh = splitString(p.Email)[1] ?? string.Empty,
                    MaNV = p.MaNv,
                    TenQH = p.MaQhNavigation.TenQh,
                    HoTen = splitString(p.HoTen)[0],
                    GioiTinh = splitString(p.HoTen)[1],
                    DiaChi = splitString(p.Email)[2] ?? string.Empty,
                    Email = splitString(p.Email)[0] ?? string.Empty,
                    DienThoai = splitString(p.HoTen)[2] ?? string.Empty
                });

            return View(result);
        }
        #endregion

        #region SearchHT
        public IActionResult SearchHT(string? query)
        {
            var nhanvien = db.NhanViens.AsQueryable();
            if (query != null)
            {
                nhanvien = nhanvien.Where(p => p.HoTen.Contains(query));
            }
            var result = nhanvien
                .Select(p => new NhanVienVM
                {
                    Hinh = splitString(p.Email)[1] ?? string.Empty,
                    MaNV = p.MaNv,
                    TenQH = p.MaQhNavigation.TenQh,
                    HoTen = splitString(p.HoTen)[0],
                    GioiTinh = splitString(p.HoTen)[1],
                    DiaChi = splitString(p.Email)[2] ?? string.Empty,
                    Email = splitString(p.Email)[0] ?? string.Empty,
                    DienThoai = splitString(p.HoTen)[2] ?? string.Empty
                });

            return View(result);
        }
        #endregion

        #region Detail
        public IActionResult Detail(string id)
        {
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

            return View(result);
        }
        #endregion

    }
}
