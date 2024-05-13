using AutoMapper;
using BirdFood.Application.Data;
using BirdFood.Contract.Abstraction.Message;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.DTOs.ComboDTO;
using BirdFood.Contract.Service;
using BirdFood.Domain.Abstraction.Repositories;
using static BirdFood.Contract.Service.Combo.Command;

namespace BirdFood.Application.Features.V1.Command.Combo
{
    public class CreateComboCommandHandler : ICommandHandler<CreateCombo, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IComboRepository _comboRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public CreateComboCommandHandler(IUnitOfWork unitOfWork, IComboRepository comboRepository, IFoodRepository foodRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _comboRepository = comboRepository;
            _foodRepository = foodRepository;
            _mapper = mapper;
        }
        public async Task<Result<BaseResponse>> Handle(CreateCombo request, CancellationToken cancellationToken)
        {
            await _foodRepository.CheckFoodsExistById(
                foods: request.CreateComboDTO.FoodCombos, 
                idSelector: food => food.Food_id);

            var combo = _mapper.Map<Domain.Entities.Combo>(request.CreateComboDTO);
            _comboRepository.Add(combo);
            
            await _unitOfWork.SaveChangesAsync();
            
            await Create_Food_Combo(request.CreateComboDTO, combo.Id);
            
            return BaseResponse.BuildSuccessResponse("Create Combo Successfully!");
        }

        private async Task Create_Food_Combo(CreateComboDTO CreateComboDTO, Guid Combo_id)
        {
            foreach(var foodCombo in CreateComboDTO.FoodCombos)
            {
                _unitOfWork.GetRepository<Domain.Entities.FoodCombo, Guid>()
                    .Add(new Domain.Entities.FoodCombo 
                    { 
                        Combo_id = Combo_id,
                        Food_id = foodCombo.Food_id,
                        TotalFood = foodCombo.TotalFood 
                    });
            }
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
