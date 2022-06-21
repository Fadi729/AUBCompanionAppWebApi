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
        public virtual ProfileQueryDTO User      { get; set; }
        public DateTime                DateLiked { get; set; }
    }
    public class LikePOSTDTO
    {
        public string PostId { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
