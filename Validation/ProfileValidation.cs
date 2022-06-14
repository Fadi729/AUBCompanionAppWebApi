using CompanionApp.ModelsDTO;
using FluentValidation;

namespace CompanionApp.Validation
{
    public class ProfileValidation : AbstractValidator<ProfileCommandDTO>
    {
        public ProfileValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .Matches(@"[a-z]{3}[0-9]{2}@(mail.aub.edu|aub.edu.lb)")
                .WithMessage("Invalid Email");
        }
    }
}
