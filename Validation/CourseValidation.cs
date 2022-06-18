using FluentValidation;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Validation
{
    public class CourseValidation : AbstractValidator<CourseDTO>
    {
        public CourseValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(course => course.Crn)
                .Must(Crn => Crn != 0)
                .WithMessage("CRN is invalid.")
                .NotEmpty()
                .WithMessage("CRN is required.")
                .Must(Crn => Crn.ToString().Trim().Length == 5)
                .WithMessage("CRN is invalid.");

            RuleFor(course => course.Title).NotEmpty().WithMessage("Course Title is required.");

            RuleFor(course => course.Subject)
                .NotEmpty()
                .WithMessage("Course Subject is required.")
                .Matches(@"^[A-Z]{4}$")
                .WithMessage("Course Subject is invalid.");

            RuleFor(course => course.Code)
                .NotEmpty()
                .WithMessage("Course Code is required.")
                .Matches(@"^[0-9]{3}[A-Z]*$")
                .WithMessage("Course Code is invalid.");

            RuleFor(course => course.Credits.ToString())
                .NotEmpty()
                .WithMessage("Course Credits is required.")
                .Matches(@"^[0-9]+$")
                .WithMessage("Course Credits is invalid.");


            RuleFor(course => course.Days1)
                .Matches(@"^[MTWRFS]{1,7}$")
                .When(x => x.Days1 is not null && x.Days1 != string.Empty)
                .WithMessage("Course Days1 is invalid.")
                .Must(x => string.Join("", x.Distinct()).Equals(x))
                .When(x => x.Days1 is not null && x.Days1 != string.Empty)
                .WithMessage("Course Days1 is invalid.");

            RuleFor(course => course.Days2)
                .Matches(@"^[MTWRFS]{1,7}$")
                .When(x => x.Days2 is not null && x.Days2 != string.Empty)
                .WithMessage("Course Days2 is invalid.")
                .Must(x => string.Join("", x.Distinct()).Equals(x))
                .When(x => x.Days2 is not null && x.Days2 != string.Empty)
                .WithMessage("Course Days2 is invalid.");

            RuleFor(course => course.StartTime1)
                .Matches(@"^([01]*[0-9]|2[0-3]):([0-5][0-9])$")
                .When(x => x.StartTime1 is not null && x.StartTime1 != string.Empty)
                .WithMessage("Course StartTime1 is invalid.");

            RuleFor(course => course.StartTime2)
                .Matches(@"^([01]*[0-9]|2[0-3]):([0-5][0-9])$")
                .When(x => x.StartTime2 is not null && x.StartTime2 != string.Empty)
                .WithMessage("Course StartTime2 is invalid.");

            RuleFor(course => course.EndTime1)
                .Matches(@"^([01]*[0-9]|2[0-3]):([0-5][0-9])$")
                .When(x => x.EndTime1 is not null && x.EndTime1 != string.Empty)
                .WithMessage("Course EndTime1 is invalid.");

            RuleFor(course => course.EndTime2)
                .Matches(@"^([01]*[0-9]|2[0-3]):([0-5][0-9])$")
                .When(x => x.EndTime2 is not null && x.EndTime2 != string.Empty)
                .WithMessage("Course EndTime2 is invalid.");
            
            RuleFor(course => course.SemesterId)
                .NotEmpty()
                .WithMessage("Course Semester is required.")
                .Matches(@"^[0-9]{6}$")
                .WithMessage("Course Semester is invalid.");
        }
    }
}
