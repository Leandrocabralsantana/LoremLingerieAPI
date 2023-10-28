namespace LoremLingerie.Contracts.Product;
    public record ProductResponse(
        Guid Id,
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        DateTime LastModifiedDateTime,
        List<string> Color,
        List<string> Size);

