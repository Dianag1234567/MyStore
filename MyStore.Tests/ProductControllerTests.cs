using Microsoft.AspNetCore.Mvc;
using Moq;
using MyStore.Controllers;
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
    public class ProductControllerTests
    {
        private Mock<IProductService> mockProductService;
        public const int id= 2;
        public ProductModel updatedProduct = new ProductModel
        {
            Categoryid = (int)Consts.Categories.Condiments,
            Productid = id,
            Productname = "Nume nou",
            Discontinued = false,
            Supplierid = Consts.TestSupplierID,
            Unitprice = Consts.Unitprice
        };

        public ProductModel newProduct = new ProductModel
        {
            Categoryid = (int)Consts.Categories.Condiments,
            Productid = 4,
            Productname = "Nume nou",
            Discontinued = false,
            Supplierid = Consts.TestSupplierID,
            Unitprice = Consts.Unitprice
        };

        public ProductControllerTests()
        {
            mockProductService = new Mock<IProductService>();
        }    

        [Fact]
        public void Should_Return_OK_OnGetAll()
        {
            //arrange
            mockProductService.Setup(x => x.GetAllProducts())
                .Returns(MultipleProducts());

            var controller = new ProductsController(mockProductService.Object);

            //act

            var response = controller.Get();

            var result = response.Result as OkObjectResult;
            var actualData= result.Value as IEnumerable<ProductModel>;//rez la care ma astept

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ProductModel>>(actualData);
        }

        [Fact]
        public void ShouldReturn_AllProducts()
        {
            //arrange
            mockProductService.Setup(x => x.GetAllProducts())
                .Returns(MultipleProducts());

            var controller = new ProductsController(mockProductService.Object);

            //act

            var response = controller.Get();

            var result = response.Result as OkObjectResult;
            var actualData = result.Value as IEnumerable<ProductModel>;//rez la care ma astept

            //assert
            Assert.Equal(MultipleProducts().Count(),actualData.Count());
        }

        [Fact]
        public void ShouldReturn_ProductByID()
        {
            //arrange
            var firstProduct = MultipleProducts()[id-1];
            mockProductService.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(firstProduct);

            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.GetProduct(id);

            var result = response.Result as OkObjectResult;
            var actualData = result.Value as ProductModel;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ProductModel>(actualData);
            Assert.Equal(firstProduct, actualData);
        }

        [Fact]
        public void ShouldReturn_BadRequest_ProductById_NotExists()
        {
            //arrange
            var firstProduct = MultipleProducts()[id - 1];
            mockProductService.Setup(x => x.GetById(id))
                .Returns(firstProduct);

            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.GetProduct(id+1);

            var result = response.Result as NotFoundResult;
            
            //assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Should_Return_Created_For_Add()
        {
            //arrange
            mockProductService.Setup(x => x.AddProduct(newProduct))
                .Returns(newProduct);

            var controller = new ProductsController(mockProductService.Object);

            //act
            var response = controller.Post(newProduct);
            var result = response.Result as CreatedAtActionResult;

            //assert
            Assert.IsType<CreatedAtActionResult>(result);

        }

        [Fact]
        public void ShouldReturn_BadRequest_For_Add()
        {
            //arrange
            var newProduct = new ProductModel
            {
                Supplierid = Consts.TestSupplierID,
                Unitprice = null
            };

            mockProductService.Setup(x => x.AddProduct(newProduct))
                .Returns(newProduct);
            
            var controller = new ProductsController(mockProductService.Object);
            controller.ModelState.AddModelError("Title", "Required");

            //act
            var response = controller.Post(newProduct);
            var result = response.Result as BadRequestResult;
            //assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public void ShouldReturn_NoContent_UpdateProduct()
        {
            //arrange
            mockProductService.Setup(x => x.AddProduct(It.IsAny<ProductModel>()));
            mockProductService.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);

            var controller = new ProductsController(mockProductService.Object);

            //act
            var updatedData = controller.Put(id, updatedProduct) as NoContentResult;

            //assert
            Assert.IsType<NoContentResult>(updatedData);
        }

        [Fact]
        public void ShouldReturn_BadRequest_For_Update()
        {
            //arrange
            mockProductService.Setup(x => x.AddProduct(It.IsAny<ProductModel>()));

            var controller = new ProductsController(mockProductService.Object);

            //act
            var updated = controller.Put(id+1, updatedProduct) as BadRequestResult;

            //assert
            Assert.IsType<BadRequestResult>(updated);
        }

        [Fact]
        public void ShouldReturn_NotFound_For_Update()
        {
            //arrange
            mockProductService.Setup(x => x.AddProduct(It.IsAny<ProductModel>()));
            mockProductService.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);

            var controller = new ProductsController(mockProductService.Object);

            //act
            var updated = controller.Put(id, updatedProduct) as NotFoundResult;

            //assert
            Assert.IsType<NotFoundResult>(updated);
        }

        [Fact]
        public void ShouldReturn_NoContent_For_Delete()
        {
            //arrange
            mockProductService.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);

            var controller = new ProductsController(mockProductService.Object);

            //act
            var deleted = controller.Delete(id) as NoContentResult;

            //assert
            Assert.IsType<NoContentResult>(deleted);
        }

        [Fact]
        public void ShouldReturn_NotFound_For_Delete()
        {
            //arrange
            mockProductService.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);

            var controller = new ProductsController(mockProductService.Object);

            //act
            var deleted = controller.Delete(id) as NotFoundResult;

            //assert
            Assert.IsType<NotFoundResult>(deleted);
        }


        private List<ProductModel> MultipleProducts()
        {
            return new List<ProductModel>()
            {
                new ProductModel
                {
                    Categoryid=(int) Consts.Categories.Condiments,
                    Productid=1,
                    Productname=Consts.ProductName,
                    Discontinued=false,
                    Supplierid=Consts.TestSupplierID,
                    Unitprice=Consts.Unitprice
                },
                new ProductModel
                {
                    Categoryid=(int) Consts.Categories.Condiments,
                    Productid=2,
                    Productname=Consts.ProductName,
                    Discontinued=false,
                    Supplierid=Consts.TestSupplierID,
                    Unitprice=Consts.Unitprice
                },
                new ProductModel
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
