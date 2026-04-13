using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EcomQLDM.Data;
using EcomQLDM.Helpers;
using EcomQLDM.ViewModels;
using System.Data;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;

namespace EcomQLDM.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly TmdtdatabaseContext db;
        private readonly IMapper _mapper;

        public KhachHangController(TmdtdatabaseContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        
        #region Register
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var khachHang = _mapper.Map<KhachHang>(model);
                    khachHang.RandomKey = MyUtil.GenerateRandomKey();
                    khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
                    khachHang.HieuLuc = true;
                    khachHang.VaiTro = 0;

                    if (Hinh != null)
                    {
                        khachHang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
                        khachHang.Hinh = Hinh.FileName;
                    }

                    db.Add(khachHang);
                    db.SaveChanges();

                    var sendmail = new SendEmail();
                    sendmail.ThongBaoDangKy(model.Email);

                    //var fromAddress = new MailAddress("nguynngthien@gmail.com", "Nguyen Ngoc Thien");
                    //var toAddress = new MailAddress("nguynngthien@gmail.com", "Nguyen Ngoc Thien");
                    //const string fromPassword = "vxqs jrxk bipp bywz";
                    //const string subject = "Subject123456789";
                    //const string body = "Body987654321";

                    //var smtp = new SmtpClient
                    //{
                    //    Host = "smtp.gmail.com",
                    //    Port = 587,
                    //    EnableSsl = true,
                    //    DeliveryMethod = SmtpDeliveryMethod.Network,
                    //    UseDefaultCredentials = false,
                    //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    //};
                    //using (var message = new MailMessage(fromAddress, toAddress)
                    //{
                    //    Subject = subject,
                    //    Body = body
                    //})
                    //{
                    //    smtp.Send(message);
                    //}


                    return RedirectToAction("Home", "HangHoa");
                }
                catch (Exception ex)
                {
                    var mess = $"{ex.Message}";
                }
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
                var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);

                if (khachHang == null)
                {
                    ModelState.AddModelError("error", "Please check your password and username and try again.");
                }
                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("error", "This user was locked");
                    }
                    else
                    {
                        if (khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey))
                        {
                            ModelState.AddModelError("error", "Please check your password and username and try again.");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, khachHang.Email),
                                new Claim(ClaimTypes.Name, khachHang.HoTen),
                                new Claim("Username", khachHang.MaKh),
                                new Claim("Email", khachHang.Email),
                                new Claim(MySetting.CLAIM_CUSTOMERID, khachHang.MaKh),

                                // claim - dynamic role
                                new Claim(ClaimTypes.Role, "Customer")
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
                                return Redirect("/");
                            }

                        }
                    }
                }
            }
            return View();
        }
        #endregion

        #region Profile
        [Authorize]
        public IActionResult Profile(string id)
        {
            id = HttpContext.User.Claims.SingleOrDefault(c => c.Type == "CustomerID").Value;
            var data = db.KhachHangs
                .SingleOrDefault(p => p.MaKh == id);
            if (data == null)
            {
                TempData["Message"] = $"No result for product {id}";
                return Redirect("/404");
            }
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

            var result = new KhachHangVM
            {
                Hinh = image ?? string.Empty,
                MaKh = data.MaKh,
                HoTen = data.HoTen,
                GioiTinh = gender,
                NgaySinh = DateOnly.FromDateTime(data.NgaySinh),
                DiaChi = data.DiaChi ?? string.Empty,
                Email = data.Email ?? string.Empty,
                DienThoai = data.DienThoai ?? string.Empty
            };

            //return View();
            return View(result);
        }

        #endregion

        #region ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(KhachHangVM model)
        {
            var sendmail = new SendEmail();
            sendmail.ThongBaoResetPassword(model.Email, model.MaKh);
            return View(model);
        }
        #endregion

        #region XacNhan
        
        public IActionResult XacNhan()
        {
            return View();
        }

        #endregion

        #region Reset
        public IActionResult Reset(string id)
        {
            ViewBag.Message = TempData["Message"];
            var data = db.KhachHangs
                .SingleOrDefault(p => p.MaKh == id);
            
            var result = new ResetPasswordVM
            {
                MaKh = data.MaKh,
                Password = "",
                Password2 = ""
            };
            return View(result);
        }
        #endregion

        #region ResetPassword
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordVM model)
        {
            if (model.Password != model.Password2)
            {
                TempData["Message"] = "Mật Khẩu Không Khớp, Xin Nhập Lại";
                return RedirectToAction("Reset", "KhachHang", new { id = model.MaKh});
            }
            var khachHang = db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.MaKh);
            //khachHang.MatKhau != model.Password.ToMd5Hash(khachHang.RandomKey);
            var newPassword = model.Password.ToMd5Hash(khachHang.RandomKey);


            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string EDIT = "UPDATE KhachHang SET MatKhau = N'" + newPassword + "' WHERE MaKh = N'" + model.MaKh + "'";
            SqlCommand comd = new SqlCommand(EDIT, conn);

            try
            {
                conn.Open();
                comd.ExecuteNonQuery();

                db.SaveChanges();
                conn.Close();
                return RedirectToAction("DangNhap", "KhachHang");
            }
            catch (Exception ex)
            {
                var mess = $"{ex.Message}";
            }
            return View();
        }
        #endregion

        #region Logout
        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }

        #endregion

        #region EditCustomer
        public IActionResult EditKH(string id)
        {
            var data = db.KhachHangs
                .SingleOrDefault(p => p.MaKh == id);
            if (data == null)
            {
                TempData["Message"] = $"No result for product {id}";
                return Redirect("/404");
            }

            string gender;
            if (data.GioiTinh == true)
            {
                gender = "Nam";
            }
            else
            {
                gender = "Nữ";
            }

            var result = new KhachHangVM
            {
                MaKh = data.MaKh,
                HoTen = data.HoTen,
                GioiTinh = gender,
                NgaySinh = DateOnly.FromDateTime(data.NgaySinh),
                DiaChi = data.DiaChi ?? string.Empty,
                Email = data.Email ?? string.Empty,
                DienThoai = data.DienThoai ?? string.Empty
            };
            return View(result);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(KhachHangVM model)
        {
            int gender;
            if (model.GioiTinh == "Nữ")
            {
                gender = 0;
            }
            else
            {
                gender = 1;
            }

            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection(dbc.ConnectionString);
            string EDIT = "UPDATE KhachHang SET HoTen = N'" + model.HoTen + "', GioiTinh = " + gender + ", NgaySinh = N'" + model.NgaySinh + "', DiaChi = N'" + model.DiaChi + "', Email = N'" + model.Email + "', DienThoai = N'" + model.DienThoai + "' WHERE MaKh = N'" + model.MaKh + "'";
            SqlCommand comd = new SqlCommand(EDIT, conn);

            try
            {
                conn.Open();
                comd.ExecuteNonQuery();

                db.SaveChanges();
                conn.Close();
                return RedirectToAction("Profile", "KhachHang");
            }
            catch (Exception ex)
            {
                var mess = $"{ex.Message}";
            }
            return View();
        }
        #endregion



    }
}
