namespace DemoWebApiDB.DtoModels.ReadModels.Reports;


/// <summary>
///     Read-only projection model representing category-wise number of products.
/// 
///     Used to map results returned by STORED PROCEDURE to this DTO/ReadModel:
///     dbo.usp_Category_ProductCount
/// </summary>
/// <remarks>
///     NOTE:
///     - It will not be registered into the EF DbContext, since it is a SP Projection only.
///     - Will be used by the CategoryService.GetCategoryProductCountAsync() method
/// </remarks>
public sealed record class CategoryProductCountReadModel
{

    /// <summary>
    ///     Category identifier.
    /// </summary>
    public int CategoryId { get; init; }


    /// <summary>
    ///     Category name.
    /// </summary>
    public string CategoryName { get; init; } = string.Empty;


    /// <summary>
    ///     Number of products under the category.
    /// </summary>
    public int ProductCount { get; init; }

}