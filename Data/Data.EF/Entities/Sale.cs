using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.EF.Entities.Interfaces;
using Data.EF.Enums;

namespace Data.EF.Entities
{
    public class Sale : IEntityBase
    {
        public long Id { get; set; }
        public DateTime InsertDateUTC { get; set; }
        public DateTime ModificationDateUTC { get; set; }
        public bool IsDeleted { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        public long SalesmanUserId { get; set; }
        
        public virtual User SalesmanUser { get; set; }
        public virtual ICollection<SaleProduct> SaleProduct { get; set; }

        public Sale()
        {
            SaleProduct = new HashSet<SaleProduct>();
        }
    }
}