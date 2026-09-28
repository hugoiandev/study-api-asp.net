using study_api.Entities;

namespace study_api.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProduct(Product product);

        Task<List<Product>> GetAllProducts();

        Task DeleteProduct(int id);

        Task<Product?> GetProductById(int id);

        Task<Product> UpdateProduct(Product product);

    }
}
