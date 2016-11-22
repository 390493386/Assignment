using System.ComponentModel.DataAnnotations;

namespace Assignment.Web.Models
{
    public class UserLoginViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string Account { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我？")]
        public bool RememberMe { get; set; }
    }

    public class AddUserViewModel
    {
        [Required]
        [Display(Name = "账号")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Account { get; set; }

        [Display(Name = "名字")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}