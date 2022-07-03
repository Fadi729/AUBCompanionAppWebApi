namespace CompanionApp.Models
{
    public class AuthResponse
    {
        public string               Token         { get; set; }
        public bool                 IsSuccessful  { get; set; }
        public IEnumerable<string>? ErrorMessages { get; set; }
    }
}
