using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoWebApi.Controllers;


[Route(template:"api/[controller]")]                                 // Route Pattern
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMyDataService _dataService;


    public ProductsController(IMyDataService dataService)
    {
        _dataService = dataService;
    }


    // GET: api/<ProductsController>
    [HttpGet]
    public IEnumerable<Product> Get()
    {
        var data = _dataService.GetProducts();
        data.Reset();
        while(data.MoveNext())
        {
            yield return data.Current;
        }
    }

    // template: GET https://localhost:xxxx/api/[controller]/[action]
    // GET https://localhost:xxxx/api/Products/GetAll
    [HttpGet(template:"GetAll")]                        // Route Pattern for the API endpoint 
    public IEnumerable<Product> GetAll()
    {
        return _dataService.GetAllProducts();
    }


    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ProductsController>
    [HttpPost]
    public void Post([FromBody] Product product)
    {
        // add the product
        _dataService.AddProduct(product);
    }

    // PUT api/<ProductsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
        // edit the product
    }

    // DELETE api/<ProductsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        // remove the product
    }
}
