namespace Core.Domain.Entities
{
    public class FoodAndDrinkOption
    {
        public int Id { get; set; }
        public bool LactoseFree { get; set; }
        public bool NutFree { get; set; }
        public bool Vegetarian { get; set; }
        public bool NonAlcoholic { get; set; }
    }
}

