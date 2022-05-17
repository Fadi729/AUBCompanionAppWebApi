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
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Major { get; set; } = null!;
        public string Class { get; set; } = null!;

        public virtual CourseTakenBy CourseTakenBy { get; set; } = null!;
        public virtual Following FollowingUser { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
        public virtual ICollection<Following> FollowingUserFollowings { get; set; }
    }
}
