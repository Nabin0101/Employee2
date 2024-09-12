using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Model;

namespace Entities.Employee
{
    public class Positions : BaseEntity
    {

        public string PositionName { get; set; }

        public ICollection<EmployeeJobHistories> EmployeeJobHistories { get; set; } = new List<EmployeeJobHistories>();
        public ICollection<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();


    }
}
