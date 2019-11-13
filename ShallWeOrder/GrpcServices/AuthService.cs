using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ShallWeOrder.DBService;
using ShallWeOrder.Models;

namespace ShallWeOrder.GrpcService
{
    public class AuthService : Auther.AutherBase
    {
        private readonly ILogger<AuthService> _logger;
        private UserDBService _userDBService;

        public AuthService(ILogger<AuthService> logger, UserDBService userDBService)
        {
            _logger = logger;
            _userDBService = userDBService;
        }

        public override Task<SignUpReply> SignUp(SignUpRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Sign up request : {Id} / {Password} / {Gender}", request.Id, request.Password, request.Gender);

            // TODO : password hashing
            User user = new User()
            {
                UserId = request.Id,
                Password = request.Password,
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

        public override Task<SignInReply> SignIn(SignInRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Sign in request : {Id} / {Password} ", request.Id, request.Password);
            return Task.FromResult(new SignInReply
            {
                Result = 200,
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
    }
}