using System;
using Data.EF.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Data.EF.Entities
{
    public class User : IEntityBase
    {
        public long Id { get; set; }
        public DateTime InsertDateUTC { get; set; }
        public DateTime ModificationDateUTC { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [StringLength(10)]
        [Required]
        public string LoginCode { get; set; }
        public bool IsAdmin { get; set; }
    }
}