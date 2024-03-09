using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos.UserRequest;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface IUserInterface
    {
        Task<IEnumerable<UserDTO>> GetUserByToken(string userToken, CancellationToken cancellationToken);
        Task<UserProfileDTO> GetUserById(string userName, CancellationToken cancellationToken);
        Task<string> CreateUser(CreateNewUserRequest registerRequest, CancellationToken cancellationToken);
        Task<UserDTO> Login(UserLoginRequest loginRequest, CancellationToken cancellationToken);
        Task<UserDTO> ResetUser(string userToken, CancellationToken cancellationToken);
        Task<IEnumerable<UserDTO>> SearchUser(string param, CancellationToken cancellationToken);
    }
}
