using Data_Access_Layer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Employee
{
    public class Employee : BaseEntity
    {

        public string? PersonId { get; set; }

        public People? People { get; set; }

        public double Salary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? EmployeeCode { get; set; }
        public bool IsDisable { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<EmployeeJobHistories>? EmployeeJobHistories { get; set; }
        public ICollection<EmployeePosition>? EmployeePositions { get; set; }


    }
}
