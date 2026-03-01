using DemoWebApiDB.Data.Data;
using DemoWebApiDB.Data.Entities;


namespace DemoWebApiDB.Tests.TestInfrastructure;


/// <summary>
///     Seeds deterministic test data for integration tests.
///     Each test starts with the same known set of data.
/// </summary>
internal static class TestDatabaseSeeder
{

    public static void Seed(ApplicationDbContext db)
    {
        // Prevent duplicate seeding (can occur due to race conditions during parallel runs of tests)
        db.Categories.RemoveRange(db.Categories);
        db.Products.RemoveRange(db.Products);
        db.SaveChanges();


        var cat1 = new Category { Name = "Electronics" };
        var cat2 = new Category { Name = "Stationery" };
        var cat3 = new Category { Name = "Printers" };        // category without products

        db.Categories.AddRange(cat1, cat2, cat3);
        db.SaveChanges();

        db.Products.AddRange(
            new Product
            {
                ProductName = "Laptop",
                Price = 50000,
                QtyInStock = 10,
                CtgryId = cat1.CategoryId
            },
            new Product
            {
                ProductName = "Pen",
                Price = 10,
                QtyInStock = 100,
                CtgryId = cat2.CategoryId
            },
            new Product
            {
                ProductName = "Desktop Computer",
                Price = 20000,
                QtyInStock = 5,
                CtgryId = cat1.CategoryId
            },
            new Product
            {
                ProductName = "Pencil",
                Price = 2,
                QtyInStock = 20,
                CtgryId = cat2.CategoryId
            },
            new Product
            {
                ProductName = "Eraser",
                Price = 1,
                QtyInStock = 30,
                CtgryId = cat2.CategoryId
            }
        );

        db.SaveChanges();
    }

}
