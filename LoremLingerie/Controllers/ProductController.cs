using ErrorOr;
using LoremLingerie.Contracts.Product;
using LoremLingerie.Models;
using LoremLingerie.ServiceErrors;
using LoremLingerie.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace LoremLingerie.Controllers;

public class ProductController : ApiController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public IActionResult CreateProduct(CreateProductRequest request)
    {
        ErrorOr<Product> requestToProductResult = Product.From(request);
        if (requestToProductResult.IsError)
        {
            return Problem(requestToProductResult.Errors);
        }
        var product = requestToProductResult.Value;
        ErrorOr<Created> createProductResult = _productService.CreateProduct(product);
        return createProductResult.Match(
            created => CreatedAtGetProduct(product),
            errors => Problem(errors)
        );

    }


    [HttpGet("{id:guid}")]
    public IActionResult GetProduct(Guid id)
    {
        ErrorOr<Product> getProductResult = _productService.GetProduct(id);

        return getProductResult.Match(
            product => Ok(MapProductResponse(product)),
            errors => Problem(errors));
    }


    [HttpPut("{id:guid}")]
    public IActionResult UpsertProduct(Guid id, UpsertProductRequest request)
    {
        ErrorOr<Product> requestToProductResult = Product.From(id, request);
        if (requestToProductResult.IsError)
        {
            return Problem(requestToProductResult.Errors);
        }
        var product = requestToProductResult.Value;
        ErrorOr<UpsertedProduct> upsertedProductResult = _productService.UpsertProduct(product);
        return upsertedProductResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetProduct(product) : NoContent(),
            Errors => Problem(Errors)
        );

    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteProduct(Guid id)
    {
        ErrorOr<Deleted> deletedProductResult = _productService.DeleteProduct(id);
        return deletedProductResult.Match(
            Deleted => NoContent(),
            Errors => Problem(Errors)
        );
    }

    private static ProductResponse MapProductResponse(Product product)
    {
        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.StartDateTime,
            product.EndDateTime,
            product.LastModifiedDateTime,
            product.Color,
            product.Size
        );
    }
    private CreatedAtActionResult CreatedAtGetProduct(Product product)
    {
        return CreatedAtAction(
            actionName: nameof(GetProduct),
            routeValues: new { id = product.Id },
            value: MapProductResponse(product));
    }

}