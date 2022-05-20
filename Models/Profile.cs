using System;
using System.Collections.Generic;

namespace CompanionApp.Models
{
    public partial class Profile
    {
        public Profile()
        {
            CourseTakenBies = new HashSet<CourseTakenBy>();
            Posts = new HashSet<Post>();
            UserFollowings = new HashSet<Profile>();
            Users = new HashSet<Profile>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Major { get; set; }
        public string? Class { get; set; }

        public virtual ICollection<CourseTakenBy> CourseTakenBies { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Profile> UserFollowings { get; set; }
        public virtual ICollection<Profile> Users { get; set; }
    }
}
