namespace DemoWebApiDB.DtoModels.Products;


/// <summary>
///     DTO Model used when deleting a Product.
///     Requires RowVersion for concurrency validation.
/// </summary>
public sealed record class ProductDeleteDto
(
    /// <summary>
    ///     Product identifier.
    /// </summary>
    [Required]
    Guid ProductId,


    [Required]
    string RowVersion

);