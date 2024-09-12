
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.EmployeeHistory
{
    public class AllEmployeeHistoryDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PositionName { get; set; }
    }
}
