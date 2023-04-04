using RegisterOfProducts.Models;

namespace RegisterOfProducts.Repository
{
    public interface IProductRepository
    {
        List<ProductModel> GetAll();
        ProductModel GetById(int id);
        ProductModel ToAdd(ProductModel product);
        ProductModel Update(ProductModel product);
        bool Delete(int id);
    }
}
