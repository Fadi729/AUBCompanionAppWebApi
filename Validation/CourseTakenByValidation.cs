using FluentValidation;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Validation
{
    public class CourseTakenByValidation : AbstractValidator<CourseTakenBy_POST_DTO>
    {
        public CourseTakenByValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Continue;

            #region Rules
            #region UserID Rule
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            #endregion

            #region CRN Rule
            RuleFor(courseTakenBy => courseTakenBy.CCrn)
                    .Must(Crn => Crn != 0)
                    .WithMessage("CRN is invalid.")
                    .NotEmpty()
                    .WithMessage("CRN is required.")
                    .Must(Crn => Crn.ToString().Trim().Length == 5)
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
                .When(courseTakenBy => courseTakenBy.Grade != null && courseTakenBy.Grade != string.Empty);
            #endregion
            #endregion
        }
    }
}
