namespace LoremLingerie.Contracts.Product;
    public record UpsertProductRequest(
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        List<string> Color,
        List<string> Size
    );