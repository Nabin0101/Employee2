using FluentValidation;
using FluentValidation.Validators;
using Infrastructure.Common.ViewModel.Employee;

namespace Infrastructure.Common.Validator
{
    public class CustomValidator : AbstractValidator<EmployeeDTO>
    {
        public CustomValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .Matches("^[a-zA-Z]+$").WithMessage("First Name should contain only alphabets.");

            RuleFor(x => x.MiddleName)
                
                .Matches("^[a-zA-Z]*$").WithMessage("Middle Name should contain only alphabets.")
                .When(x => !string.IsNullOrEmpty(x.MiddleName));

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .Matches("^[a-zA-Z]+$").WithMessage("Last Name should contain only alphabets.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");
                
            RuleFor(x => x.Salary)
                .NotEmpty().WithMessage("Salary is required");

            RuleFor(x => x.EmployeeCode)
                .NotEmpty().WithMessage("Employee Code is required")
                .Matches("^[A-Z0-9]+$").WithMessage("Employee Code should contain only capital letters and numbers.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required");

            RuleFor(x => x.PositionId)
                .NotEmpty().WithMessage("Position Id is required");

            RuleFor(x => x.JobStartDate)
                .NotEmpty().WithMessage("Job Start Date is required")
                .Must(BeAValidDate).WithMessage("Job Start Date should be a valid date.");

            RuleFor(x => x.JobEndDate)
                .NotEmpty().WithMessage("Job End Date is required")
                .Must(BeAValidDate).WithMessage("Job End Date should be a valid date.")
                .GreaterThanOrEqualTo(x => x.JobStartDate).WithMessage("Job End Date cannot be before the Job Start Date.");

            RuleFor(x => x.IsDisable)
                .NotNull().WithMessage("IsDisabled is required")
                .Must(x => x ==true || x ==false).WithMessage("IsDisabled should be a boolean value.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
