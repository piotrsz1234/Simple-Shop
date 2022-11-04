﻿using System;
using Data.EF.Entities.Interfaces;

namespace Data.EF.Entities
{
    public class SaleProduct : EntityBase
    {
        public long Id { get; set; }
        public DateTime InsertDateUTC { get; set; }
        public DateTime ModificationDateUTC { get; set; }
        public bool IsDeleted { get; set; }
        public long SaleId { get; set; }
        public long ProductId { get; set; }
        
        public virtual Sale Sale { get; set; }
        public virtual Product Product { get; set; }
    }
}