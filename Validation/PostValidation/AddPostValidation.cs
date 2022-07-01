using FluentValidation;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Validation.PostValidation
{
    public class AddPostValidation : AbstractValidator<PostPOSTCommandDTO>
    {
        public AddPostValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            #region Rules
            #region UserID Rule
            //RuleFor(x => x.UserId)
            //        .NotEmpty()
            //        .WithMessage("UserId is required")
            //        .Matches(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")
            //        .WithMessage("UserId must be a valid GUID");
            #endregion
            #endregion
        }
    }
}
