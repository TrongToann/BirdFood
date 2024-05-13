using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Application.Utils;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.Service;
using BirdFood.Domain.Abstraction.Repositories;
using BirdFood.Domain.Exceptions.Auth;
using static BirdFood.Contract.Service.Auth.Command;

namespace BirdFood.Application.Features.V1.Command.Auth
{
    public class RegisterCommandeHandler : ICommandHandler<Register, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public RegisterCommandeHandler(IUnitOfWork unitOfWork, IMapper mappper, IAccountRepository accountRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mappper;
            _accountRepository = accountRepository;
        }
        public async Task<Result<BaseResponse>> Handle(Register request, CancellationToken cancellationToken)
        {
            if(request.RegisterDTO.Password != request.RegisterDTO.ConfirmPassword)
                throw new AuthBadRequest();
            var hashPassword = new HashPassword();
            var userCheck = await _accountRepository
                .FindSingleAsync(x => x.Username == request.RegisterDTO.Username);
            if (userCheck != null) throw new AuthBadRequest();
            var user = _mapper.Map<Domain.Entities.Account>(request.RegisterDTO);
            user.Role = 30;
            user.Password = hashPassword.Hash(request.RegisterDTO.Password);
            _accountRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
            var respone = new BaseResponse();
            respone.Success = true;
            respone.Message = "Register Successfully!";
            respone.Errors = [];
            return respone;
        }
    }
}
