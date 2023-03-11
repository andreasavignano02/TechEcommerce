namespace TechEcommerce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using WebGrease.Css.Ast.Selectors;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Order = new HashSet<Order>();
        }

        [Key]
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

        public List<Products> GetIdG()
        {
            ModelDbContext dbContext = new ModelDbContext();
            List<Products> list = dbContext.Products.Where(p => p.IdTypeProducts == 2 || p.IdTypeProducts == 4 || p.IdTypeProducts == 16 || p.IdTypeProducts == 6).ToList();
            return list;
        }

        public List<Products> GetIdS()
        {
            ModelDbContext dbContext = new ModelDbContext();
            List<Products> list = dbContext.Products.Where(p => p.IdTypeProducts == 7 || p.IdTypeProducts == 4).ToList();
            return list;
        }

        public List<Products> GetIdC()
        {
            ModelDbContext dbContext = new ModelDbContext();
            List<Products> list = dbContext.Products.Where(p => p.IdTypeProducts == 1 || p.IdTypeProducts == 8 || p.IdTypeProducts == 11 || p.IdTypeProducts == 15 || p.IdTypeProducts == 14 || p.IdTypeProducts == 13 || p.IdTypeProducts == 9 || p.IdTypeProducts == 10 || p.IdTypeProducts == 11 || p.IdTypeProducts == 12).ToList();
            return list;
        }

    }
}
