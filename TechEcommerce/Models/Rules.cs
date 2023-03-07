namespace TechEcommerce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rules
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rules()
        {
            Utents = new HashSet<Utents>();
        }

        [Key]
        public int IDRules { get; set; }

        [Required]
        [StringLength(15)]
        public string Rule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Utents> Utents { get; set; }

        public static implicit operator string(Rules v)
        {
            throw new NotImplementedException();
        }
    }
}
