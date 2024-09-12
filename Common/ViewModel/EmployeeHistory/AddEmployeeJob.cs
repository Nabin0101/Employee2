
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.EmployeeHistory
{
    public class AddEmployeeJob
    {
        [Required(ErrorMessage = "Employee  Id is required")]

        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "Position  Id is required")]
        public string PositionId { get; set; }
        [Required(ErrorMessage = "StartDate   is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate   is required")]
        public DateTime EndDate { get; set; }
    }
}
