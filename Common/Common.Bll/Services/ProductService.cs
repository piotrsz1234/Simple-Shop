using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.Bll.Services.Interfaces;
using Data.Dto.Models;
using Data.EF.Entities;
using Data.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Data.Dto.Dtos;

namespace Common.Bll.Services
{
    internal class ProductService : ServiceBase, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository) :
            base(mapper)
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
                return null;
            }
        }
        
        public async Task<ProductDto?> GetOneByBarcodeAsync(string barcode)
        {
            try
            {
                var product = await _productRepository.GetOneAsync(x => x.Barcode == barcode && x.IsDeleted == false);

                return Mapper.Map<ProductDto>(product);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IReadOnlyCollection<ProductDto>? Search(BrowseProductsModel model)
        {
            try
            {
                var products = _productRepository.Search(model.Text, model.Offset, model.Length);

                return Mapper.Map<IReadOnlyCollection<ProductDto>>(products);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}