using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace ShallWeOrder
{
    public class AuthService : Auther.AutherBase
    {
        private readonly ILogger<AuthService> _logger;
        public AuthService(ILogger<AuthService> logger)
        {
            _logger = logger;
        }

        public override Task<SignUpReply> SignUp(SignUpRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Sign up request : ", request.Id, request.Password, request.Gender);
            return Task.FromResult(new SignUpReply
            {
                Result = 200,
            });
        }

        public override Task<SignInReply> SignIn(SignInRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Sign in request : ", request.Id, request.Password);
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