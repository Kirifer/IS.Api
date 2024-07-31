
namespace Is.Models.Entities.SupplyCodes
{
    public class IsSupplyCodesCreateDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }


    }
}
