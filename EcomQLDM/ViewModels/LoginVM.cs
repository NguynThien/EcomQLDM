using System.ComponentModel.DataAnnotations;

namespace EcomQLDM.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username không hợp lệ")]
        [MaxLength(20, ErrorMessage = "20 letters max")]
        public string UserName { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Mật khẩu không đúng")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
