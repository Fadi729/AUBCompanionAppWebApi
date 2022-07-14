namespace CompanionApp.Models.Interfaces;

public interface ICourseTakenBy
{
    Guid UserId { get; set; }
    int CCrn { get; set; }
    string SemesterId { get; set; }
    string? Grade { get; set; }
    Course CCrnNavigation { get; set; }
    Semester Semester { get; set; }
    Profile User { get; set; }
}