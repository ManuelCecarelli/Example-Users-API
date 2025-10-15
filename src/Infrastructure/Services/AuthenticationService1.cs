using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

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
            User? user = await _userRepository.GetUserByEmail(email);

            if (user is null)
            { 
                return null;
            }

            if (user.Password != password)
            {
                return null;
            }

            return user;
        }

        public async Task<string> Authenticate(AuthenticationRequestDTO authenticationRequestDTO)
        {
            // primero validamos las credenciales
            var validatedUser = await ValidateUser(authenticationRequestDTO.Email, authenticationRequestDTO.Password);

            if (validatedUser is null)
            {
                throw new InvalidCredentialsException("Credenciales inválidas");
            }

            // luego comenzamos el proceso de creación del token

            var securityPassword = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));

            var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", validatedUser.Id.ToString()));
            claimsForToken.Add(new Claim("role", validatedUser.Rol.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(1),
                signature
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();
        }
    }
}
