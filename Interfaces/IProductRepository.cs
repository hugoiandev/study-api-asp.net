using study_api.Entities;

namespace study_api.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> Create(Product product);

        Task<List<Product>> GetAll();

        Task<Product?> GetById(int id);

        Task<Product> UpdateById(Product product);

        Task DeleteById(int id);
    }
}
