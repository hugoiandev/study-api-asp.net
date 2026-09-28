using study_api.Entities;
using study_api.Interfaces;

namespace study_api.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _repository;

        public ProductService() { }

        public ProductService(IProductRepository productRepository)
        {
        
            _repository = productRepository;
        }

        public async Task<Product> CreateProduct(Product _product)
        {
            var product = await _repository.Create(_product);

            return product;
        }

        public async Task DeleteProduct(int id)
        {
            await _repository.DeleteById(id);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _repository.GetAll();

            return products;
        }

        public async Task<Product?> GetProductById(int id)
        {
            var product = await _repository.GetById(id);

            return product;
        }

        public async Task<Product> UpdateProduct(Product _product)
        {
            var product = await _repository.UpdateById(_product);

            return product;
        }
    }
}
