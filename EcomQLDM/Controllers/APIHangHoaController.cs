using EcomQLDM.Data;
using EcomQLDM.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcomQLDM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIHangHoaController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public APIHangHoaController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: api/APIHangHoa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangHoa>>> GetHangHoas()
        {
            return await _context.HangHoas.Take(3).ToListAsync();
        }

        // GET: api/APIHangHoa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HangHoa>> GetHangHoa(int id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);

            if (hangHoa == null)
            {
                return NotFound();
            }

            return hangHoa;
        }

        // PUT: api/APIHangHoa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHangHoa(int id, HangHoa hangHoa)
        {
            if (id != hangHoa.MaHh)
            {
                return BadRequest();
            }

            _context.Entry(hangHoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HangHoaExists(id))
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

        // POST: api/APIHangHoa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HangHoa>> PostHangHoa(HangHoa hangHoa)
        {
            _context.HangHoas.Add(hangHoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHangHoa", new { id = hangHoa.MaHh }, hangHoa);
        }

        // DELETE: api/APIHangHoa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHangHoa(int id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            _context.HangHoas.Remove(hangHoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HangHoaExists(int id)
        {
            return _context.HangHoas.Any(e => e.MaHh == id);
        }
    }

    // -- ** ** -- //

    [Route("api/[controller]")]
    [ApiController]
    public class DisplayController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public DisplayController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: api/APIHangHoa
        [HttpGet]
        public PhanLoaiHHVM GetHangHoas()
        {
            //return await _context.HangHoas.Take(1).ToListAsync();

            var hangHoas = _context.HangHoas.AsQueryable();

            var loai1 = hangHoas
                .Where(p => p.MaLoai == 1)
                .Take(4)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    MoTaNgan = p.MoTaDonVi ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai
                }).ToList();
            var loai2 = hangHoas
                .Where(p => p.MaLoai == 2)
                .Take(4)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    MoTaNgan = p.MoTaDonVi ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai
                }).ToList();

            var loai3 = hangHoas
                .Where(p => p.MaLoai == 3)
                .Take(4)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    MoTaNgan = p.MoTaDonVi ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai
                }).ToList();

            var loai4 = hangHoas
                .Where(p => p.MaLoai == 4)
                .Take(4)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    MoTaNgan = p.MoTaDonVi ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai
                }).ToList();

            var loai5 = hangHoas
                .Where(p => p.MaLoai == 5)
                .Take(4)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = p.Hinh ?? "",
                    MoTaNgan = p.MoTaDonVi ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai
                }).ToList();

            var model = new PhanLoaiHHVM
            {
                HhLoai1 = loai1,
                HhLoai2 = loai2,
                HhLoai3 = loai3,
                HhLoai4 = loai4,
                HhLoai5 = loai5,
            };

            return model;
        }


    }
    #region HomePage
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }

        public HomePageController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: api/APIHomePage https://localhost:7092/api/HomePage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangHoaVM>>> GetHangHoas()
        {

            var hangHoas = _context.HangHoas.AsQueryable();
            var ChiTietPn = _context.ChiTietPns.AsQueryable();
            var ChiTietPx = _context.ChiTietPxes.AsQueryable();

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
                .Take(8)
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

            var hh = new List<HangHoaVM>();
            //var results = hangHoas
            //    //.Where(p => p.MaLoai == 1)
            //    .Take(8)
            //    .Select(p => new HangHoaVM
            //    {
            //        MaHh = p.MaHh,
            //        TenHH = p.TenHh,
            //        DonGia = p.DonGia ?? 0,
            //        Hinh = splitHinh(p.Hinh)[0] ?? "",
            //        MoTaNgan = p.MoTaDonVi ?? "",
            //        TenLoai = p.MaLoaiNavigation.TenLoai
            //    }).ToList();
            hh.AddRange(hangs);
            return hh;
        }
    }
    #endregion

    #region UserPage
    [Route("api/[controller]")]
    [ApiController]
    public class UserPageController : ControllerBase
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

        public UserPageController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/UserPage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangHoaVM>>> GetHangHoas()
        {

            var hangHoas = _context.HangHoas.AsQueryable();
            var ChiTietPn = _context.ChiTietPns.AsQueryable();
            var ChiTietPx = _context.ChiTietPxes.AsQueryable();

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

            var hangs1 = ketQuaHang
                .Where(p => (p.SoLuongHH - p.SoLuongXuat) > 0)
                .Skip(1)
                .Take(2)
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

            var hangs2 = ketQuaHang
                .Where(p => (p.SoLuongHH - p.SoLuongXuat) > 0)
                .Skip(5)
                .Take(2)
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

            var hangs3 = ketQuaHang
                .Where(p => (p.SoLuongHH - p.SoLuongXuat) > 0)
                .Skip(8)
                .Take(2)
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

            var hangs4 = ketQuaHang
                .Where(p => (p.SoLuongHH - p.SoLuongXuat) > 0)
                .Skip(3)
                .Take(2)
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

            var hh = new List<HangHoaVM>();
            //var results = hangHoas
            //    //.Where(p => p.MaLoai == 1)
            //    .Take(8)
            //    .Select(p => new HangHoaVM
            //    {
            //        MaHh = p.MaHh,
            //        TenHH = p.TenHh,
            //        DonGia = p.DonGia ?? 0,
            //        Hinh = splitHinh(p.Hinh)[0] ?? "",
            //        MoTaNgan = p.MoTaDonVi ?? "",
            //        TenLoai = p.MaLoaiNavigation.TenLoai
            //    }).ToList();
            hh.AddRange(hangs1);
            hh.AddRange(hangs2);
            hh.AddRange(hangs3);
            hh.AddRange(hangs4);
            return hh;
        }
    }
    #endregion

    #region TopHH
    [Route("api/[controller]")]
    [ApiController]
    public class TopHHController : ControllerBase
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

        public TopHHController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/TopHH
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangHoaVM>>> GetHangHoas()
        {

            var hangHoas = _context.HangHoas.AsQueryable();
            var ChiTietPn = _context.ChiTietPns.AsQueryable();
            var ChiTietPx = _context.ChiTietPxes.AsQueryable();

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

            var hangs1 = ketQuaHang
                .Where(p => (p.SoLuongHH - p.SoLuongXuat) > 0)
                .Skip(1)
                .Take(2)
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

            var hangs2 = ketQuaHang
                .Where(p => (p.SoLuongHH - p.SoLuongXuat) > 0)
                .Skip(5)
                .Take(2)
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

            var hangs3 = ketQuaHang
                .Where(p => (p.SoLuongHH - p.SoLuongXuat) > 0)
                .Skip(8)
                .Take(2)
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

            var hangs4 = ketQuaHang
                .Where(p => (p.SoLuongHH - p.SoLuongXuat) > 0)
                .Skip(3)
                .Take(2)
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

            var hh = new List<HangHoaVM>();
            //var results = hangHoas
            //    //.Where(p => p.MaLoai == 1)
            //    .Take(8)
            //    .Select(p => new HangHoaVM
            //    {
            //        MaHh = p.MaHh,
            //        TenHH = p.TenHh,
            //        DonGia = p.DonGia ?? 0,
            //        Hinh = splitHinh(p.Hinh)[0] ?? "",
            //        MoTaNgan = p.MoTaDonVi ?? "",
            //        TenLoai = p.MaLoaiNavigation.TenLoai
            //    }).ToList();
            //hh.AddRange(hangs1);
            //hh.AddRange(hangs2);
            //hh.AddRange(hangs3);
            //hh.AddRange(hangs4);


            var ketQuaHangMoi = hangHoas.Select(p => new TonKhoVM
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

            var hangs = ketQuaHangMoi
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

            hh.AddRange(hangs);
            return hh;
        }
    }
    #endregion

    #region TimLoai
    [Route("api/[controller]")]
    [ApiController]
    public class TimLoaiController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }

        public TimLoaiController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/TimLoai?maLoai=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangHoaVM>>> GetHangHoas(int maLoai)
        {
            //return await _context.HangHoas.Take(1).ToListAsync();

            var hangHoas = _context.HangHoas.AsQueryable();
            var hh = new List<HangHoaVM>();
            var results = hangHoas
                .Where(p => p.MaLoai == maLoai)
                .Take(8)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = splitHinh(p.Hinh)[0] ?? "",
                    MoTaNgan = p.MoTaDonVi ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai
                }).ToList();
            hh.AddRange(results);
            return hh;
        }
    }
    #endregion

    #region TimLoai
    [Route("api/[controller]")]
    [ApiController]
    public class BestController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }

        public BestController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/Best?maHH=2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietPxVM>>> GetHangHoas(int maHH)
        {
            //return await _context.HangHoas.Take(1).ToListAsync();

            var hangHoas = _context.ChiTietPxes.AsQueryable();
            var hh = new List<ChiTietPxVM>();
            var results = hangHoas
                .Where(p => p.MaHh == maHH)
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
                }).ToList();
            hh.AddRange(grouped);
            return hh;
        }
    }
    #endregion

    #region TimTenHH
    [Route("api/[controller]")]
    [ApiController]
    public class TimTenHHController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }

        public TimTenHHController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/TimTenHH?search=tivi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangHoaVM>>> GetHangHoas(string? search)
        {
            //return await _context.HangHoas.Take(1).ToListAsync();

            var hangHoas = _context.HangHoas.AsQueryable();
            var hh = new List<HangHoaVM>();
            var results = hangHoas
                .Where(p => p.TenHh.Contains(search))
                .Take(8)
                .Select(p => new HangHoaVM
                {
                    MaHh = p.MaHh,
                    TenHH = p.TenHh,
                    DonGia = p.DonGia ?? 0,
                    Hinh = splitHinh(p.Hinh)[0] ?? "",
                    MoTaNgan = p.MoTaDonVi ?? "",
                    TenLoai = p.MaLoaiNavigation.TenLoai
                }).ToList();
            hh.AddRange(results);
            return hh;
        }
    }
    #endregion

    #region ChiTietHang
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietHangController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }

        public ChiTietHangController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/ChiTietHang?maHH=2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietHangHoaVM>>> GetHangHoas(int maHH)
        {
            //return await _context.HangHoas.Take(1).ToListAsync();

            var hangHoas = _context.HangHoas.AsQueryable();
            var data = _context.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == maHH);
            string[] subs = data.MoTaDonVi.Split('!');
            string[] hinhSubs = data.Hinh.Split('!');
            var ChiTietPn = _context.ChiTietPns.AsQueryable();
            var ChiTietPx = _context.ChiTietPxes.AsQueryable();

            var SoLuongHH = ChiTietPn.Where(g => g.MaHh == maHH).Select(g => new SoluongHH
            {
                SoLuongNhap = g.SoLuong,
            }).Sum(g => g.SoLuongNhap);
            var SoLuongXuat = ChiTietPx.Where(g => g.MaHh == maHH).Select(g => new SoluongHH
            {
                SoLuongXuat = g.SoLuong,
            }).Sum(g => g.SoLuongXuat);
            var hh = new List<ChiTietHangHoaVM>();
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
            hh.Add(result);
            return hh;
        }
    }
    #endregion

    #region Test
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }

        public TestController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: api/APIHomePage https://localhost:7092/api/Test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietDHVM>>> GetHangHoas()
        {
            var hangHoas = _context.HangHoas.AsQueryable();
            var doanhThuTheoNgay = _context.ChiTietDhs
                .Where(ctdh => ctdh.MaTrangThai == 5)
                .GroupBy(ctdh => ctdh.NgayNhanDh.Date)
                .Select(g => new ChiTietDHVM
                {
                    NgayNhanDh = g.Key,
                    Dongia = g.Sum(g => g.MaHhNavigation.DonGia) ?? 0
                })
                .OrderBy(x => x.NgayNhanDh)
                .ToList();

            var hh = new List<ChiTietDHVM>();
            
            hh.AddRange(doanhThuTheoNgay);
            return hh;
        }
    }
    #endregion

}
