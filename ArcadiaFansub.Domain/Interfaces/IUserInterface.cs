using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos.UserRequest;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface IUserInterface
    {
        Task<IEnumerable<UserDto>> GetUserByToken(string userToken, CancellationToken cancellationToken);
        Task<UserProfileDto> GetUserById(string userName, CancellationToken cancellationToken);
        Task<string> CreateUser(CreateNewUserRequest registerRequest, CancellationToken cancellationToken);
        Task<UserDto> Login(UserLoginRequest loginRequest, CancellationToken cancellationToken);
        Task<UserDto> ResetUser(string userToken, CancellationToken cancellationToken);
        Task<IEnumerable<UserDto>> SearchUser(string param, CancellationToken cancellationToken);
    }
}
