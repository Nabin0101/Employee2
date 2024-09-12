using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.ViewModel.EmployeeHistory;
using Infrastructure.Common.ViewModel.ResponseModel;

namespace Business_Layer.EmployeeJobHistory
{
    public interface IEmployeeJobHistories
    {
         Task<APIResponseModel> GetEmployeeHistory(String id);
         Task<APIResponseModel> UpdateEmployeeJobHistory(String id, UpdateEmployeeJobHistoryDto updateEmployeeJobHistoryDto);
         Task<APIResponseModel> GetAllEmployeeJobHistory(PaginationModel paginationModel);
         Task<APIResponseModel> AddEmployeeJobs(AddEmployeeJob addEmployeeJob);
    }
}
