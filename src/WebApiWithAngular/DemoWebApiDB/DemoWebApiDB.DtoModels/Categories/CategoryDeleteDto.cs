namespace DemoWebApiDB.DtoModels.Categories;


/// <summary>
///     DTO Model used when deleting a Category.
///     Requires RowVersion for concurrency validation.
/// </summary>
public sealed record class CategoryDeleteDto
(
    [Required]
    int CategoryId,

    [Required]
    string RowVersion
);