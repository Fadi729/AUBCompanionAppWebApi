using System;
using System.Collections.Generic;

namespace CompanionApp.Models
{
    public partial class Following
    {
        public Guid UserId { get; set; }
        public Guid UserFollowingid { get; set; }

        public virtual Profile User { get; set; } = null!;
        public virtual Profile UserFollowing { get; set; } = null!;
    }
}
