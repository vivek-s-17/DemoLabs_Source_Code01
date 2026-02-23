using DemoWebApiDB.Data;
using DemoWebApiDB.Migrations;
using DemoWebApiDB.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApiDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoWithIncludeController : ControllerBase
    {
        private readonly ApplicationDataContext _dbContext;
        
        public DemoWithIncludeController(ApplicationDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        // GET /api/DemoWithInclude
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            // ------ VERSION 1
            // IEnumerable<Category>? result = await _dbContext.Categories.ToListAsync();
            // var result = await _dbContext.Categories
            //                      .ToListAsync();             // SQL fired here! Early Loading
            // return Ok(result);

            // ------ VERSION 2
            var result = from c in _dbContext.Categories
                         select c;
            return Ok( result );                                // SQL fired here!
        }


        // GET /api/DemoWithInclude
        [HttpGet("GetAllWithInclude")]
        public async Task<IActionResult> GetAllCategoriesWithIncludedProducts()
        {
            // ------ VERSION 1 (project the data into a new DTO)
            //var result
            //   = await _dbContext.Categories
            //                     .Include(c => c.Products)            // Eager Loading of PRODUCTS
            //                     .Select(c => new
            //                     {
            //                         c.CategoryId,
            //                         c.Name,
            //                         c.Products
            //                     })
            //                     .ToListAsync();                      // SQL is fired here!

            //return Ok(result);

            // ------ VERSION 2  (project the data into a new DTO)
            var result = from c in _dbContext.Categories
                                             .Include(c => c.Products)       // Eager loading of PRODUCTS
                         select new
                         {
                             c.CategoryId,
                             c.Name,
                             c.Products
                         };
            return Ok(result);                          // SQL is fired here!
        }



        // GET /api/DemoWithInclude
        [HttpGet("GetProductsOfCategory")]
        public async Task<IActionResult> GetProductsOfCategory()
        {
            // var result = await _dbContext.Categories
            //                      .Include(c => c.Products )            // Eager Loading
            //                      .FirstOrDefaultAsync();               // SQL get ONE Category & ALL PRODUCTS of Category

            var result = from c in _dbContext.Categories.Include(c => c.Products)       // Eager loading
                         select c;

            if (result is not null)
            {
                // var firstCategory = result.FirstOrDefault();              // SQL get ONE Category & ALL PRODUCTS of Category
                var firstCategory = await result.FirstOrDefaultAsync();      
                if (firstCategory is not null)
                {
                    var noOfProductsInFirstCategory 
                        = firstCategory.Products?.Count;     // for AWAIT: SQL get ONE Category & ALL PRODUCTS of Category
                }
            }

            return Ok(result);
        }


    }
}
