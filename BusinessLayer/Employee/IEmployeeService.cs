using Data_Access_Layer.Model;
using Infrastructure.Common.ViewModel.Employee;
using Infrastructure.Common.ViewModel.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;

namespace Business_Layer.EmployeeService
{
    public interface IEmployeeService
    {
        Task<APIResponseModel> SaveEmployee(EmployeeDTO employeeDto);
        Task<APIResponseModel> GetListOfEmployee(PaginationModel paginationModel);
        Task<APIResponseModel> DeleteEmployee(string id);
        Task<APIResponseModel> GetEmployee(String id);
        Task<APIResponseModel> UpdateEmployee(EmployeeUpdateDto employee, string id);
        Task<APIResponseModel> SearchEmployeesByFirstName(string firstName);
        Task<APIResponseModel> GroupBySalary();
    }
}
