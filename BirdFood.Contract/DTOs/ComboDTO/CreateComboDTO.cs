namespace BirdFood.Contract.DTOs.ComboDTO
{
    public class CreateComboDTO 
    {
        public string Name { get; set; }
        public string Thumb { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<InputFoodCombo> FoodCombos { get; set; }
    }
    public class InputFoodCombo
    {
        public Guid Food_id { get; set; }
        public int TotalFood { get; set; }
    }
}
