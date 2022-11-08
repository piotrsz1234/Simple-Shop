using System.Collections.Generic;
using Common.Bll.Services.Enums;
using Data.Dto.Models;

namespace Common.Bll.Services.Interfaces
{
    public interface ISaleService
    {
        SaveSaleResult SaveSale(IReadOnlyCollection<AddSaleProductModel> models, long userId);
    }
}