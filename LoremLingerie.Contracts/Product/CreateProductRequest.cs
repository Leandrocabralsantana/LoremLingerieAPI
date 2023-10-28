namespace LoremLingerie.Contracts.Product;
    public record CreateProductRequest(
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        List<string> Color,
        List<string> Size
    );