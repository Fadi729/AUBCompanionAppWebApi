using FluentValidation;
using CompanionApp.ModelsDTO;

namespace CompanionApp.Validation
{
    public class SemesterValidation : AbstractValidator<SemesterDTO>
    {
        public SemesterValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Semester is required")
                .Matches(@"^[0-9]{6}$")
                .WithMessage("ID is invalid");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .Matches(@"^Fall$|^Spring$|^Summer$")
                .WithMessage("Title is invalid");

            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("Year is required")
                .Matches(@"[0-9]{4}-[0-9]{4}")
                .WithMessage("Year is invalid")
                .Must(x =>
                {
                    string[] years = x.Split('-');
                    int year1      = int.Parse(years[0]);
                    int year2      = int.Parse(years[1]);
                    return year2 == year1 + 1;
                })
                .WithMessage("Year is invalid");
        }
    }
}
