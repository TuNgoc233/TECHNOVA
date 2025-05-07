using System.ComponentModel.DataAnnotations;

namespace TECHNOVA.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập hoặc email")]
        [Display(Name = "Username/Email")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Keep me signed in")]
        public bool RememberMe { get; set; }
    }
}
