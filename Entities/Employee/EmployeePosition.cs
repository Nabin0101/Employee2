using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Employee
{
    public class EmployeePosition
    {
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string PositionId { get; set; }
        public Positions Position { get; set; }


    }
}
