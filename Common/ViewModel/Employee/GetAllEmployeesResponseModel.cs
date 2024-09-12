
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.Employee
{
    public class GetAllEmployeesResponseModel
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EmployeeCode { get; set; }
        public double Salary { get; set; }
        public bool IsDisable { get; set; }
        public bool IsDeleted { get; set; }



    }
}
