
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.ViewModel.EmployeeHistory
{
    public class EmployeeHistoryDto
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public List<JobHistoryDto> JobHistories { get; set; }

    }


}

