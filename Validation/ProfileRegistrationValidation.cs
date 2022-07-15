using FluentValidation;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Validation
{
    public class ProfileRegistrationValidation : AbstractValidator<ProfileRegistrationDTO>
    {
        public ProfileRegistrationValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            #region Rules
            #region FirstName Rule
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First name is required.");
            #endregion
            #region LastName Rule
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last name is required.");
            #endregion
            #region Email Rule

            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .Matches(@"[a-z]{3}[0-9]{2}@(mail.aub.edu|aub.edu.lb)")
                .WithMessage("Invalid Email");
            #endregion
            #region Password Rule
            RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.\@]+").WithMessage("Your password must contain at least one (!? *.@).");
            #endregion
            #endregion
        }
    }
}
