using System;

namespace Data.Dto.Models
{
    [Serializable]
    public sealed class AddEditProductModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public bool IsAgeRestricted { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public bool IsCountable { get; set; }
    }
}