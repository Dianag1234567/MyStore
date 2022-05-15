using MyStore.Tests.Mocks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyStore.Tests
{
    public class ProductServiceTests
    {
        private readonly MockProductService mockProductService;

        public ProductServiceTests(MockProductService mockProductService)
        {
            this.mockProductService = mockProductService;
        }

        //[Fact]
        //public void WhateverTest()
        //{
        //    var productById = mockProductService.MockGetById(new Domain.Entities.Product { });
        //}
    }
}
