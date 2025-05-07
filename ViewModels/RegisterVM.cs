using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TECHNOVA.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự")]
        public string customerID { get; set; }

        [Display(Name = "Password")]

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "Fullname")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string fullName { get; set; }

        [Display(Name = "Mobile No")]
        [MaxLength(24, ErrorMessage = "Tối đa 24 kí tự")]
        [RegularExpression(@"0[98753]\d{8}", ErrorMessage = "Chưa đúng định dạng di động Việt Nam")]
        public string phone { get; set; }
        [Display(Name = "Email")]

        [EmailAddress(ErrorMessage = "Chưa đúng định dạng email")]
        public string Email { get; set; }

        [NotMapped]
        [Compare("password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
