using API_Exercise.DTO;
using API_Exercise.Models;

namespace API_Exercise.ServiceContract
{
    public interface IJwtTokenService
    {
        LoginResponseDTO GenerateToken(User user);
    }
}
