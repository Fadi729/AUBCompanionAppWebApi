namespace CompanionApp.Models.Interfaces;

public interface ISemester
{
    string Id { get; set; }
    string Title { get; set; }
    string Year { get; set; }
    ICollection<CourseTakenBy> CourseTakenBy { get; set; }
    ICollection<Course> Courses { get; set; }
}