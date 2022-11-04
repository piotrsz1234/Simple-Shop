using System;
using Data.EF.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Data.EF.Entities
{
    public class Role : IdentityRole<long>, IEntityBase
    {
        public DateTime InsertDateUTC { get; set; }
        public DateTime ModificationDateUTC { get; set; }
        public bool IsDeleted { get; set; }
    }
}