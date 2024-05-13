using BirdFood.Contract.DTOs.BaseDTO;
using System.Collections.ObjectModel;

namespace BirdFood.Contract.DTOs.ComboDTO
{
    public class ComboDTO : Base, IComboDTO
    {
        public string Name { get; set; }
        public string Thumb { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Collection<FoodComboDTO> FoodCombos { get; set; }
    }
    public class FoodComboDTO
    {
        public Guid Food_id { get; set; }
        public int TotalFood { get; set; }
        public FoodDTO.FoodDTO Food { get; set; }
    }
}
