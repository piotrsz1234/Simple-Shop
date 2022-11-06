using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Bll.Helpers;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Common.Bll.Services
{
    public class SaleService : ServiceBase, ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleProductRepository _saleProductRepository;
        private readonly IProductRepository _productRepository;

        public SaleService(ILogger logger, IMapper mapper, ISaleRepository saleRepository,
            ISaleProductRepository saleProductRepository, IProductRepository productRepository) : base(logger, mapper)
        {
            _saleRepository = saleRepository;
            _saleProductRepository = saleProductRepository;
            _productRepository = productRepository;
        }

        public async Task<SaleDto> GetSaleInfoAsync(long id)
        {
            try
            {
                var sale = await _saleRepository.GetOneAsync(id, x => x.SaleProduct, x => x.SalesmanUser);
                var productIds = sale.SaleProduct.Select(x => x.ProductId).ToArray();
                var products = await _productRepository.GetAllAsync(x => productIds.Contains(x.ID));
                var result = Mapper.Map<SaleDto>(sale);
                result.Items = sale.SaleProduct.Where(x => x.IsDeleted == false).Select(x => new SaleItemDto()
                {
                    Count = x.Count,
                    Product = Mapper.Map<ProductDto>(products.First(y => y.Id == x.ProductId))
                }).ToArray();

                return result;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return null;
            }
        }
    }
}