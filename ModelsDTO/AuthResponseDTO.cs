namespace CompanionApp.ModelsDTO
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
    }
    public class AuthFailResponse
    {
        public IEnumerable<string>? ErrorMessages { get; set; }
    }
}
