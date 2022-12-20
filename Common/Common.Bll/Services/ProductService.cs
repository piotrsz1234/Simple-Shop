using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.Bll.Services.Enums;
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

        public async Task<AddEditProductResult> AddEditProductAsync(AddEditProductModel model)
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
                else if (await _productRepository.AnyAsync(x => x.Barcode == model.Barcode && x.IsDeleted == false))
                    return AddEditProductResult.BarcodeAlreadyExists;

                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Barcode))
                {
                    return AddEditProductResult.Error;
                }
                
                var newEntity = Mapper.Map<Product>(model);
                await _productRepository.AddAsync(newEntity);
                await _productRepository.SaveChangesAsync();
                return AddEditProductResult.Ok;
            }
            catch (Exception e)
            {
                return AddEditProductResult.Error;
            }
        }
        
        public AddEditProductResult AddEditProduct(AddEditProductModel model)
        {
            try
            {
                if (model.Id.HasValue)
                {
                    var alreadyExisting = _productRepository.GetOne(model.Id.Value);

                    if (alreadyExisting != null)
                    {
                        alreadyExisting.IsDeleted = true;
                        alreadyExisting.ModificationDateUTC = DateTime.UtcNow;
                    }
                }
                else if (_productRepository.Any(x => x.Barcode == model.Barcode && x.IsDeleted == false))
                    return AddEditProductResult.BarcodeAlreadyExists;

                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Barcode))
                {
                    return AddEditProductResult.Error;
                }
                
                var newEntity = Mapper.Map<Product>(model);
                _productRepository.Add(newEntity);
                _productRepository.SaveChanges();
                return AddEditProductResult.Ok;
            }
            catch (Exception e)
            {
                return AddEditProductResult.Error;
            }
        }

        public ProductDto? GetOne(long productId)
        {
            var product = _productRepository.GetOne(productId);

            return Mapper.Map<ProductDto>(product);
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
        
        public bool RemoveProduct(long productId)
        {
            try
            {
                var product = _productRepository.GetOne(productId);
                
                if (product is null)
                {
                    return false;
                }

                product.IsDeleted = true;
                product.ModificationDateUTC = DateTime.UtcNow;

                _productRepository.SaveChanges();

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

        public ProductDto? GetOneByBarcode(string barcode)
        {
            try
            {
                var product = _productRepository.GetOne(x => x.Barcode == barcode && x.IsDeleted == false);

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