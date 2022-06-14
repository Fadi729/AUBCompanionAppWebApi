namespace CompanionApp.ModelsDTO
{
    public class ProfileQuerryDTO
    {
        public Guid    Id        { get; set; }
        public string  FirstName { get; set; } = null!;
        public string  LastName  { get; set; } = null!;
        public string  Email     { get; set; } = null!;
        public string? Major     { get; set; }
        public string? Class     { get; set; }
    }
    public class ProfileCommandDTO
    {
        public string  FirstName { get; set; } = null!;
        public string  LastName  { get; set; } = null!;
        public string  Email     { get; set; } = null!;
        public string? Major     { get; set; }
        public string? Class     { get; set; }
    }
}
