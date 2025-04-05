namespace SalesManagmentSystem.Models.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Warehouses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Warehouses()
        {
            Inventory = new HashSet<Inventory>();
        }

        [Key]
        public int WarehouseID { get; set; }

        public int StoreID { get; set; }

        public int ProductID { get; set; }

        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventory { get; set; }

        public virtual Products Products { get; set; }

        public virtual Stores Stores { get; set; }
    }
}
