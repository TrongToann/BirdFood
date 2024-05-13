using BirdFood.Application.Abstractions;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Service;
using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Exceptions.Auth;
using static BirdFood.Contract.Service.Auth.Command;

namespace BirdFood.Application.Features.V1.Command.Auth
{
    public class LogoutCommandHandler : ICommandHandler<Logout, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenUsedRepository _tokenUsedRepository;

        public LogoutCommandHandler(IUnitOfWork unitOfWork, ITokenRepository tokenRepository, ITokenUsedRepository tokenUsedRepository)
        {
            _unitOfWork = unitOfWork;
            _tokenRepository = tokenRepository;
            _tokenUsedRepository = tokenUsedRepository;
        }

        public async Task<Result<BaseResponse>> Handle(Logout request, CancellationToken cancellationToken)
        {
            var token = await _tokenRepository.FindSingleAsync(x => x.Account_id == request.Account_id);
            if (token == null) throw new AuthBadRequest();
            List<Domain.Entities.TokenUsed> tokens = _tokenUsedRepository
                .GetAll(x => x.TokenId == token.Id).ToList();
            _tokenUsedRepository.RemoveMultiple(tokens);
            _tokenRepository.Remove(token);
            await _unitOfWork.SaveChangesAsync();
            var response = new BaseResponse();
            response.Success = true;
            response.Message = "Logout Successfully!";
            response.Errors = [];
            return response;
        }
    }
}
