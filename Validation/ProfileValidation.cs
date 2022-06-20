using FluentValidation;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Validation
{
    public class ProfileValidation : AbstractValidator<ProfileCommandDTO>
    {
        public ProfileValidation()
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
                    .Matches    (@"[a-z]{3}[0-9]{2}@(mail.aub.edu|aub.edu.lb)")
                    .WithMessage("Invalid Email");  
            #endregion
            #endregion
        }
    }
}
