using ErpServer.Application.Features.Auth.Login;
using ErpServer.Domain.Entities;

namespace ErpServer.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(AppUser user);
    }
}
