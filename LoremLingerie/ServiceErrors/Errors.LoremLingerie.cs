using ErrorOr;

namespace LoremLingerie.ServiceErrors;

public static class Errors
{
    public static class Product
    {
        public static Error InvalidName => Error.Validation(
            code: "Product.InvalidName",
            description: $"The name must be between {Models.Product.MinNameLength} and {Models.Product.MaxNameLength} characters long."
        );
        public static Error InvalidDescription => Error.Validation(
       code: "Product.InvalidName",
       description: $"The name must be between {Models.Product.MinDescriptionLength} and {Models.Product.MaxDescriptionLength} characters long."
   );
        public static Error NotFound => Error.NotFound(
            code: "Product.NotFound",
            description: "The product was not found."
        );

    }



}