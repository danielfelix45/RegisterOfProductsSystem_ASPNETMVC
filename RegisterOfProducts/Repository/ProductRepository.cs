using RegisterOfProducts.Data;
using RegisterOfProducts.Models;

namespace RegisterOfProducts.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        public ProductRepository(ProductDbContext productDbContext)
        {
            _context = productDbContext;
        }

        public List<ProductModel> GetAll()
        {
            return _context.Products.ToList();
        }

        public ProductModel GetById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public ProductModel ToAdd(ProductModel product)
        {
            product.DateRegister = DateTime.Now;
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public ProductModel Update(ProductModel product)
        {
            ProductModel productDB = GetById(product.Id);

            if (productDB == null) throw new Exception("There was an error updating the product");

            productDB.Name = product.Name;
            productDB.Description = product.Description;
            productDB.Price = product.Price;
            productDB.DateUpdate = DateTime.Now;

            _context.Products.Update(productDB);
            _context.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            ProductModel productDB = GetById(id);

            if (productDB == null) throw new Exception("There was an error deleting the product");

            _context.Products.Remove(productDB);
            _context.SaveChanges();
            return true;
        }

    }
}
