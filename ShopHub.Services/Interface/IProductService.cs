using ShopHub.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopHub.Services.Interface
{
    public interface IProductService
    {
        public ProductDto AddProduct(ProductDto product);
        public List<ProductDto> GetAllProducts();
        public bool RemoveProduct(int productId);
        public ProductDto GetProductById(int productId);
        public ProductDto UpdateProduct(ProductDto product);
        public List<ProductDto> GetProductsByLocationId(int locationId);
        public void MinusProductQuantity(int productId, int quantity);
    }
}
