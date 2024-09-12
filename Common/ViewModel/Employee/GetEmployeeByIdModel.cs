
using Infrastructure.Common.ViewModel.EmployeeHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.Employee
{
    public class GetEmployeeByIdModel
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeCode { get; set; }
        public double Salary { get; set; }
        public bool IsDisable { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<AllEmployeeHistoryDTO> EmployeeJobHistories { get; set; }

    }
}
