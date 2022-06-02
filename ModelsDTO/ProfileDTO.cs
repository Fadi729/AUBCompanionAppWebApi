namespace CompanionApp.ModelsDTO
{
    public class ProfileDTO
    {
        public Guid    Id        { get; set; }
        public string  FirstName { get; set; } = null!;
        public string  LastName  { get; set; } = null!;
        public string  Email     { get; set; } = null!;
        public string? Major     { get; set; }
        public string? Class     { get; set; }
    }
    public class ProfileDTOPOST
    {
        public string  FirstName { get; set; } = null!;
        public string  LastName  { get; set; } = null!;
        public string  Email     { get; set; } = null!;
        public string? Major     { get; set; }
        public string? Class     { get; set; }
    }
    public class ProfileDTOPUT
    {
        public string  FirstName { get; set; } = null!;
        public string  LastName  { get; set; } = null!;
        public string  Email     { get; set; } = null!;
        public string? Major     { get; set; }
        public string? Class     { get; set; }
    }
}
