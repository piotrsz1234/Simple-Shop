using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Bll.Services.Enums;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.Dto.Models;
using Data.EF.Entities;
using Data.EF.Enums;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Common.Bll.Services
{
    internal class SaleService : ServiceBase, ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleProductRepository _saleProductRepository;
        private readonly IProductRepository _productRepository;

        public SaleService(IMapper mapper, ISaleRepository saleRepository,
            ISaleProductRepository saleProductRepository, IProductRepository productRepository) : base(mapper)
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
                var products = await _productRepository.GetAllAsync(x => productIds.Contains(x.Id));
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
                return null;
            }
        }

        public SaveSaleResult SaveSale(IReadOnlyCollection<AddSaleProductModel> models, long userId)
        {
            try
            {
                var sale = new Sale()
                {
                    PaymentMethod = PaymentMethod.Cash,
                    SalesmanUserId = userId
                };
                
                _saleRepository.Add(sale);
                _saleRepository.SaveChanges();

                foreach (var model in models)
                {
                    _saleProductRepository.Add(new SaleProduct()
                    {
                        Count = model.Count,
                        ProductId = model.ProductId,
                        SaleId = sale.Id
                    });
                }
                
                _saleProductRepository.SaveChanges();

                return SaveSaleResult.Ok;
            }
            catch (Exception e)
            {
                return SaveSaleResult.Error;
            }
        }
    }
}