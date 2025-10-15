using Application.Interfaces;
using Application.Models.RequestDTO;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthenticationService(IUserRepository userRepository, IConfiguration config)
        {
            _config = config;
            _userRepository = userRepository;
        }
        private async Task<User?> ValidateUser(string email, string password)
        {
            User? user = await _userRepository.GetByEmail(email);

            if (user is null)
            {
                return null;
            }

            if (user.HashedPassword != password)
            {
                return null;
            }

            return user;
        }

        public async Task<string> Authenticate(AuthenticationCredentialsDTO authenticationCredentialsDTO)
        {
            var validatedUser = await ValidateUser(authenticationCredentialsDTO.Email, authenticationCredentialsDTO.Password);

            if (validatedUser is null)
            {
                throw new Exception("Credenciales inválidas");
            }

            // luego comenzamos el proceso de creación del token

            var securityPassword = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));

            var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", validatedUser.Id.ToString()));
            claimsForToken.Add(new Claim("role", validatedUser.Role.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(15),
                signature
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();
        }
    }
}
