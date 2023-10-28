
using ErrorOr;
using LoremLingerie.Models;

namespace LoremLingerie.Services.Products;

public interface IProductService
{
    ErrorOr<Created> CreateProduct(Product product);
    ErrorOr<Product> GetProduct(Guid id);
    ErrorOr<UpsertedProduct> UpsertProduct(Product product);
    ErrorOr<Deleted> DeleteProduct(Guid id);
}