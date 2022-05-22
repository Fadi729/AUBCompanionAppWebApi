using System;
using System.Collections.Generic;

namespace CompanionApp.Models
{
    public partial class Profile
    {
        public Profile()
        {
            CourseTakenBies = new HashSet<CourseTakenBy>();
            FollowingIsFollowingNavigations = new HashSet<Following>();
            FollowingUsers = new HashSet<Following>();
            Posts = new HashSet<Post>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Major { get; set; }
        public string? Class { get; set; }

        public virtual ICollection<CourseTakenBy> CourseTakenBies { get; set; }
        public virtual ICollection<Following> FollowingIsFollowingNavigations { get; set; }
        public virtual ICollection<Following> FollowingUsers { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
