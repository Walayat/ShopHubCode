using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopHub.Models.Context;
using ShopHub.Models.Dtos;
using ShopHub.Models.Models;
using ShopHub.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopHub.Services.Services
{
    public class ProductService : IProductService
    {
        private ShopHubContext _context;
        private IMapper _mapper;
        public ProductService(ShopHubContext shopHubContext, IMapper mapper)
        {
            _context = shopHubContext;
            _mapper = mapper;
        }

        //This method is used to add product to database
        public ProductDto AddProduct(ProductDto product)
        {
            var mappedData = _mapper.Map<Product>(product);
            _context.Products.Add(mappedData);
            _context.SaveChanges();

            return _mapper.Map<ProductDto>(mappedData);
        }

        //This method is used to update product to database
        public ProductDto UpdateProduct(ProductDto product)
        {
            var record = _context.Products.Find(product.Id);
            _context.Entry(record).CurrentValues.SetValues(product);
            _context.SaveChanges();

            return _mapper.Map<ProductDto>(record);
        }

        //This method is used to get product by productId from database
        public ProductDto GetProductById(int productId)
        {
            var product = _context.Products.Include(x=>x.Location).FirstOrDefault(x => x.Id == productId);
            return _mapper.Map<ProductDto>(product);
        }

        //This method is used to minus product quantity to our stock when user have place their orders
        public void MinusProductQuantity(int productId, int quantity)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            product.Quantity = quantity;
            _context.SaveChanges();
        }

        //This method is used to get products by specific locationId
        public List<ProductDto> GetProductsByLocationId(int locationId)
        {
            var product = _context.Products.Include(x => x.Location).Where(x => x.LocationId == locationId).ToList();
            if (!(product is null) && product.Count > 0)
            {
                return _mapper.Map<List<ProductDto>>(product);
            }
            else
            {
                return null;
            }
        }

        //This method is used to get all products without specifying location
        public List<ProductDto> GetAllProducts()
        {
            var products = _context.Products.Include(x=>x.Location).ToList();
            if (!(products is null) && products.Count > 0)
            {
                return _mapper.Map<List<ProductDto>>(products);
            }
            else
            {
                return null;
            }
        }

        //This method is used to remove product from database
        public bool RemoveProduct(int productId)
        {
            var record = _context.Products.FirstOrDefault(x => x.Id == productId);
            var response = false;

            if (!(record is null))
            {
                _context.Products.Remove(record);
                _context.SaveChanges();
                response = true;
                return response;
            }
            else
            {
                return response;
            }
        }
    }
}
