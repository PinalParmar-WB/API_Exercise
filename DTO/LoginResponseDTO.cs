namespace API_Exercise.DTO
{
    public class LoginResponseDTO
    {
        public string Email { get; set; }
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }

}
