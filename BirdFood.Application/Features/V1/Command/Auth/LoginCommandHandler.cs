using BirdFood.Application.Abstractions;
using BirdFood.Application.Data;
using BirdFood.Application.Utils;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Service.Auth;
using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Exceptions.Auth;
using System.Security.Claims;
using static BirdFood.Contract.Service.Auth.Command;

namespace BirdFood.Application.Features.V1.Command.Auth
{
    public class LoginCommandHandler : ICommandHandler<Login, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenUsedRepository _tokenUsedRepository;

        public LoginCommandHandler(
            IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService,
            ITokenRepository tokenRepository, ITokenUsedRepository tokenUsedRepository,
            IAccountRepository accountRepository) 
            =>
            (_unitOfWork, _jwtTokenService, _accountRepository, _tokenRepository, _tokenUsedRepository) 
            = (unitOfWork, jwtTokenService, accountRepository, tokenRepository, tokenUsedRepository);

        public async Task<Result<Response>> Handle(Login request, CancellationToken cancellationToken)
        {
            var hashPassword = new HashPassword();
            var userCheck = await _accountRepository.FindSingleAsync(x => x.Username == request.LoginDTO.UserName);
            if (userCheck == null) throw new AuthNotFound(request.LoginDTO.UserName);
            var isValid = hashPassword.Verify(request.LoginDTO.Password, userCheck.Password);
            if (isValid == false) throw new AuthBadRequest();
            var tokenCheck = await _tokenRepository.FindSingleAsync(p => p.Account_id == userCheck.Id);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userCheck.Id.ToString()),
                    new Claim(ClaimTypes.Role, userCheck.Role.ToString()),
                };
            var accessToken = _jwtTokenService.GenerateAccessToken(claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();
            if (tokenCheck == null)
            {
                
                var token = new Domain.Entities.Token();
                token.RefreshToken = refreshToken;
                token.PublicKey = "avc";
                token.Account_id = userCheck.Id;
                _tokenRepository.Add(token);
            }
            else
            {
                var token = new Domain.Entities.TokenUsed();
                token.TokenValue = tokenCheck.RefreshToken;
                token.TokenId = tokenCheck.Id;
                _tokenUsedRepository.Add(token);
            }
            var response = new Response();
            await _unitOfWork.SaveChangesAsync();
            response.AccessToken = accessToken;
            response.RefreshToken = refreshToken;
            return response;
        }
    }
}
