﻿namespace CompanionApp.ModelsDTO
{
    public class FollowingDTO
    {
        public DateTime?          DateFollowed          { get; set; }
        public virtual ProfileQuerryDTO IsFollowingNavigation { get; set; } = null!;
    }
    public class FollowingPOSTDTO
    {
        public Guid UserId      { get; set; }
        public Guid IsFollowing { get; set; }
    }
}
