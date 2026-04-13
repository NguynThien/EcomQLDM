using System;
using System.Configuration;
using EcomQLDM.Data;
using EcomQLDM.Helpers;
using EcomQLDM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcomQLDM.Controllers
{
    public class CartController : Controller
    {
        private readonly TmdtdatabaseContext db;


        public CartController(TmdtdatabaseContext context)
        {
            db = context;
        }


        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

        public IActionResult Index()
        {

            return View(Cart);
        }
        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"No result for product {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    MaHh = hangHoa.MaHh,
                    TenHH = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0,
                    Hinh = splitHinh(hangHoa.Hinh)[0] ?? string.Empty,
                    SoLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"No result for product {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    MaHh = hangHoa.MaHh,
                    TenHH = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0,
                    Hinh = splitHinh(hangHoa.Hinh)[0] ?? string.Empty,
                    SoLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong -= quantity;
            }

            if (item.SoLuong == 0)
            {
                gioHang.Remove(item);
            }

            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            var id = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "CustomerID").Value;
            var data = db.KhachHangs.SingleOrDefault(p => p.MaKh == id);
            if (Cart.Count == 0)
            {
                return Redirect("/");
            }
            ViewBag.HoTen = data.HoTen;
            ViewBag.DiaChi = data.DiaChi;
            ViewBag.DienThoai = data.DienThoai;
            return View(Cart);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID).Value;

                var khachHang = new KhachHang();
                if (model.GiongKhachHang)
                {
                    khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == customerId);
                }

                var hoaDons = db.HoaDons.AsQueryable();
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

                //var firstId = result.First().MaHD;
                var lastId = result.OrderBy(p => p.MaHD).Last().MaHD;

                var hoadon = new HoaDon
                {
                    MaKh = customerId,
                    HoTen = model.HoTen ?? khachHang.HoTen,
                    DiaChi = model.DiaChi ?? khachHang.DiaChi,
                    //DienThoai = model.DienThoai ?? khachHang.DienThoai,
                    NgayDat = DateTime.Now,
                    CachThanhToan = "COD",
                    CachVanChuyen = "POST",
                    MaTrangThai = 0,
                    GhiChu = model.GhiChu

                };
                //DBConnection dbc = new DBConnection();
                //SqlConnection conn = new SqlConnection(dbc.ConnectionString);
                //string IDENTITY_INSERT = "INSERT INTO HoaDon (MaKH, NgayDat, NgayCan, DiaChi, CachThanhToan, CachVanChuyen, PhiVanChuyen, MaTrangThai) VALUES (N'" + customerId + "', GETDATE(), DATEADD(DAY, 2, GETDATE()), N'" + model.DiaChi + "', N'COD', N'SHIPPER', 0, 0)";
                //SqlCommand comd = new SqlCommand(IDENTITY_INSERT, conn);
                //conn.Open();
                //comd.ExecuteNonQuery();
                //db.SaveChanges();
                //conn.Close();

                var insertHD = new HoaDon
                {
                    MaKh = customerId,
                    NgayDat = DateTime.Now,
                    NgayCan = DateTime.Now,
                    DiaChi = model.DiaChi,
                    CachThanhToan = "COD",
                    CachVanChuyen = "SHIPPER",
                    PhiVanChuyen = 0,
                    MaTrangThai = 0
                };

                db.Add(insertHD);
                db.SaveChanges();

                var cthds = new List<ChiTietHd>();
                foreach (var item in Cart)
                {
                    cthds.Add(new ChiTietHd
                    {
                        MaHd = lastId +1,
                        SoLuong = item.SoLuong,
                        DonGia = item.DonGia,
                        MaHh = item.MaHh,
                        GiamGia = 0

                    });
                }

                var donHangs = db.DonHangs.AsQueryable();
                var listDH = donHangs
                .Select(p => new DonHang
                {
                    MaDh = p.MaDh,
                    HangHoa = p.HangHoa,
                    MaKh = p.MaKh,
                    MaTrangThai = p.MaTrangThai,
                    DiaChiLayHang = p.DiaChiLayHang,
                    DiaChiGiaoHang = p.DiaChiGiaoHang,
                    NgayLapDh = p.NgayLapDh,
                    GhiChu = p.GhiChu
                });
                var lastMaDH = 0;
                if (listDH.Count() != 0) {
                    lastMaDH = listDH.OrderBy(p => p.MaDh).Last().MaDh;
                }
                var kho = db.Khos.AsQueryable();
                var ctpxs = db.ChiTietPxes.AsQueryable();
                var donHang = new List<DonHang>();

                foreach (var item in cthds)
                {
                    for (int i = 0; i < item.SoLuong; i++)
                    {
                        donHang.Add(new DonHang
                        {
                            HangHoa = item.MaHh,
                            MaKh = customerId,
                            MaTrangThai = 1,
                            DiaChiLayHang = "Chờ xử lý",
                            DiaChiGiaoHang = model.DiaChi ?? khachHang.DiaChi,
                            NgayLapDh = DateTime.Now,
                            GhiChu = "Chờ xử lý"
                        });

                        var maKho = "BMdepot";
                        var px = new PhieuXuat
                        {
                            MaKho = maKho,
                            NgayLapPhieu = DateOnly.FromDateTime(DateTime.Now),
                            NgayXuatHang = DateOnly.FromDateTime(DateTime.Now)
                        };

                        db.Add(px);
                        db.SaveChanges();
                        var phieuXuat = db.PhieuXuats.OrderBy(px => px.MaPx).Last();
                        var chitietPX = new ChiTietPx
                        {
                            MaPx = phieuXuat.MaPx,
                            MaHh = item.MaHh,
                            SoLuong = 1
                        };
                        db.Add(chitietPX);
                        db.SaveChanges();
                    }
                }

                db.AddRange(cthds);
                db.AddRange(donHang);
                db.SaveChanges();
                db.Database.BeginTransaction();
                HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());
                return RedirectToAction("MuaHangTC", "Cart");

            }
            return View(Cart);
        }


        public IActionResult MuaHangTC()
        {
            return View();
        }

    }
}
