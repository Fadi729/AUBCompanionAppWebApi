using FluentValidation;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Validation.CommentValidation
{
    public class EditCommentValidation : AbstractValidator<CommentPUTCommandDTO>
    {
        public EditCommentValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            
            #region Rules
            #region Id Rule
            RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id is required")
                    .Matches(
                        @"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")
                    .WithMessage("Id must be a valid GUID"); 
            #endregion

            #region UserID Rule
            RuleFor(x => x.UserID)
                    .NotEmpty()
                    .WithMessage("UserId is required")
                    .Matches(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")
                    .WithMessage("UserId must be a valid GUID");
            #endregion

            #region PostId Rule
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
