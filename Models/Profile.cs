using CompanionApp.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
namespace CompanionApp.Models
{
    public partial class Profile : IdentityUser<Guid>, IProfile
    {
        public Profile()
        {
            Comments                        = new HashSet<Comment>();
            CourseTakenBy                   = new HashSet<CourseTakenBy>();
            FollowingIsFollowingNavigations = new HashSet<Following>();
            FollowingUsers                  = new HashSet<Following>();
            Likes                           = new HashSet<Like>();
            Posts                           = new HashSet<Post>();
        }

        #region Attributes
        public Guid    Id        { get; set; }
        public string? PasswordHash { get; set; }
        public string  FirstName { get; set; } = null!;
        public string  LastName  { get; set; } = null!;
        public string  Email     { get; set; } = null!;
        public string? Major     { get; set; }
        public string? Class     { get; set; }
        #endregion
        #region Navigation Properties
        public virtual ICollection<Comment      > Comments                        { get; set; }
        public virtual ICollection<CourseTakenBy> CourseTakenBy                   { get; set; }
        public virtual ICollection<Following    > FollowingIsFollowingNavigations { get; set; }
        public virtual ICollection<Following    > FollowingUsers                  { get; set; }
        public virtual ICollection<Like         > Likes                           { get; set; }
        public virtual ICollection<Post         > Posts                           { get; set; }
        #endregion
    }
}
