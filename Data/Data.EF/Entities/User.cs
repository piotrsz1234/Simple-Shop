using System;
using Data.EF.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Data.EF.Entities
{
    public class User : IdentityUser<long>, EntityBase
    {
        public DateTime InsertDateUTC { get; set; }
        public DateTime ModificationDateUTC { get; set; }
        public bool IsDeleted { get; set; }
    }
}