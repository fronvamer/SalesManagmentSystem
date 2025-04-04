namespace SalesManagmentSystem.Models.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SaleItems
    {
        [Key]
        public int SaleItemID { get; set; }

        public int SaleID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public decimal SalePrice { get; set; }

        public virtual Products Products { get; set; }

        public virtual Sales Sales { get; set; }
    }
}
