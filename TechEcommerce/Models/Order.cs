namespace TechEcommerce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.UI;

    [Table("Order")]
    public partial class Order
    {
        [Key]
        public int IDOrder { get; set; }

        public int IdProducts { get; set; }

        public int Quantity { get; set; }

        public int IdUtent { get; set; }

        public virtual Products Products { get; set; }

        public virtual Utents Utents { get; set; }
    }
}
