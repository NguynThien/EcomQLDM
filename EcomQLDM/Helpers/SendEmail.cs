using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;
using System.Threading.Tasks;
namespace EcomQLDM.Helpers
{
    public class SendEmail
    {
        private const string diaChiEmail = "nguynngthien@gmail.com";
        private const string password = "vxqs jrxk bipp bywz";
        public void ThongBaoDangKy(string diaChiEmailKH)
        {
            try
            {
                // Gmail SMTP server settings
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587; // TLS port

                // Sender Gmail credentials (use App Password, not your main password)
                string senderEmail = diaChiEmail;
                string senderEmailPassword = password; // 16-char app password

                // Recipient email
                string recipientEmail = diaChiEmailKH;

                // Message html body

                var htmlBody = $@"
        <html>
          <body style='font-family: Arial, sans-serif; line-height:1.6;'>
            <p><strong>Kính chào khách hàng,</strong></p>

            <p>Cửa hàng <strong>Điện máy Caelum</strong> xin chân thành cảm ơn Quý khách đã đăng ký tài khoản mua sắm tại hệ thống của chúng tôi!</p>

            <p>Từ nay, Quý khách có thể dễ dàng:</p>
            <ul>
              <li>Mua sắm hàng ngàn sản phẩm điện máy chính hãng với giá ưu đãi.</li>
              <li>Theo dõi đơn hàng, lịch sử mua sắm và tình trạng bảo hành.</li>
              <li>Nhận thông tin sớm nhất về các chương trình khuyến mãi, ưu đãi đặc biệt dành riêng cho thành viên Caelum.</li>
            </ul>

            <p>Chúng tôi hy vọng mang đến cho Quý khách <strong>trải nghiệm mua sắm tiện lợi, nhanh chóng và đáng tin cậy nhất</strong>.</p>

            
            <p>Trân trọng,<br/><strong>Đội ngũ Cửa hàng Điện máy Caelum</strong></p>
          </body>
        </html>";

                // Create the email message
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail, "Điện Máy Caelum");
                mail.To.Add(recipientEmail);
                mail.Subject = "Chào mừng bạn đến với Cửa hàng Điện máy Caelum!";
                mail.Body = htmlBody;
                mail.IsBodyHtml = true; // Set to true if sending HTML content
                
                // Configure SMTP client
                using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(senderEmail, senderEmailPassword);
                    smtp.EnableSsl = true; // Enable TLS encryption
                    smtp.Send(mail);
                }

            }
            catch (SmtpException ex)
            {
                Console.WriteLine("SMTP Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
        }

        public void ThongBaoResetPassword(string diaChiEmailKH, string maKH)
        {
            try
            {
                // Gmail SMTP server settings
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587; // TLS port

                // Sender Gmail credentials (use App Password, not your main password)
                string senderEmail = diaChiEmail;
                string senderEmailPassword = password; // 16-char app password

                // Recipient email
                string recipientEmail = diaChiEmailKH;

                // Message html body

                var htmlBody = $@"
        <html>
          <body style='font-family: Arial, sans-serif; line-height:1.6;'>
            <p><strong>Kính chào khách hàng,</strong></p>

            <p>Cửa hàng <strong>Điện máy Caelum</strong>, chúng tôi đã nhận được yêu cầu cập nhật lại mật khẩu của bạn.</p>

            <p>Để thực hiện cập nhật lại mật khẩu xin truy cập vào link bên dưới:</p>
            
            <a href='https://localhost:7092/KhachHang/Reset/" + maKH + $@"'>https://localhost:7092/KhachHang/Reset/" + maKH + $@"</a>

            <p>Chúng tôi hy vọng mang đến cho Quý khách <strong>trải nghiệm mua sắm tiện lợi, nhanh chóng và đáng tin cậy nhất</strong>.</p>

            
            <p>Trân trọng,<br/><strong>Đội ngũ Cửa hàng Điện máy Caelum</strong></p>
          </body>
        </html>";

                // Create the email message
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail, "Điện Máy Caelum");
                mail.To.Add(recipientEmail);
                mail.Subject = "Cập Nhật Mật Khẩu";
                mail.Body = htmlBody;
                mail.IsBodyHtml = true; // Set to true if sending HTML content

                // Configure SMTP client
                using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(senderEmail, senderEmailPassword);
                    smtp.EnableSsl = true; // Enable TLS encryption
                    smtp.Send(mail);
                }

            }
            catch (SmtpException ex)
            {
                Console.WriteLine("SMTP Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
            }
        }

    }
}
