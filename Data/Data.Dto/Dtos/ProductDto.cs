using Data.Dto.BaseDtos;

namespace Data.Dto.Dtos
{
    public sealed class ProductDto : BaseProductDto
    {
        public new long Id { get; set; }
    }
}