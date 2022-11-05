using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.Bll.Services.Interfaces;
using Data.Dto.Models;
using Data.EF.Entities;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Common.Bll.Helpers;
using Data.Dto.Dtos;

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

        public async Task<bool> RemoveProductAsync(long productId)
        {
            try
            {
                var product = await _productRepository.GetOneAsync(productId);
                if (product is null)
                {
                    return false;
                }

                product.IsDeleted = true;
                product.ModificationDateUTC = DateTime.UtcNow;

                await _productRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return false;
            }
        }

        public async Task<ProductDto?> GetOneAsync(long id)
        {
            try
            {
                var product = await _productRepository.GetOneAsync(id);

                return Mapper.Map<ProductDto>(product);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return null;
            }
        }

        public async Task<IReadOnlyCollection<ProductDto>?> SearchAsync(BrowseProductsModel model)
        {
            try
            {
                var products = await _productRepository.SearchAsync(model.Text, model.Offset, model.Length);

                return Mapper.Map<IReadOnlyCollection<ProductDto>>(products);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return null;
            }
        }
    }
}