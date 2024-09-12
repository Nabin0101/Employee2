using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.LoginSignup
{
    public class SignUpDTO
    {
        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First Name should contain only alphabets")]

        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last Name should contain only alphabets")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        [RegularExpression(@"^[a-z][a-z0-9_]*$", ErrorMessage = "Username must start with a lowercase letter and can only contain lowercase letters, numbers, and underscores.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }
    }
}
