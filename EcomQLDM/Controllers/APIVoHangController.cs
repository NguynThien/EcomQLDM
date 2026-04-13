using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcomQLDM.Data;
using EcomQLDM.Helpers;
using EcomQLDM.ViewModels;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcomQLDM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIVoHangController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public APIVoHangController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: api/APIVoHang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoHang>>> GetVoHangs()
        {
            return await _context.VoHangs.ToListAsync();
        }

        // GET: api/APIVoHang/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoHang>> GetVoHang(int id)
        {
            var voHang = await _context.VoHangs.FindAsync(id);

            if (voHang == null)
            {
                return NotFound();
            }

            return voHang;
        }

        // PUT: api/APIVoHang/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoHang(int id, VoHang voHang)
        {
            if (id != voHang.MaVh)
            {
                return BadRequest();
            }

            _context.Entry(voHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoHangExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/APIVoHang
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VoHang>> PostVoHang(VoHang voHang)
        {
            _context.VoHangs.Add(voHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoHang", new { id = voHang.MaVh }, voHang);
        }

        // DELETE: api/APIVoHang/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoHang(int id)
        {
            var voHang = await _context.VoHangs.FindAsync(id);
            if (voHang == null)
            {
                return NotFound();
            }

            _context.VoHangs.Remove(voHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoHangExists(int id)
        {
            return _context.VoHangs.Any(e => e.MaVh == id);
        }
    }

    #region ChiTietVoHang
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietVoHangController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }
        public static string[] splitMoTa(string mota)
        {
            string[] motaSubs = mota.Split('!');
            return motaSubs;
        }

        public ChiTietVoHangController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/ChiTietVoHang?makh=thien600
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoHangVM>>> GetVoHangs(string makh)
        {
            var voHang = _context.VoHangs.AsQueryable();
            var ctvh = new List<VoHangVM>();
            var results = voHang
                .Where(p => p.MaKh == makh && p.HieuLuc == 1 && p.SoLuong > 0)
                .Select(p => new VoHangVM
                {
                    MaVH = p.MaVh,
                    MaKH = p.MaKh,
                    MaHH = p.MaHh,
                    SoLuong = p.SoLuong,
                    HieuLuc = p.HieuLuc,
                    TenHH = p.MaHhNavigation.TenHh ?? "",
                    MaNCC = p.MaHhNavigation.MaNcc ?? "",
                    DonGia = p.MaHhNavigation.DonGia ?? 0,
                    Hinh = splitHinh(p.MaHhNavigation.Hinh ?? "")[0],
                    TenLoai = p.MaHhNavigation.MaLoaiNavigation.TenLoai,
                    MoTa = p.MaHhNavigation.MoTa ?? "",
                    MoTaNgan1 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[0],
                    MoTaNgan2 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[1],
                    MoTaNgan3 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[2],
                    MoTaNgan4 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[3]
                }).ToList();
            ctvh.AddRange(results);
            return ctvh;
        }
    }
    #endregion

    #region ThanhTienVoHang
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhTienVoHangController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }
        public static string[] splitMoTa(string mota)
        {
            string[] motaSubs = mota.Split('!');
            return motaSubs;
        }

        public ThanhTienVoHangController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/ThanhTienVoHang?makh=thien600
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThanhTienVM>>> GetVoHangs(string makh)
        {
            var voHang = _context.VoHangs.AsQueryable();
            var ctvh = new List<VoHangVM>();
            var results = voHang
                .Where(p => p.MaKh == makh && p.HieuLuc == 1 && p.SoLuong > 0)
                .Select(p => new VoHangVM
                {
                    MaVH = p.MaVh,
                    MaKH = p.MaKh,
                    MaHH = p.MaHh,
                    SoLuong = p.SoLuong,
                    HieuLuc = p.HieuLuc,
                    TenHH = p.MaHhNavigation.TenHh ?? "",
                    MaNCC = p.MaHhNavigation.MaNcc ?? "",
                    DonGia = p.MaHhNavigation.DonGia ?? 0,
                    Hinh = splitHinh(p.MaHhNavigation.Hinh ?? "")[0],
                    TenLoai = p.MaHhNavigation.MaLoaiNavigation.TenLoai,
                    MoTa = p.MaHhNavigation.MoTa ?? "",
                    MoTaNgan1 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[0],
                    MoTaNgan2 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[1],
                    MoTaNgan3 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[2],
                    MoTaNgan4 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[3]
                }).ToList();
            ctvh.AddRange(results);
            var thanhTien = new ThanhTienVM {
                MaKH = makh,
                SoLuong = 0,
                ThanhTien = 0
            };
            foreach (var item in ctvh)
            {
                thanhTien.SoLuong += item.SoLuong;
                thanhTien.ThanhTien += item.SoLuong * item.DonGia;
            }
            var dsThanhTien = new List<ThanhTienVM>();
            dsThanhTien.Add(thanhTien);

            return dsThanhTien;
        }
    }
    #endregion

    #region MuaHang
    [Route("api/[controller]")]
    [ApiController]
    public class MuaHangController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }
        public static string[] splitMoTa(string mota)
        {
            string[] motaSubs = mota.Split('!');
            return motaSubs;
        }

        public MuaHangController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/MuaHang?makh=thien600
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThanhTienVM>>> GetVoHangs(string makh)
        {
            var voHang = _context.VoHangs.AsQueryable();
            var ctvh = new List<VoHangVM>();
            var results = voHang
                .Where(p => p.MaKh == makh && p.HieuLuc == 1)
                .Select(p => new VoHangVM
                {
                    MaVH = p.MaVh,
                    MaKH = p.MaKh,
                    MaHH = p.MaHh,
                    SoLuong = p.SoLuong,
                    HieuLuc = p.HieuLuc,
                    TenHH = p.MaHhNavigation.TenHh ?? "",
                    MaNCC = p.MaHhNavigation.MaNcc ?? "",
                    DonGia = p.MaHhNavigation.DonGia ?? 0,
                    Hinh = splitHinh(p.MaHhNavigation.Hinh ?? "")[0],
                    TenLoai = p.MaHhNavigation.MaLoaiNavigation.TenLoai,
                    MoTa = p.MaHhNavigation.MoTa ?? "",
                    MoTaNgan1 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[0],
                    MoTaNgan2 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[1],
                    MoTaNgan3 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[2],
                    MoTaNgan4 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[3]
                }).ToList();
            ctvh.AddRange(results);
            var thanhTien = new ThanhTienVM
            {
                MaKH = makh,
                SoLuong = 0,
                ThanhTien = 0
            };
            foreach (var item in ctvh)
            {
                thanhTien.SoLuong += item.SoLuong;
                thanhTien.ThanhTien += item.SoLuong * item.DonGia;
            }
            var dsThanhTien = new List<ThanhTienVM>();
            dsThanhTien.Add(thanhTien);

            var donHangs = _context.DonHangs.AsQueryable();
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
            if (listDH.Count() != 0)
            {
                lastMaDH = listDH.OrderBy(p => p.MaDh).Last().MaDh;
            }
            var kho = _context.Khos.AsQueryable();
            var ctpxs = _context.ChiTietPxes.AsQueryable();
            var donHang = new List<DonHang>();

            var ttKH = _context.KhachHangs.SingleOrDefault(p => p.MaKh == makh);

            foreach (var item in ctvh)
            {
                for (int i = 0; i < item.SoLuong; i++)
                {
                    donHang.Add(new DonHang
                    {
                        HangHoa = item.MaHH,
                        MaKh = makh,
                        MaTrangThai = 1,
                        DiaChiLayHang = "Chờ xử lý",
                        DiaChiGiaoHang = ttKH.DiaChi ?? "",
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

                    _context.Add(px);
                    _context.SaveChanges();
                    var phieuXuat = _context.PhieuXuats.OrderBy(px => px.MaPx).Last();
                    var chitietPX = new ChiTietPx
                    {
                        MaPx = phieuXuat.MaPx,
                        MaHh = item.MaHH,
                        SoLuong = 1
                    };
                    _context.Add(chitietPX);
                    _context.SaveChanges();
                }
            }
            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string UPDATE = "UPDATE VoHang SET HieuLuc = 0 WHERE MaKH = N'" + makh + "'";
            SqlCommand comd = new SqlCommand(UPDATE, conn);
            conn.Open();
            comd.ExecuteNonQuery();
            _context.SaveChanges();
            conn.Close();

            _context.AddRange(donHang);
            _context.SaveChanges();

            return dsThanhTien;
        }
    }
    #endregion

    #region ThemVoHang
    [Route("api/[controller]")]
    [ApiController]
    public class ThemVoHangController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }
        public static string[] splitMoTa(string mota)
        {
            string[] motaSubs = mota.Split('!');
            return motaSubs;
        }

        public ThemVoHangController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/ThemVoHang?makh=thien600&mahh=2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThanhTienVM>>> GetVoHangs(string makh, int mahh)
        {
            var voHang = _context.VoHangs.AsQueryable();
            var ctvh = new List<VoHangVM>();
            var results = voHang
                .Where(p => p.MaKh == makh && p.HieuLuc == 1 && p.SoLuong > 0)
                .Select(p => new VoHangVM
                {
                    MaVH = p.MaVh,
                    MaKH = p.MaKh,
                    MaHH = p.MaHh,
                    SoLuong = p.SoLuong,
                    HieuLuc = p.HieuLuc,
                    TenHH = p.MaHhNavigation.TenHh ?? "",
                    MaNCC = p.MaHhNavigation.MaNcc ?? "",
                    DonGia = p.MaHhNavigation.DonGia ?? 0,
                    Hinh = splitHinh(p.MaHhNavigation.Hinh ?? "")[0],
                    TenLoai = p.MaHhNavigation.MaLoaiNavigation.TenLoai,
                    MoTa = p.MaHhNavigation.MoTa ?? "",
                    MoTaNgan1 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[0],
                    MoTaNgan2 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[1],
                    MoTaNgan3 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[2],
                    MoTaNgan4 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[3]
                }).ToList();
            ctvh.AddRange(results);
            var thanhTien = new ThanhTienVM
            {
                MaKH = makh,
                SoLuong = 0,
                ThanhTien = 0
            };
            foreach (var item in ctvh)
            {
                thanhTien.SoLuong += item.SoLuong;
                thanhTien.ThanhTien += item.SoLuong * item.DonGia;
            }
            var dsThanhTien = new List<ThanhTienVM>();
            dsThanhTien.Add(thanhTien);

            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string IDENTITY_INSERT = "INSERT INTO VoHang (MaKH, MaHH, SoLuong, HieuLuc) VALUES (N'" + makh + "', " + mahh + ", 1, 1)";
            SqlCommand comd = new SqlCommand(IDENTITY_INSERT, conn);

            conn.Open();
            comd.ExecuteNonQuery();

            _context.SaveChanges();
            conn.Close();

            return dsThanhTien;
        }
    }
    #endregion

    #region TangSoLuong
    [Route("api/[controller]")]
    [ApiController]
    public class TangSoLuongController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }
        public static string[] splitMoTa(string mota)
        {
            string[] motaSubs = mota.Split('!');
            return motaSubs;
        }

        public TangSoLuongController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/TangSoLuong?mavh=3
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoHangVM>>> GetVoHangs(int mavh)
        {
            
            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string IDENTITY_INSERT = "UPDATE VoHang SET SoLuong = SoLuong + 1 WHERE MaVH = " + mavh + "";
            SqlCommand comd = new SqlCommand(IDENTITY_INSERT, conn);

            conn.Open();
            comd.ExecuteNonQuery();

            _context.SaveChanges();
            conn.Close();
            var voHang = _context.VoHangs.AsQueryable();
            var ctvh = new List<VoHangVM>();
            var results = voHang
                .Where(p => p.MaKh == "" && p.HieuLuc == 1 && p.SoLuong > 0)
                .Select(p => new VoHangVM
                {
                    MaVH = p.MaVh,
                    MaKH = p.MaKh,
                    MaHH = p.MaHh,
                    SoLuong = p.SoLuong,
                    HieuLuc = p.HieuLuc,
                    TenHH = p.MaHhNavigation.TenHh ?? "",
                    MaNCC = p.MaHhNavigation.MaNcc ?? "",
                    DonGia = p.MaHhNavigation.DonGia ?? 0,
                    Hinh = splitHinh(p.MaHhNavigation.Hinh ?? "")[0],
                    TenLoai = p.MaHhNavigation.MaLoaiNavigation.TenLoai,
                    MoTa = p.MaHhNavigation.MoTa ?? "",
                    MoTaNgan1 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[0],
                    MoTaNgan2 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[1],
                    MoTaNgan3 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[2],
                    MoTaNgan4 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[3]
                }).ToList();
            ctvh.AddRange(results);
            return ctvh;
        }
    }
    #endregion

    #region GiamSoLuong
    [Route("api/[controller]")]
    [ApiController]
    public class GiamSoLuongController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }
        public static string[] splitMoTa(string mota)
        {
            string[] motaSubs = mota.Split('!');
            return motaSubs;
        }

        public GiamSoLuongController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/GiamSoLuong?mavh=3
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoHangVM>>> GetVoHangs(int mavh)
        {

            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string IDENTITY_INSERT = "UPDATE VoHang SET SoLuong = SoLuong - 1 WHERE MaVH = " + mavh + "";
            SqlCommand comd = new SqlCommand(IDENTITY_INSERT, conn);

            conn.Open();
            comd.ExecuteNonQuery();

            _context.SaveChanges();
            conn.Close();
            var voHang = _context.VoHangs.AsQueryable();
            var ctvh = new List<VoHangVM>();
            var results = voHang
                .Where(p => p.MaKh == "" && p.HieuLuc == 1 && p.SoLuong > 0)
                .Select(p => new VoHangVM
                {
                    MaVH = p.MaVh,
                    MaKH = p.MaKh,
                    MaHH = p.MaHh,
                    SoLuong = p.SoLuong,
                    HieuLuc = p.HieuLuc,
                    TenHH = p.MaHhNavigation.TenHh ?? "",
                    MaNCC = p.MaHhNavigation.MaNcc ?? "",
                    DonGia = p.MaHhNavigation.DonGia ?? 0,
                    Hinh = splitHinh(p.MaHhNavigation.Hinh ?? "")[0],
                    TenLoai = p.MaHhNavigation.MaLoaiNavigation.TenLoai,
                    MoTa = p.MaHhNavigation.MoTa ?? "",
                    MoTaNgan1 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[0],
                    MoTaNgan2 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[1],
                    MoTaNgan3 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[2],
                    MoTaNgan4 = splitMoTa(p.MaHhNavigation.MoTaDonVi ?? "")[3]
                }).ToList();
            ctvh.AddRange(results);
            return ctvh;
        }
    }
    #endregion
}
