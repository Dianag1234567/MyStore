using AutoMapper;
using Moq;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Infrastructure;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyStore.Tests
{
    public class ProductsServicesTests
    {
        private readonly Mock<IProductRepository> mockProductRepository;
        private readonly MapperConfiguration mappingConfig;
        private readonly IMapper _mapper;
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

        public ProductsServicesTests()
        {
            mockProductRepository = new Mock<IProductRepository>();

            mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public void Should_Return_All_GetAllProducts()
        {
            //arrange
            mockProductRepository.Setup(x => x.GetAll())
                .Returns(MultipleProducts());

            var service = new ProductService(mockProductRepository.Object,_mapper);

            //act
            var response = service.GetAllProducts() as IEnumerable<ProductModel>;

            //assert
            Assert.IsType<List<ProductModel>>(response);
            Assert.Equal(MultipleProducts().Count, response.ToList().Count);
        }

        [Fact]
        public void ShouldReturn_ProductByID()
        {
            //arrange
            var firstProduct = MultipleProducts()[id - 1];
            mockProductRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(firstProduct);

            var service = new ProductService(mockProductRepository.Object,_mapper);

            //act
            var response = service.GetById(id) as ProductModel;

            //assert
            Assert.IsType<ProductModel>(response);
            Assert.Equal(firstProduct.Productid, response.Productid);
        }

        [Fact]
        public void Should_Work_For_Add()
        {
            //arrange
            mockProductRepository.Setup(x => x.Add(It.IsAny<Product>()))
                .Returns(newProduct);

            var service = new ProductService(mockProductRepository.Object,_mapper);

            //act
            var response = service.AddProduct(_mapper.Map<ProductModel>(newProduct));

            //assert
            Assert.IsType<ProductModel>(response);

        }

        [Fact]
        public void ShouldReturn_Work_UpdateProduct()
        {
            //arrange
            var service = new ProductService(mockProductRepository.Object, _mapper);

            //act
            var response= service.UpdateProduct(_mapper.Map<ProductModel>(newProduct));

            //assert
            Assert.IsType<ProductModel>(response);
            Assert.Equal(response.Productname, newProduct.Productname);

        }

        [Fact]
        public void ShouldReturn_True_For_Delete()
        {
            //arrange
            mockProductRepository.Setup(x => x.Delete(It.IsAny<Product>()))
                .Returns(true);
            mockProductRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(MultipleProducts()[id]);

            var service = new ProductService(mockProductRepository.Object,_mapper);

            //act
            var deleted = service.DeleteProduct(id);

            //assert
            Assert.True(deleted);
        }

        private List<Product> MultipleProducts()
        {
            return new List<Product>()
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
                },
                new Product
                {
                    Categoryid=(int) Consts.Categories.Condiments,
                    Productid=3,
                    Productname=Consts.ProductName,
                    Discontinued=false,
                    Supplierid=Consts.TestSupplierID,
                    Unitprice=Consts.Unitprice
                }
            };
        }
    }
}
