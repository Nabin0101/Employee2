
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.Employee
{
    public class EmployeeDTO
    {

        public string FirstName { get; set; }


        public string MiddleName { get; set; }


        public string LastName { get; set; }


        public string Address { get; set; }



        public string Email { get; set; }

        public double Salary { get; set; }


        public string EmployeeCode { get; set; }


        public string PositionId { get; set; }


        [DataType(DataType.Date)]
        public DateTime JobStartDate { get; set; }


        [DataType(DataType.Date)]
        public DateTime JobEndDate { get; set; }


        public bool IsDisable { get; set; }


    }
}
