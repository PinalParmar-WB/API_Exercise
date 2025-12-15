namespace API_Exercise.DTO
{
    public class RegisterRequestDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }

}
