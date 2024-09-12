using Data_Access_Layer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Employee
{
    public class EmployeeJobHistories : BaseEntity
    {

        public string? EmployeeId { get; set; }
        public string? PositionId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Employee? Employee { get; set; }

        public Positions? Position { get; set; }

    }
}
