using FluentValidation;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Validation.CommentValidation
{
    public class AddCommentValidation : AbstractValidator<CommentPOSTCommandDTO>
    {
        public AddCommentValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            #region Rules
            #region UserID Rule
            RuleFor(x => x.UserID)
                    .NotEmpty()
                    .WithMessage("UserId is required")
                    .Matches(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")
                    .WithMessage("UserId must be a valid GUID");
            #endregion

            #region PostID Rule
            RuleFor(x => x.PostID)
                    .NotEmpty()
                    .WithMessage("PostId is required")
                    .Matches(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")
                    .WithMessage("PostId must be a valid GUID");
            #endregion

            #region Text Rule
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text Is Required");
            #endregion 
            #endregion
        }
    }
}
