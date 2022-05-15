using Moq;
using MyStore.Data;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyStore.Tests
{
    public class ProductRepositoryTests
    {
        private readonly Mock<IProductRepository> mockProductRepository;
        public const int id = 2;
        public Product newProduct = new Product
        {
            Categoryid = (int)Consts.Categories.Condiments,
            Productid = 4,
            Productname = "Nume nou xxxxxxxxx",
            Discontinued = false,
            Supplierid = Consts.TestSupplierID,
            Unitprice = Consts.Unitprice
        };
        public Product updatedProduct = new Product
        {
            Categoryid = (int)Consts.Categories.Condiments,
            Productid = id,
            Productname = "Nume nou",
            Discontinued = false,
            Supplierid = Consts.TestSupplierID,
            Unitprice = Consts.Unitprice
        };
        public ProductRepositoryTests()
        {
            mockProductRepository = new Mock<IProductRepository>();
        }

        [Fact]
        public void Should_GetAllProducts()
        {
            //arrange
            mockProductRepository.Setup(repo => repo.GetAll())
                .Returns(ReturnMultiple());

            //act
            var result = mockProductRepository.Object.GetAll();

            //assert
            Assert.IsType<List<Product>>(result);
            Assert.Equal(2, result.Count());
            
        }

        [Fact]
        public void ShouldGet_ProductByID()
        {
            //arrange
            var myProduct = ReturnMultiple()[id-1];
            mockProductRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(myProduct);

            //act
            var response = mockProductRepository.Object.GetById(id) as Product;

            //assert
            Assert.IsType<Product>(response);
            Assert.Equal(myProduct.Productid, response.Productid);
        }


        [Fact]
        public void Should_Add_Product()
        {
            //arrange
            mockProductRepository.Setup(x => x.Add(It.IsAny<Product>()))
                .Returns(newProduct);

            //act
            var response = mockProductRepository.Object.Add(newProduct);

            //assert
            Assert.IsType<Product>(response);
            Assert.Equal(newProduct.Productid, response.Productid);

        }

        [Fact]
        public void ShouldReturn_Work_UpdateProduct()
        {
            //arrange
            mockProductRepository.Setup(x => x.Update(It.IsAny<Product>()))
                .Returns(newProduct);

            //act
            var response = mockProductRepository.Object.Update(newProduct);

            //assert
            Assert.IsType<Product>(response);
            Assert.Equal(response.Productname, newProduct.Productname);

        }

        [Fact]
        public void ShouldReturn_True_For_Delete()
        {
            //arrange
            mockProductRepository.Setup(x => x.Delete(It.IsAny<Product>()))
                .Returns(true);
            mockProductRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(ReturnMultiple()[id - 1]);

            //act
            var deleted = mockProductRepository.Object.Delete(ReturnMultiple()[id - 1]);

            //assert
            Assert.True(deleted);
        }

        [Fact]
        public void ShouldReturn_False_For_Delete()
        {
            //arrange
            mockProductRepository.Setup(x => x.Delete(It.IsAny<Product>()))
                .Returns(false);
            mockProductRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(ReturnMultiple()[id-1]);

            //act
            var deleted = mockProductRepository.Object.Delete(ReturnMultiple()[id-1]);

            //assert
            Assert.False(deleted);
        }
        private List<Product> ReturnMultiple()
        {
            return new List<Product>
                {
                    new Product
                    {
                        Categoryid=(int) Consts.Categories.Condiments,
                        Productid=1,
                        Productname=Consts.ProductName,
                        Discontinued=false,
                        Supplierid=Consts.TestSupplierID,
                        Unitprice=Consts.Unitprice

                    },
                    new Product
                    {
                        Categoryid=(int) Consts.Categories.Condiments,
                        Productid=2,
                        Productname=Consts.ProductName,
                        Discontinued=false,
                        Supplierid=Consts.TestSupplierID,
                        Unitprice=Consts.Unitprice
                    }
                };
        }
    }
}
