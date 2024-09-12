using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.Employee
{
    public  class GroupByModel
    {
        public double Salary { get; set; }
        public int Count { get; set; }
        public List<String> EmployeeName { get; set; }
    }
}
