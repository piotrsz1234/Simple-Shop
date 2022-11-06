using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.EF.Entities.Interfaces;

namespace Data.EF.Entities
{
    public class Product : IEntityBase
    {
        [Required]
        [Key]
        public long Id { get; set; }

        public DateTime InsertDateUTC { get; set; }
        public DateTime ModificationDateUTC { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsAgeRestricted { get; set; }
        [Required]
        public string Barcode { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsCountable { get; set; }
        [Required]
        public decimal VATPercentage { get; set; }
        
        public virtual ICollection<SaleProduct> SaleProduct { get; set; }

        public Product()
        {
            SaleProduct = new HashSet<SaleProduct>();
        }
    }
}