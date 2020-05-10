using System;
using System.Collections.Generic;
using System.Text;

namespace ShopHub.Models.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; set;}
    }
}
