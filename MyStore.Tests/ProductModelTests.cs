using MyStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyStore.Tests
{
    public class ProductModelTests
    {
        public const string ProductNameRequiredMessage = "The Productname field is required.";
        public const string ProductNameTooShortMessage = "The field Productname must be a string or array type with a minimum length of '4'.";
        public const string UnitPriceNameRequiredMessage = "The Unitprice field is required.";
        public const int ValidCategoryId = 2;
        public const string ProductName_Short = "Abc";

        [Fact]
        public void Should_Pass()
        {
            //sut = subject under test
            var sut = new ProductModel()
            {
                Categoryid = ValidCategoryId,
                Productid = Consts.TestProduct,
                Supplierid = Consts.TestSupplierID,
                Unitprice = Consts.Unitprice,
                Discontinued = true,
                Productname = Consts.ProductName
            };

            ////act
            //lista de erori
            var validationResults = new List<ValidationResult>();
            //validam sut; contextul, unde punem rezultatul, daca vrem sa validam toate proprietatile -> true . facem trigger la validare
            var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            //assert - trebuie sa ii aruncam ceva
            Assert.True(actual, "Expected to succeed");
        
        }

        [Fact]
        public void Should_Fail_When_ProductName_IsEmpty()
        {
            //sut = subject under test
            var sut = new ProductModel()
            {
                Categoryid = ValidCategoryId,
                Productid = Consts.TestProduct,
                Supplierid = Consts.TestSupplierID,
                Unitprice = Consts.Unitprice,
                Discontinued = true,
                Productname = ""
            };

            ////act
            //lista de erori
            var validationResults = new List<ValidationResult>();
            //validam sut; contextul, unde punem rezultatul, daca vrem sa validam toate proprietatile -> true . facem trigger la validare
            var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);

            var message = validationResults[0];
            //var message1 = validationResults[1];
            //assert
            Assert.Equal(ProductNameRequiredMessage, message.ErrorMessage);
            //Assert.Equal("The Unitprice field is required.", message1.ErrorMessage);
        }

        [Fact]
        public void Should_Fail_When_Unitprice_IsEmpty()
        {
            var sut = new ProductModel()
            {
                Categoryid = ValidCategoryId,
                Productid = Consts.TestProduct,
                Supplierid = Consts.TestSupplierID,
                Unitprice = null,
                Discontinued = true,
                Productname = Consts.ProductName
            };

            //act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);
            var message = validationResults[0];

            //assert
            Assert.Equal(UnitPriceNameRequiredMessage, message.ErrorMessage);


        }

        [Fact]
        public void Should_Fail_When_Productname_IsLessThan4()
        {
            //arrange
            var sut = new ProductModel()
            {
                Categoryid = ValidCategoryId,
                Productid = Consts.TestProduct,
                Supplierid = Consts.TestSupplierID,
                Unitprice = Consts.Unitprice,
                Discontinued = true,
                Productname = ProductName_Short
            };

            //act
            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResults, true);
            var message = validationResults[0];

            //assert
            Assert.Equal(ProductNameTooShortMessage, message.ErrorMessage);
        }

    }
}
