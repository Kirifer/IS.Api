namespace Is.Models.Entities.Supply
{
    public class IsSuppliesUpdateDto
    {
        public string Category { get; set; }
        public string Item { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public int SuppliesTaken { get; set; }
        public decimal CostPerUnit { get; set; }
    }
}
