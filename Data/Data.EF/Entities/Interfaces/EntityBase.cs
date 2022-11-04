using System;
using System.ComponentModel.DataAnnotations;

namespace Data.EF.Entities.Interfaces
{
    public interface EntityBase
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public DateTime InsertDateUTC { get; set; }
        [Required]
        public DateTime ModificationDateUTC { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}