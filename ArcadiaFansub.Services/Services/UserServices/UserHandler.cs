using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.UserRequest;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.UserServices
{
    public class UserHandler(ArcadiaFansubContext AF) : IUserInterface
    {
        public async Task<string> CreateUser(CreateNewUserRequest registerRequest, CancellationToken cancellationToken)
        {
            var doesUserExist=await AF.Users.Where(x=>x.UserEmail == registerRequest.UserEmail||x.UserName==registerRequest.UserName).FirstOrDefaultAsync();
            if (doesUserExist!=null)
            {
                return "User Already Exists";
            }
            User newUser = new()
            {
                UserName = registerRequest.UserName,
                FavoritedAnimes="",
                UserEmail = registerRequest.UserEmail,
                UserPassword=registerRequest.UserPassword,
                UserPermission="User",
                UserToken=CreateRegisterToken(registerRequest.UserName,registerRequest.UserEmail,registerRequest.UserPassword),
            };
            AF.Users.Add(newUser);
            AF.SaveChanges();
            return "Succesfully Registered";
        }
        public async Task<UserProfileDTO> GetUserById(string userName, CancellationToken cancellationToken)
        {
            var userQuery = await AF.Users.Where(x => x.UserName == userName).Select(item => new UserProfileDTO
            {
                FavoritedAnimes = item.FavoritedAnimes.Trim(),
                UserId = item.UserId,
                UserName = item.UserName.Trim(),
                UserPermission = item.UserPermission.Trim(),
                UserEmail = item.UserEmail.Trim()
            }).FirstOrDefaultAsync(cancellationToken);
            if (userQuery != null)
            {
                return userQuery;
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<UserDTO>> GetUserByToken(string userToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> Login(UserLoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var userLoginQuery = await AF.Users.Where(x => x.UserEmail == loginRequest.UserEmail && x.UserPassword == loginRequest.Password).Select(item => new UserDTO
            {
                FavoritedAnimes = item.FavoritedAnimes.Trim(),
                UserId = item.UserId,
                UserName = item.UserName.Trim(),
                UserToken = item.UserToken.Trim(),
                UserPermission=item.UserPermission.Trim(),
                UserEmail=item.UserEmail.Trim()
            }).FirstOrDefaultAsync(cancellationToken);
            if (userLoginQuery != null)
            {
            }
            return userLoginQuery;
        }
        public string CreateRegisterToken(string userName, string userEmail, string password)
        {
            List<Claim> claims = new List<Claim>();
            {
                _ = new Claim(ClaimTypes.Name, userName, userEmail, password);
            }
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("BMNIGKHITKGSGITRPJKGJMEISNF"));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwttoken;
        }
    }
}
