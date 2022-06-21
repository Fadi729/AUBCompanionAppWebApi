using FluentValidation;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Validation
{
    public class FollowingsValidation : AbstractValidator<FollowingPOSTDTO>
    {
        public FollowingsValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            
            #region Rules
            #region UserId Rules
            RuleFor(x => x.UserId)
                       .NotEmpty()
                       .WithMessage("UserId is required")
                       .Matches(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")
                       .WithMessage("UserId must be a valid GUID");
            #endregion

            #region IsFollowing Rule
            RuleFor(x => x.IsFollowing)
                    .NotEmpty()
                    .WithMessage("IsFollowing is required")
                    .Matches(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")
                    .WithMessage("IsFollowing must be a valid GUID");
            #endregion            
            #endregion
        }
    }
}
