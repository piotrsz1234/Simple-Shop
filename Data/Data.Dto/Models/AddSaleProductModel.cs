namespace Data.Dto.Models
{
    public class AddSaleProductModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public bool IsAgeRestricted { get; set; }
        public decimal Count { get; set; }
        public decimal PricePerOne { get; set; }
    }
}