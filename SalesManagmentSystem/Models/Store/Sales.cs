namespace SalesManagmentSystem.Models.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sales()
        {
            SaleItems = new HashSet<SaleItems>();
        }

        [Key]
        public int SaleID { get; set; }

        public int CustomerID { get; set; }

        public int StoreID { get; set; }

        public int EmployeeID { get; set; }

        public DateTime SaleDate { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual Employees Employees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleItems> SaleItems { get; set; }

        public virtual Stores Stores { get; set; }
    }
}
