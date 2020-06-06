using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopHub.Models.Context;
using ShopHub.Models.Dtos;
using ShopHub.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ShopHub.Test
{
    public class NewTestCases
    {
       
        [Fact]
        public void AddsProductToDbTest()
        { // Arrange
            var options = new DbContextOptionsBuilder<ShopHubContext>()
            .UseInMemoryDatabase(databaseName: "AddProductToDbTest").Options;

            //Act
            using (var db = new ShopHubContext(options))
            {
                Product cup = new Product
                {
                    LocationId = 5,
                    Name = "cup",
                    Quantity = 10,
                    Price = "100"
                };
                
                db.Add(cup);
                db.SaveChanges();
            }
            //Assert
            using (var context = new ShopHubContext(options))
            {
                Assert.Equal(1, context.Products.Count());

                var createdProductInTempDB = context.Products.Where(p => p.LocationId == 5).FirstOrDefault();
                Assert.Equal("100", createdProductInTempDB.Price);
                Assert.Equal(10, createdProductInTempDB.Quantity);
            }
        }

        [Fact]
        public void AddMultiipleProductToDbTest()
        { // Arrange
            var options = new DbContextOptionsBuilder<ShopHubContext>()
            .UseInMemoryDatabase(databaseName: "AddProductToDbTest").Options;

            //Act
            using (var db = new ShopHubContext(options))
            {
                List<Product> products = new List<Product>()
                {
                    new Product { LocationId = 2, Name = "Bike", Price = "4500", Quantity = 5 },
                    new Product { LocationId = 3, Name = "Apple", Price = "500", Quantity = 10 }
                };
                
                db.AddRange(products);
                db.SaveChanges();
            }
            //Assert
            using (var context = new ShopHubContext(options))
            {
                var getProductsFromTempDB = context.Products.Where(p => p.LocationId == 2 || p.LocationId == 3).ToList();
                Assert.Equal(2, getProductsFromTempDB.Count());
            }
        }

        [Fact]
        public void GetProductByLocationIdTest()
        { // Arrange
            var options = new DbContextOptionsBuilder<ShopHubContext>()
            .UseInMemoryDatabase(databaseName: "AddProductToDbTest").Options;
            Product getProductFromTempDB = new Product();

            //Act
            using (var db = new ShopHubContext(options))
            {
                Product cup = new Product
                {
                    LocationId = 2,
                    Name = "cup",
                    Quantity = 10,
                    Price = "100"
                };

                db.Add(cup);
                db.SaveChanges();

                getProductFromTempDB = db.Products.Where(p => p.LocationId == 2).FirstOrDefault();
            }
            //Check product is not null after save it to database
            //Assert
            Assert.NotNull(getProductFromTempDB);
        }

        [Fact]
        public void UpdateProductToDbTest()
        { // Arrange
            var options = new DbContextOptionsBuilder<ShopHubContext>()
            .UseInMemoryDatabase(databaseName: "AddProductToDbTest").Options;

            //Act
            using (var db = new ShopHubContext(options))
            {
                Product cap = new Product
                {
                    LocationId = 1,
                    Name = "cap",
                    Quantity = 20,
                    Price = "200"
                };

                db.Add(cap);
                db.SaveChanges();

                //Update the quantity of recently saved product
                var createdProductEntity = db.Products.Where(p => p.LocationId == 1).FirstOrDefault();
                createdProductEntity.Quantity = 30;
                createdProductEntity.Name = "Straight Cap";
                db.SaveChanges();

            }
            //Assert
            using (var context = new ShopHubContext(options))
            {
                var getProductFromTempDB = context.Products.Where(p => p.LocationId == 1).FirstOrDefault();
                Assert.Equal(30, getProductFromTempDB.Quantity);
                Assert.Equal("Straight Cap", getProductFromTempDB.Name);
            }
        }


        [Fact]
        public void RemoveProductToDbTest()
        { // Arrange
            var options = new DbContextOptionsBuilder<ShopHubContext>()
            .UseInMemoryDatabase(databaseName: "AddProductToDbTest").Options;
            var expectedOutComes = string.Empty;
            //Act
            using (var db = new ShopHubContext(options))
            {
                Product bottle = new Product
                {
                    LocationId = 1,
                    Name = "bottle",
                    Quantity = 90,
                    Price = "100"
                };

                db.Add(bottle);
                db.SaveChanges();

                //Remove the recently saved product
                var productEntity = db.Products.Where(p => p.LocationId == 1).FirstOrDefault();
                db.Products.Remove(productEntity);
                db.SaveChanges();

            }
            //Assert
            using (var context = new ShopHubContext(options))
            {
                var getProductFromTempDB = context.Products.Where(p => p.LocationId == 1).FirstOrDefault();
                if (getProductFromTempDB is null)
                {
                    expectedOutComes = "";
                }
                Assert.Equal("", expectedOutComes);
            }
        }


        [Fact]
        public void GetProductByProductId()
        { // Arrange
            var options = new DbContextOptionsBuilder<ShopHubContext>()
            .UseInMemoryDatabase(databaseName: "AddProductToDbTest").Options;
            var expectedProductId = 1;
            //Act
            using (var db = new ShopHubContext(options))
            {
                Product watch = new Product
                {
                    LocationId = 1,
                    Name = "watch",
                    Quantity = 30,
                    Price = "300"
                };

                db.Add(watch);
                db.SaveChanges();

            }
            //Assert
            using (var context = new ShopHubContext(options))
            {
                var getProductFromTempDB = context.Products.Where(p => p.Id == 1).FirstOrDefault();
                
                Assert.Equal(expectedProductId, getProductFromTempDB.Id);
            }
        }



        /*For Order Section*/

        [Fact]
        public void AddOrderToDbTest()
        { // Arrange
            var options = new DbContextOptionsBuilder<ShopHubContext>()
            .UseInMemoryDatabase(databaseName: "AddOrderToDbTest").Options;

            //Act
            using (var db = new ShopHubContext(options))
            {
                Order order = new Order
                {
                    UserId = 1,
                    ProductId = 2,
                    Quantity = 10,
                    Timestamp = DateTime.Now
                };

                db.Add(order);
                db.SaveChanges();
            }
            //Assert
            using (var context = new ShopHubContext(options))
            {
                var createdOrderInTempDB = context.Orders.Where(p => p.Id == 1).FirstOrDefault();
                Assert.NotNull(createdOrderInTempDB);
            }
        }

        [Fact]
        public void MinusQuantityWhenOrderPlaceToDbTest()
        { // Arrange
            var options = new DbContextOptionsBuilder<ShopHubContext>()
            .UseInMemoryDatabase(databaseName: "AddOrderToDbTest").Options;

            //Act

            using (var db = new ShopHubContext(options))
            {
                //Add product to db
                Product watch = new Product
                {
                    LocationId = 1,
                    Name = "watch",
                    Quantity = 30,
                    Price = "300"
                };

                db.Add(watch);
                db.SaveChanges();

                //Place order to db
                Order order = new Order
                {
                    UserId = 1,
                    ProductId = 1,
                    Quantity = 10,
                    Timestamp = DateTime.Now
                };

                db.Add(order);
                db.SaveChanges();

                //Minus Stock Quantity
                var createdProductInTempDB = db.Products.Where(p => p.Name.Equals("watch")).FirstOrDefault();
                createdProductInTempDB.Quantity = createdProductInTempDB.Quantity - order.Quantity;
                db.SaveChanges();
            }
            //Assert
            using (var context = new ShopHubContext(options))
            {
                var createdOrderInTempDB = context.Products.Where(p => p.Name.Equals("watch")).FirstOrDefault();
                Assert.Equal(20,createdOrderInTempDB.Quantity);
            }
        }
    }
}
