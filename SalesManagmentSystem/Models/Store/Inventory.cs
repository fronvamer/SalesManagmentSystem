namespace SalesManagmentSystem.Models.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inventory")]
    public partial class Inventory
    {
        public int InventoryID { get; set; }

        public int WarehouseID { get; set; }

        public int ProductID { get; set; }

        public int RecordedQuantity { get; set; }

        public int ActualQuantity { get; set; }

        public int Difference { get; set; }

        public virtual Products Products { get; set; }

        public virtual Warehouses Warehouses { get; set; }
    }
}
