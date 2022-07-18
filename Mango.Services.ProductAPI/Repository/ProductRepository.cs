﻿using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext  _db;
        private IMapper _mapper;
        
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper; 
            _db = db; 
        }
              
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productDto);
            // as in if it exists already then just update it 
            if (product.ProductId > 0)
            {
                _db.Products.Update(product);
            }
            else
            {
                _db.Products.Add(product);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                // Product product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
                Product product = await _db.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
                if (product == null)
                {
                    return false;
                }
                _db.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
 
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            // Product product = await _db.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            Product product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> productList = await _db.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(productList);
        }
    }
}
