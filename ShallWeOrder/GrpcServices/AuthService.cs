using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ShallWeOrder.DBService;
using ShallWeOrder.Models;

namespace ShallWeOrder.GrpcService
{
    [Authorize]
    public class AuthService : Auther.AutherBase
    {
        private readonly ILogger<AuthService> _logger;
        private IConfiguration _configuration;
        private UserDBService _userDBService;

        public AuthService(
            ILogger<AuthService> logger,
            IConfiguration configuration,
            UserDBService userDBService)
        {
            _logger = logger;
            _configuration = configuration;
            _userDBService = userDBService;
        }

        [AllowAnonymous]
        public override Task<SignUpReply> SignUp(SignUpRequest request, ServerCallContext context)
        {

            var sha = new SHA512Managed();
            var hashedPassword = Convert.ToBase64String(
                sha.ComputeHash(Encoding.ASCII.GetBytes(request.Password)));

            _logger.LogInformation("Sign up request : {Id} / {Password} / {Gender}", request.Id, hashedPassword, request.Gender);

            // TODO : password hashing
            User user = new User()
            {
                UserId = request.Id,
                Password = hashedPassword,
                Gender = (int)request.Gender,
                Registerday = DateTime.Now,
            };

            _userDBService.Create(user);

            // TODO : change reply to empty?
            return Task.FromResult(new SignUpReply
            {
                Result = 200,
            });
        }

        [AllowAnonymous]
        public override Task<SignInReply> SignIn(SignInRequest request, ServerCallContext context)
        {
            var sha = new SHA512Managed();
            var hashedPassword = Convert.ToBase64String(
                sha.ComputeHash(Encoding.ASCII.GetBytes(request.Password)));

            _logger.LogInformation("Sign in request : {Id} / {Password} ", request.Id, hashedPassword);
            var user = _userDBService.Get(request.Id, hashedPassword);
            if (user == null)
            {
                return Task.FromResult(new SignInReply
                {
                    Result = 503, // (?)
                    AccessToken = "",
                    RefreshToken = "",
                });
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            };

            return Task.FromResult(new SignInReply
            {
                Result = 200,
                AccessToken = GenerateToken(claims, DateTime.UtcNow.AddMinutes(30)),
                RefreshToken = GenerateToken(claims, DateTime.UtcNow.AddDays(30)),
            });
        }

        public override Task<SignOutReply> SignOut(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("Sign out request");
            return Task.FromResult(new SignOutReply
            {
                Result = 200,
            });
        }

        private string GenerateToken(Claim[] claims, DateTime expires)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Auth:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}