using Microsoft.AspNetCore.Mvc;
using RegisterOfProducts.Models;
using RegisterOfProducts.Repository;

namespace RegisterOfProducts.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }
        public IActionResult Index()
        {
            List<ProductModel> products = _repository.GetAll();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            ProductModel product = _repository.GetById(id);
            return View(product);
        }

        public IActionResult ConfirmDelete(int id)
        {
            ProductModel product = _repository.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Create(ProductModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.ToAdd(product);
                    TempData["SuccessMessage"] = "Product successfully registered";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = $"We were unable to register the product, try again, error detail:{e.Message}";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public IActionResult Edit(ProductModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Update(product);
                    TempData["SuccessMessage"] = "Product successfully altered";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = $"We were unable to altered the product, try again, error detail:{e.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _repository.Delete(id);
                if (deleted)
                {
                    TempData["SuccessMessage"] = "Successfully deleted product";
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not deleted";
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = $"We were unable to delete the product, please try again, error detail: {e.Message}";
            }

            return RedirectToAction("Index");
        }
    }

}
