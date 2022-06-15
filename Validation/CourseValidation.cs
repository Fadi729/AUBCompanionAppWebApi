using CompanionApp.ModelsDTO;
using FluentValidation;

namespace CompanionApp.Validation
{
    public class CourseValidation : AbstractValidator<CourseDTO>
    {
        public CourseValidation()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(course => course.Crn)
                .NotEmpty()
                .WithMessage("CRN is required")
                .Must(Crn => Crn.ToString().Trim().Length == 5)
                .WithMessage("CRN is invalid.");

            RuleFor(course => course.Title).NotEmpty().WithMessage("Course Title is required.");

            RuleFor(course => course.Subject)
                .NotEmpty()
                .WithMessage("Course Subject is required.")
                .Matches(@"\b[A-Z]{4}\b")
                .WithMessage("Course Subject is invalid.");

            RuleFor(course => course.Code)
                .NotEmpty()
                .WithMessage("Course Code is required.")
                .Matches(@"\b[0-9]{3}[A-Z]*\b")
                .WithMessage("Course Code is invalid");

            RuleFor(course => course.Credits.ToString())
                .NotEmpty()
                .WithMessage("Course Credits is required.")
                .Matches(@"\b[0-9]+\b")
                .WithMessage("Course Credits is invalid");

            RuleFor(course => course.SemesterId)
                .NotEmpty()
                .WithMessage("Course Semester is required.")
                .Matches(@"\b[0-9]{6}\b")
                .WithMessage("Course Semester is invalid");

            RuleFor(course => course.Days1)
                .Matches(@"\b[MTWRFS]{1,7}\b")
                .When(x => x.Days1 is not null && x.Days1 != string.Empty)
                .WithMessage("Course Days1 is invalid.")
                .Must(x => string.Join("", x.Distinct()).Equals(x))
                .When(x => x.Days1 is not null && x.Days1 != string.Empty)
                .WithMessage("Course Days1 is invalid.");

            RuleFor(course => course.Days2)
                .Matches(@"\b[MTWRFS]{1,7}\b")
                .When(x => x.Days1 is not null && x.Days1 != string.Empty)
                .WithMessage("Course Days1 is invalid.")
                .Must(x => string.Join("", x.Distinct()).Equals(x))
                .When(x => x.Days1 is not null && x.Days1 != string.Empty)
                .WithMessage("Course Days1 is invalid.");

            RuleFor(course => course.StartTime1)
                .Matches(@"\b[0-9]{1,2}:[0-9]{2}\b")
                .When(x => x.StartTime1 is not null && x.StartTime1 != string.Empty)
                .WithMessage("Course StartTime1 is invalid.");

            RuleFor(course => course.StartTime2)
                .Matches(@"\b[0-9]{1,2}:[0-9]{2}\b")
                .When(x => x.StartTime2 is not null && x.StartTime2 != string.Empty)
                .WithMessage("Course StartTime2 is invalid.");

            RuleFor(course => course.EndTime1)
                .Matches(@"\b[0-9]{1,2}:[0-9]{2}\b")
                .When(x => x.EndTime1 is not null && x.EndTime1 != string.Empty)
                .WithMessage("Course EndTime1 is invalid.");

            RuleFor(course => course.EndTime2)
                .Matches(@"\b[0-9]{1,2}:[0-9]{2}\b")
                .When(x => x.EndTime2 is not null && x.EndTime2 != string.Empty)
                .WithMessage("Course EndTime2 is invalid.");
        }
    }
}
