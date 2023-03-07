namespace TechEcommerce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDProduct { get; set; }

        [Required]
        public string NameProducts { get; set; }

        [Required]
        [StringLength(10)]
        public string CodeProducts { get; set; }

        [Required]
        public string ImgProducts { get; set; }

        [Column(TypeName = "money")]
        public decimal Cost { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExitDate { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        public int IdTypeProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }

        public virtual TypeProducts TypeProducts { get; set; }
    }
}
