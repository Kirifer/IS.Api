using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Is.Models.Entities.SupplyCodes
{
    public class IsSupplyCodesFilterDto
    {
        public string Code { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
    }
}
