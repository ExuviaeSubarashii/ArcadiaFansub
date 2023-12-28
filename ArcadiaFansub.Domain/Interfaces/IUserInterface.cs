using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos.UserRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface IUserInterface
    {
        Task<IEnumerable<UserDTO>> GetUserByToken(string userToken);
        Task<IEnumerable<UserDTO>> GetUserById(int userId);
        Task<string> CreateUser(CreateNewUserRequest registerRequest);
        Task<UserDTO> Login(UserLoginRequest loginRequest);
    }
}
