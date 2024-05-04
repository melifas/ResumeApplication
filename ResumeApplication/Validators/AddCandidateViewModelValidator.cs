using FluentValidation;
using ResumeApplication.Models.ViewModels.Candidate;

namespace ResumeApplication.Validators
{
    public class AddCandidateViewModelValidator : AbstractValidator<AddCandidateViewModel>
    {
        public AddCandidateViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.Moblie).Matches(Constants.PhoneRegEx).WithMessage("Invalid phone number format.Phone number must have 10 digits ");

        }
    }
}
