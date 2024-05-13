using System.Collections.ObjectModel;

namespace BirdFood.Contract.DTOs.ComboDTO
{
    public interface IComboDTO
    {
        public string Name { get; set; }
        public string Thumb { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Collection<FoodComboDTO> FoodCombos { get; set; }
    }
}
