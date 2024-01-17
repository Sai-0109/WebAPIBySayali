using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiFirst.Models;

namespace WebApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        AppDbContext _db;
        public ProductsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetAll")]
        public IActionResult GetAll()
        {
           
            var products = _db.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)

                return BadRequest("Please correct product Id");

            Product prod = _db.Products.Find(id);
            if (prod != null)
            {
                Category category = _db.Categories.Find(prod.CategoryId);
                prod.category = new Category()
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsActive = category.IsActive,

                };
                return Ok(prod);
            }
            return NotFound($"{id} product does not exists");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]

        public IActionResult Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product prod = new Product()
                    {
                        Name = product.Name,
                        UnitPrice = product.UnitPrice,
                        CategoryId =product.CategoryId
                    };
                    _db.Products.Add(prod);
                    _db.SaveChanges();
                    return Created("Create", prod);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        ex.Message);
                }
            }
            return BadRequest("Please Check Input Data");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult Update(int id, ProductModel product)
        {
            if (id != product.Id)
                return BadRequest("Please correct product Id");
            if (ModelState.IsValid)
            {
                try
                {

                    Product prod = new Product()
                    {
                        Id = id,
                        Name = product.Name,
                        UnitPrice = product.UnitPrice,
                        CategoryId = product.CategoryId
                    };
                    _db.Products.Update(prod);
                    _db.SaveChanges();
                    return Ok(prod);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       ex.Message);
                }
            }
            return BadRequest("Please Check Input Data");
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult Delete(int id)
        {

            if (id <= 0)
                return BadRequest("Please Correct Product Id");

            if (ModelState.IsValid)
            {
                try
                {
                    Product prod = _db.Products.Find(id);
                    _db.Products.Remove(prod);
                    _db.SaveChanges();
                    return Ok(prod);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        ex.Message);
                }
            }
            return BadRequest("Please Check Input Data");
        }


    }
}
