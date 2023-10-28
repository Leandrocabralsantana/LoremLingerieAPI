using ErrorOr;
using LoremLingerie.Contracts.Product;
using LoremLingerie.ServiceErrors;

namespace LoremLingerie.Models;

public class Product
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;
    public const int MinDescriptionLength = 20;
    public const int MaxDescriptionLength = 150;

    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> Color { get; } = new();
    public List<string> Size { get; } = new();

    private Product(
        Guid id,
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime lastModifiedDateTime,
        List<string> color,
        List<string> size
    )
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Color = color;
        Size = size;
    }

    public static ErrorOr<Product> Create(
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        List<string> color,
        List<string> size,
        Guid? id = null
    )
    {
        List<Error> errors = new();

        if (name.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Product.InvalidName);
        }
        if (description.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Product.InvalidDescription);
        }
        if (errors.Count > 0)
        {
            return errors;
        }
        return new Product(
        id ?? Guid.NewGuid(),
        name,
        description,
        startDateTime,
        endDateTime,
        DateTime.UtcNow,
        color,
        size
    );
    }

    public static ErrorOr<Product> From(CreateProductRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Color,
            request.Size
        );
    }
    public static ErrorOr<Product> From(Guid id, UpsertProductRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Color,
            request.Size,
            id
        );
    }


}