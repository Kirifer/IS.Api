﻿using Is.Core.Database.Abstraction;
using Is.Datalayer.Interface;
using System.Drawing;

namespace Is.Datalayer.Entities
{
    public class SupplyCodes : DbEntityIdBase
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int SequenceCode { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public bool SupplyTaken { get; set; }
        //public string CodeDisplay { get; set; }
        public Guid UserId { get; set; }

    }
}