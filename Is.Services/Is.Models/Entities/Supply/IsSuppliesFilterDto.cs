namespace Is.Models.Entities.Supply
{
    public class IsSuppliesFilterDto
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
    }
}