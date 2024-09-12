using Business_Layer.EmployeeService;
using Data_Access_Layer.ApplicationContext;
using Entities.Employee;
using FluentValidation;
using Infrastructure.Common.ViewModel.Employee;
using Infrastructure.Common.ViewModel.EmployeeHistory;
using Infrastructure.Common.ViewModel.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System.Linq;

namespace Business_Layer.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly APIResponseModel _apiResponse;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IValidator<EmployeeDTO> _validator;
        public EmployeeService(ApplicationDBContext dbContext,
                                    APIResponseModel apiResponse,
                                    ISieveProcessor sieveProcessor,
                                    IValidator<EmployeeDTO> validator)
        {
            _dbContext = dbContext;
            _apiResponse = apiResponse;
            _sieveProcessor = sieveProcessor;
            _validator = validator;
        }

        public async Task<APIResponseModel> SaveEmployee(EmployeeDTO employeeDto)
        {
            var validationResult = await _validator.ValidateAsync(employeeDto);
            if (!validationResult.IsValid)
            {
                _apiResponse.Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var position = await _dbContext.Positions.FindAsync(employeeDto.PositionId);
                    if (position == null)
                    {
                        _apiResponse.Message = "Position not found.";
                        _apiResponse.IsSuccess = false;
                        return _apiResponse;
                    }
                    var people = new People
                    {
                        FirstName = employeeDto.FirstName,
                        MiddleName = employeeDto.MiddleName,
                        LastName = employeeDto.LastName,
                        Email = employeeDto.Email,
                        Address = employeeDto.Address,
                    };
                    await _dbContext.People.AddAsync(people);

                    var utcStartDate = employeeDto.JobStartDate.ToUniversalTime();
                    var utcEndDate = employeeDto.JobEndDate.ToUniversalTime();


                    var employee = new Employee
                    {
                        PersonId = people.Id,
                        Salary = employeeDto.Salary,
                        StartDate = utcStartDate,
                        EndDate = utcEndDate,
                        EmployeeCode = employeeDto.EmployeeCode,
                        IsDisable = employeeDto.IsDisable
                    };
                    await _dbContext.Employee.AddAsync(employee);

                    var jobHistory = new EmployeeJobHistories
                    {
                        EmployeeId = employee.Id,
                        PositionId = position.Id,
                        StartDate = utcStartDate,
                        EndDate = utcEndDate,
                    };
                    await _dbContext.EmployeeJobHistories.AddAsync(jobHistory);

                    var employeePositions = new EmployeePosition
                    {
                        EmployeeId = employee.Id,
                        PositionId = position.Id
                    };
                    await _dbContext.EmployeePositions.AddAsync(employeePositions);

                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    _apiResponse.Data = employee;
                    return _apiResponse;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _apiResponse.Message = ex.ToString();
                    _apiResponse.IsSuccess = false;
                    return _apiResponse;
                }
            }
        }
        public async Task<APIResponseModel> GetListOfEmployee(PaginationModel paginationModel)
        {
            try
            {
                var query = _dbContext.Employee
                                        .Include(e => e.People)
                                        .Include(e => e.EmployeeJobHistories)
                                        .Include(e => e.EmployeePositions)
                                        .Where(e => !e.IsDeleted)
                                        .AsQueryable()
                                        .AsNoTracking();



                var sieveModel = new SieveModel()
                {
                    PageSize = paginationModel.PageSize,
                    Page = paginationModel.PageNumber,
                    Filters = paginationModel.Filter,
                    ////Filters = !string.IsNullOrEmpty(paginationModel.Filter)
                    ////    ? $"People.FirstName@={paginationModel.Filter},People.LastName@={paginationModel.Filter},People.Email@={paginationModel.Filter},People.Address@={paginationModel.Filter}"
                    ////    : null
                };



                var data = _sieveProcessor.Apply(sieveModel, query, applyPagination: false);

                var result = data.Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize)
                                 .Take(paginationModel.PageSize);

                var response = await result.Select(a => new GetAllEmployeesResponseModel
                {
                    Id = a.Id,
                    PersonId = a.PersonId,
                    FirstName = a.People.FirstName,
                    LastName = a.People.LastName,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    EmployeeCode = a.EmployeeCode,
                    Salary = a.Salary
                }).ToListAsync();

                var totalCount = result.Count();

                _apiResponse.Data = new
                {
                    TotalRecords = totalCount,
                    PageNumber = sieveModel.Page,
                    PageSize = sieveModel.PageSize,
                    Employees = response
                };
                return _apiResponse;

            }
            catch (Exception ex)
            {
                _apiResponse.Message = ex.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;

            }

        }
        public async Task<APIResponseModel> GetEmployee(string id)
        {
            try
            {
                var employee = await _dbContext.Employee
                                  .Include(e => e.People)
                                  .Include(e => e.EmployeeJobHistories)
                                  .Include(e => e.EmployeePositions)
                                  .Where(e => e.Id == id && !e.IsDeleted)
                                  .Select(e => new GetEmployeeByIdModel
                                  {
                                      Id = e.Id,
                                      PersonId = e.PersonId,
                                      FirstName = e.People.FirstName,
                                      LastName = e.People.LastName,
                                      EmployeeCode = e.EmployeeCode,
                                      Salary = e.Salary,
                                      IsDisable = e.IsDisable,
                                      IsDeleted = e.IsDeleted,
                                      EmployeeJobHistories = e.EmployeeJobHistories
                                                            .Select(a => new AllEmployeeHistoryDTO
                                                            {
                                                                StartDate = a.StartDate,
                                                                EndDate = a.EndDate,
                                                                PositionName = a.Position.PositionName
                                                            }).ToList(),

                                  })
                          .FirstOrDefaultAsync();

                _apiResponse.Data = employee;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.Message = ex.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
        }
        public async Task<APIResponseModel> UpdateEmployee(EmployeeUpdateDto employee, string id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingEmployee = await _dbContext.Employee
                                                             .Include(e => e.People)
                                                            .FirstOrDefaultAsync(e => e.Id == id);

                    if (existingEmployee == null)
                    {
                        _apiResponse.Message = "Employee not found.";
                        _apiResponse.IsSuccess = false;
                        return _apiResponse;
                    }
                    var people = existingEmployee.People;

                    people.FirstName = employee.FirstName;
                    people.MiddleName = employee.MiddleName;
                    people.LastName = employee.LastName;
                    people.Address = employee.Address;
                    people.Email = employee.Email;
                    existingEmployee.Salary = employee.Salary;
                    existingEmployee.EmployeeCode = employee.EmployeeCode;
                    existingEmployee.IsDisable = employee.IsDisable;

                    _dbContext.People.Update(people);
                    _dbContext.Employee.Update(existingEmployee);
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                    _apiResponse.Message = "Employee updated successfully.";
                    return _apiResponse;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _apiResponse.Message = ex.ToString();
                    _apiResponse.IsSuccess = false;
                    return _apiResponse;
                }
            }
        }
        public async Task<APIResponseModel> DeleteEmployee(string id)
        {
            try
            {
                var employee = await _dbContext.Employee.FirstOrDefaultAsync(e => e.Id == id);
                if (employee == null)
                {
                    _apiResponse.Message = "No Employee is found";
                    _apiResponse.IsSuccess = false;
                    return _apiResponse;

                }
                else if (employee.IsDeleted)
                {
                    _apiResponse.Message = "This Employee hass been deleted";
                    _apiResponse.IsSuccess = false;
                    return _apiResponse;
                }
                else
                {
                    employee.IsDeleted = true;

                    _dbContext.Employee.Update(employee);
                    await _dbContext.SaveChangesAsync();

                    _apiResponse.Message = "Employee successfully deleted.";
                    _apiResponse.IsSuccess = true;
                    return _apiResponse;
                }
            }
            catch (Exception ex)
            {
                _apiResponse.Message = ex.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }

        }

        public async Task<APIResponseModel> SearchEmployeesByFirstName(string firstName)
        {
            try
            {
                var data = await _dbContext.Employee
                    .Include(a => a.People)
                    .Where(a => EF.Functions.Like(a.People.FirstName, $"%{firstName}%"))
                    .Select(a => new GetAllEmployeesResponseModel
                    {
                        Id = a.Id,
                        PersonId = a.PersonId,
                        FirstName = a.People.FirstName,
                        LastName = a.People.LastName,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        EmployeeCode = a.EmployeeCode,
                        Salary = a.Salary
                    })
                    .ToListAsync();
                if (data == null)
                {
                    _apiResponse.Message = "No employees found with the given first name.";
                    _apiResponse.IsSuccess = false;
                    return _apiResponse;
                }
                _apiResponse.Data = data;
                return _apiResponse;
            }
            catch (Exception e)
            {
                _apiResponse.Message = e.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
        }

        public async Task<APIResponseModel> GroupBySalary()
        {
            try
            {
                var data = await _dbContext.Employee
                                        .GroupBy(e => e.Salary)
                                        .Select(a => new GroupByModel
                                        {
                                            Salary = a.Key,
                                            Count = a.Count(),
                                            EmployeeName = a.Select(e => e.People.FirstName + " " + e.People.LastName).ToList()
                                        }).ToListAsync();

                if (data == null)
                {
                    _apiResponse.Message = "No data found";
                    _apiResponse.IsSuccess = false;
                    return _apiResponse;
                }
                else
                {
                    _apiResponse.Message = "Data Returned Successfully";
                    _apiResponse.Data = data;
                    return _apiResponse;
                }

            }
            catch (Exception e)
            {
                _apiResponse.Message = e.ToString();
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
        }
    }
}
