using CompanionApp.Models.Interfaces;

namespace CompanionApp.Models
{
    public partial class CourseTakenBy : ICourseTakenBy
    {
        #region Attributes
        public Guid    UserId     { get; set; }
        public int     CCrn       { get; set; }
        public string  SemesterId { get; set; } = null!;
        public string? Grade      { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Course   CCrnNavigation { get; set; } = null!;
        public virtual Semester Semester       { get; set; } = null!;
        public virtual Profile  User           { get; set; } = null!;
        #endregion
    }
}