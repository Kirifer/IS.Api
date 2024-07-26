namespace Is.Models.Entities.Supply
{
    public class IsSuppliesCreateDto
    {
        public string Category { get; set; }
        public string Item { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public int SuppliesTaken { get; set; }
        public int SuppliesLeft { get; set; }
        public decimal CostPerUnit { get; set; }
        public decimal Total { get; set; }
    }
}