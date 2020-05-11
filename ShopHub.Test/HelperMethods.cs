using ShopHub.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopHub.Test
{
    public static class HelperMethods
    {
        public static UserAuthDto GetTestUserIdForRegisterAndLogin()
        {
            return new UserAuthDto()
            {
                Id = 1000,
            };
        }
        public static UserAuthDto GetTestUserIdNameForRegisterAndLogin()
        {
            return new UserAuthDto()
            {
                Id = 1000,
                FirstName = "John",
            };
        }

        public static UserAuthDto GetTestUserIdCompleteNameForRegisterAndLogin()
        {
            return new UserAuthDto()
            {
                Id = 1000,
                FirstName = "John",
                LastName = "Mic",
            };
        }

        public static UserAuthDto GetTestUserIdNameUserTypeForRegisterAndLogin()
        {
            return new UserAuthDto()
            {
                Id = 1000,
                FirstName = "John",
                LastName = "Mic",
                UserTypeId = 2,
            };
        }


        public static ProductDto GetTestProduct()
        {
            return new ProductDto()
            {
                Id = 1,
                LocationId = 1,
                Name = "New Product",
                Price = "100",
                Quantity = 200
            };
        }

    }
}
