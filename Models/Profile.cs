using System;
using System.Collections.Generic;

namespace CompanionApp.Models
{
    public partial class Profile
    {
        public Profile()
        {
            FollowingUserFollowings = new HashSet<Following>();
        }

        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Major { get; set; }
        public string? Class { get; set; }

        public virtual CourseTakenBy CourseTakenBy { get; set; } = null!;
        public virtual Following FollowingUser { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
        public virtual ICollection<Following> FollowingUserFollowings { get; set; }
    }
}
