using FluentValidation;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Validation
{
    public class LikeValidation : AbstractValidator<LikePOSTDTO>
    {
        public LikeValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            #region Rules
            #region UserID Rule
            RuleFor(x => x.UserId)
                    .NotEmpty()
                    .WithMessage("UserId is required")
                    .Matches(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")
                    .WithMessage("UserId must be a valid GUID");
            #endregion

            #region PostID Rule
            RuleFor(x => x.PostId)
                    .NotEmpty()
                    .WithMessage("PostId is required")
                    .Matches(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")
                    .WithMessage("PostId must be a valid GUID");
            #endregion 
            #endregion
        }
    }
}
