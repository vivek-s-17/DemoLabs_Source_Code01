using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace DemoWebApiDB.DtoModels.ReadModels;


/// <summary>
///     Read-only projection of product with the category details.
/// </summary>
/// <remarks>
///     This would be populated using the VIEW
///     NOTE:
///         - No validation annotations, since it is a ReadModel
///         - No need for a [Key].  EF Mapping is achieved via DBSet + HasNoKey().
///         - Since [Price] is a decimal column, it needs its precision to be defined.
/// </remarks>
[Keyless]
public sealed class ProductWithCategoryReadModel
{

    public Guid Id { get; set; }
    
    
    public string ProductName { get; set; } = string.Empty;


    [Column(TypeName = "decimal(7,2)")] 
    public decimal Price { get; set; }
    
    
    public int? QtyInStock { get; set; }
    
    
    public int CategoryId { get; set; }
    
    
    public string CategoryName { get; set; } = string.Empty;

}
