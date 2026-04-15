using FluentValidation;
using MiniClinicManagementSystem.Core.DTOs.PatientDTOs;

namespace MiniClinicManagementSystem.Core.Validations.PatientValidation
{
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientDTO>
    {
        public UpdatePatientValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(100).WithMessage("First Name cannot exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FirstName));

            RuleFor(x => x.LastName)
                .MaximumLength(100).WithMessage("Last Name cannot exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.UtcNow).WithMessage("Date of Birth must be in the past.")
                .When(x => x.DateOfBirth != default);

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.PhoneNumber)
             .MaximumLength(20).WithMessage("Phone Number cannot exceed 20 characters.");
        }
    }
}
