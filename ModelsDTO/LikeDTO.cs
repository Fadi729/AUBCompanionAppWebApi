namespace CompanionApp.ModelsDTO
{
    public class LikeDTO
    {
        public Guid     PostId    { get; set; }
        public Guid     UserId    { get; set; }
        public DateTime DateLiked { get; set; }
    }
    public class LikeDTOUsers
    {
        public virtual ProfileQuerryDTO User      { get; set; }
        public DateTime           DateLiked { get; set; }
    }
    public class LikeDTOwObjects
    {
        public virtual PostDTO    Post      { get; set; } = null!;
        public virtual ProfileQuerryDTO User      { get; set; } = null!;
        public DateTime           DateLiked { get; set; }
    }
    public class LikePOSTDTO
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
