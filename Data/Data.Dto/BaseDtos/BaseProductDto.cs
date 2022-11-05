using System.Runtime.CompilerServices;

namespace Data.Dto.BaseDtos
{
    public class BaseProductDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public bool IsAgeRestricted { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public bool IsCountable { get; set; }
    }
}