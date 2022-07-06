namespace CompanionApp.Models.Interfaces;

public interface ICourse
{
    int Crn { get; set; }
    string Title { get; set; }
    string Subject { get; set; }
    string Code { get; set; }
    byte Credits { get; set; }
    string? Section { get; set; }
    string? Attribute { get; set; }
    string? Levels { get; set; }
    string? Days1 { get; set; }
    TimeSpan? StartTime1 { get; set; }
    TimeSpan? EndTime1 { get; set; }
    string? Location1 { get; set; }
    string? Type1 { get; set; }
    string? Instructor1 { get; set; }
    string? Days2 { get; set; }
    TimeSpan? StartTime2 { get; set; }
    TimeSpan? EndTime2 { get; set; }
    string? Location2 { get; set; }
    string? Type2 { get; set; }
    string? Instructor2 { get; set; }
    string SemesterId { get; set; }
    string? Prerequisites { get; set; }
    string? Corequisites { get; set; }
    string? MutualExclusion { get; set; }
    string? Restrictions { get; set; }
    Semester Semester { get; set; }
    ICollection<CourseTakenBy> CourseTakenBy { get; set; }
}