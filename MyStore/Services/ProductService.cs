using AutoMapper;
using MyStore.Data;
using MyStore.Domain.Entities;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Services
{
    public interface IProductService
    {
        ProductModel AddProduct(ProductModel newProduct);
        bool DeleteProduct(int id);
        bool Exists(int id);
        IEnumerable<ProductModel> GetAllProducts();
        ProductModel GetById(int id);
        ProductModel UpdateProduct(ProductModel model);
    }
    public class ProductService: IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService (IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            //take domain objects
            var allProducts= productRepository.GetAll().ToList();//List <Product>
            //transform domain objects from Product -> ProductModel

            var productModels = mapper.Map<IEnumerable<ProductModel>>(allProducts);
            

            return productModels;
        }

        public ProductModel GetById(int id)
        {
            var product = productRepository.GetById(id);

            var productModel = mapper.Map<ProductModel>(product);
            return productModel;
        }

        public ProductModel AddProduct(ProductModel newProduct)
        {
            //-> ProductModel in Product. repository ca si concept nu stiu sa lucreze decat cu obiectele de domeniu
            //assuming is valid -> transform to Product (domain object)

            Product productToAddd = mapper.Map<Product>(newProduct);
            var addedProduct= productRepository.Add(productToAddd);

            newProduct = mapper.Map<ProductModel>(addedProduct);
            return newProduct;
        }

        public ProductModel UpdateProduct(ProductModel model)
        {
            Product productToUpdate = mapper.Map<Product>(model);

            productRepository.Update(productToUpdate);
            return mapper.Map<ProductModel>(productToUpdate);

        }

        public bool Exists(int id)
        {
            return productRepository.Exists(id);
        }

        public bool DeleteProduct(int id)
        {
            //get item by id
            var itemToDelete = productRepository.GetById(id);
            //delete item
            return productRepository.Delete(itemToDelete);
        }
    }
}
