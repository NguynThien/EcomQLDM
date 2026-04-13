using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcomQLDM.Data;
using EcomQLDM.ViewModels;
using EcomQLDM.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcomQLDM.Controllers
{
    #region APIKhachHang
    [Route("api/[controller]")]
    [ApiController]
    public class APIKhachHangController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public APIKhachHangController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: api/APIKhachHang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetKhachHangs()
        {
            return await _context.KhachHangs.ToListAsync();
        }

        // GET: api/APIKhachHang/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHang>> GetKhachHang(string id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return khachHang;
        }

        // PUT: api/APIKhachHang/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(string id, KhachHang khachHang)
        {
            if (id != khachHang.MaKh)
            {
                return BadRequest();
            }

            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
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

        // POST: api/APIKhachHang
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhachHang>> PostKhachHang(KhachHang khachHang)
        {
            _context.KhachHangs.Add(khachHang);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KhachHangExists(khachHang.MaKh))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKhachHang", new { id = khachHang.MaKh }, khachHang);
        }

        // DELETE: api/APIKhachHang/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang(string id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachHangExists(string id)
        {
            return _context.KhachHangs.Any(e => e.MaKh == id);
        }
    }
    #endregion

    #region LoginKH
    [Route("api/[controller]")]
    [ApiController]
    public class LoginKHController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }

        public LoginKHController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/LoginKH?makh=thien600&matkhau=123456789
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHangVM>>> GetKhachHangs(string makh, string matkhau)
        {
            var emkh = new List<KhachHangVM>();
            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.MaKh == makh);
            if (khachHang == null) {
                return BadRequest();
            }
            if (khachHang != null) {
                if (!khachHang.HieuLuc)
                {
                    return BadRequest();
                }
                else {
                    if (khachHang.MatKhau != matkhau.ToMd5Hash(khachHang.RandomKey))
                    {
                        return BadRequest();
                    }
                    else
                    {
                        string gender;
                        if (khachHang.GioiTinh == true)
                        {
                            gender = "Male";
                        }
                        else
                        {
                            gender = "Female";
                        }
                        var kh = new KhachHangVM
                        {
                            Hinh = khachHang.Hinh ?? string.Empty,
                            MaKh = khachHang.MaKh,
                            Password = khachHang.MatKhau,
                            HoTen = khachHang.HoTen,
                            GioiTinh = gender,
                            NgaySinh = DateOnly.FromDateTime(khachHang.NgaySinh),
                            DiaChi = khachHang.DiaChi ?? string.Empty,
                            Email = khachHang.Email ?? string.Empty,
                            DienThoai = khachHang.DienThoai ?? string.Empty
                        };
                        emkh.Add(kh);
                        return emkh;
                    }
                }
            }
            return emkh;
        }
    }
    #endregion
    #region LoginKH
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileKHController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }

        public ProfileKHController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/ProfileKH?makh=thien600
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHangVM>>> GetKhachHangs(string makh)
        {
            var emkh = new List<KhachHangVM>();
            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.MaKh == makh);
            if (khachHang == null)
            {
                return BadRequest();
            }
            if (khachHang != null)
            {
                if (!khachHang.HieuLuc)
                {
                    return BadRequest();
                }
                else
                {
                    string gender;
                    if (khachHang.GioiTinh == true)
                    {
                        gender = "Nam";
                    }
                    else
                    {
                        gender = "Nữ";
                    }
                    var kh = new KhachHangVM
                    {
                        Hinh = khachHang.Hinh ?? string.Empty,
                        MaKh = khachHang.MaKh,
                        Password = khachHang.MatKhau,
                        HoTen = khachHang.HoTen,
                        GioiTinh = gender,
                        NgaySinh = DateOnly.FromDateTime(khachHang.NgaySinh),
                        DiaChi = khachHang.DiaChi ?? string.Empty,
                        Email = khachHang.Email ?? string.Empty,
                        DienThoai = khachHang.DienThoai ?? string.Empty
                    };
                    emkh.Add(kh);
                    return emkh;
                }
            }
            return emkh;
        }
    }
    #endregion

    #region XepHangKH
    [Route("api/[controller]")]
    [ApiController]
    public class XepHangKHController : ControllerBase
    {
        private readonly TmdtdatabaseContext _context;

        public static string[] splitHinh(string hinh)
        {
            string[] hinhSubs = hinh.Split('!');
            return hinhSubs;
        }

        public XepHangKHController(TmdtdatabaseContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7092/api/XepHangKH
        [HttpGet]
        public async Task<ActionResult<IEnumerable<XepHangKHVM>>> GetKhachHangs()
        {
            var khachHangs = _context.KhachHangs.AsQueryable();
            var donHangs = _context.DonHangs.AsQueryable();
            var kh = new List<XepHangKHVM>();
            var results = donHangs
                //.Where(p => p.MaTrangThai == 4)
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
            kh.AddRange(grouped);
            return kh;
        }
    }
    #endregion
}
