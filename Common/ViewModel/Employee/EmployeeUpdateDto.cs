
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.Employee
{
    public class EmployeeUpdateDto
    {
        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "FirstName  should be in alphabet")]
        public string FirstName { get; set; }



        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "FirstName  should be in alphabet")]
        public string MiddleName { get; set; }

        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "FirstName  should be in alphabet")]
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, 100000, ErrorMessage = "Salary must be a positive number.")]
        public double Salary { get; set; }


        [Required]
        [MaxLength(5)]

        public string EmployeeCode { get; set; }



        [Required]
        [DataType(DataType.Date)]
        public DateTime JobStartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime JobEndDate { get; set; }

        [Required]
        public bool IsDisable { get; set; }

    }
}
