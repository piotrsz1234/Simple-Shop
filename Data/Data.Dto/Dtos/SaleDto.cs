using System;
using System.Collections.Generic;

namespace Data.Dto.Dtos
{
    public class SaleDto
    {
        public string SalesmanName { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalPrice { get; set; }
        public IReadOnlyCollection<SaleItemDto> Items { get; set; }
    }

    public class SaleItemDto
    {
        public decimal Count { get; set; }
        public ProductDto Product { get; set; }
    }
}