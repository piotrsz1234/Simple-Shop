using System;
using System.Threading.Tasks;
using AutoMapper;
using Common.Bll.Services.Interfaces;
using Data.Dto.Models;
using Data.EF.Entities;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Common.Bll.Services
{
    internal class ProductService : ServiceBase, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(ILogger<ProductService> logger, IMapper mapper, IProductRepository productRepository) :
            base(logger, mapper)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> AddEditProductAsync(AddEditProductModel model)
        {
            try
            {
                if (model.Id.HasValue)
                {
                    var alreadyExisting = await _productRepository.GetOneAsync(model.Id.Value);
                    if (alreadyExisting != null)
                    {
                        alreadyExisting.IsDeleted = true;
                        alreadyExisting.ModificationDateUTC = DateTime.UtcNow;
                    }
                }

                var newEntity = Mapper.Map<Product>(model);
                await _productRepository.AddAsync(newEntity);
                await _productRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                return false;
            }
        }
    }
}