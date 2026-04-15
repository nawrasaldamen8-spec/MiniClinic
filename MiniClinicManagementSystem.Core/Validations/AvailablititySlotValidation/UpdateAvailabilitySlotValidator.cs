using FluentValidation;
using MiniClinicManagementSystem.Core.DTOs.AvailabilitySlotDTOs;

namespace MiniClinicManagementSystem.Core.Validations.AvailablititySlotValidation
{
    public class UpdateAvailabilitySlotValidator : AbstractValidator<UpdateAvailabilitySlotDTO>
    {
        public UpdateAvailabilitySlotValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Availability slot is required.")
                .GreaterThan(0).WithMessage("Invalid availability slot.");

            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.")
                .GreaterThan(0)
                .WithMessage("Invalide doctor");

            RuleFor(x => x.DayOfWeek)
                .NotEmpty().WithMessage("Day of week is required.")
                .IsInEnum()
                .WithMessage("Invalid day of week");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Start time is required.")
                .GreaterThan(TimeSpan.Zero).WithMessage("Start time must be after midnight.")
                .LessThan(TimeSpan.FromHours(24)).WithMessage("Start time must be before midnight.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End time is required.")
                .GreaterThan(TimeSpan.Zero).WithMessage("End time must be after midnight.")
                .LessThan(TimeSpan.FromHours(24)).WithMessage("End time must be before midnight.");

            RuleFor(x => x)
                .Must(x => x.StartTime < x.EndTime)
                .WithMessage("Start time must be before end time.");
        }

    }
}
