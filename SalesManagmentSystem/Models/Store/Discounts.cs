namespace SalesManagmentSystem.Models.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Discounts
    {
        [Key]
        public int DiscountID { get; set; }

        public decimal ThresholdAmount { get; set; }

        public decimal DiscountPercentage { get; set; }
    }
}
