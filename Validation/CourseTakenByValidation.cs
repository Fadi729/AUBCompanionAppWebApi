using FluentValidation;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Validation
{
    public class CourseTakenByValidation : AbstractValidator<CourseTakenBy_POST_DTO>
    {
        public CourseTakenByValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            #region Rules
            #region UserID Rule
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required")
                .Matches(
                    @"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")
                .WithMessage("UserId must be a valid GUID");
            #endregion

            #region CRN Rule
            RuleFor(courseTakenBy => courseTakenBy.CCrn)
                .NotEmpty()
                .WithMessage("CRN is required.")
                .Must(Crn => Crn.Trim().Length == 5)
                .WithMessage("CRN is invalid.");
            #endregion

            #region SemesterID Rule
            RuleFor(courseTakenBy => courseTakenBy.SemesterId)
                .NotEmpty()
                .WithMessage("Course Semester is required.")
                .Matches(@"^[0-9]{6}$")
                .WithMessage("Course Semester is invalid.");
            #endregion

            #region Grade Rule
            RuleFor(courseTakenBy => courseTakenBy.Grade)
                .Matches(@"^[ABC][+-]*$|^[D]\+*$|^[FIPW]$|^PR$|^NP$")
                .When(
                    courseTakenBy =>
                        courseTakenBy.Grade != null && courseTakenBy.Grade != string.Empty
                )
                .WithMessage("Grade is invalid");
            #endregion
            #endregion
        }
    }
}
