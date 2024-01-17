using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFirst.Models;

namespace WebApiFirst.Controllers
{
   // [Route("api/[controller]/[action]")]

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        AppDbContext _db;
        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetCategories")]
        //[Produces("application/xml")]
        public IActionResult GetCategories( int version=1)
        {
            if(version == 1)
            {
                var categories = _db.Categories.ToList();
                return Ok(categories);
            }
            else if (version == 2)
            {
                var categories = _db.Categories.Where(c=>c.IsActive).ToList();
                return Ok(categories);
            }
           
            return Ok(null);
        }

        //[HttpGet(Name = "GetAllAsync")]
        //public async Task<IActionResult> GetAllAsync(string type)
        //{
        //         var categories = await _db.Categories.ToListAsync();
        //         return Ok(categories);
        //}
        //[HttpPost]
        //public IActionResult Create(Category category)
        //{
        //     _db.Categories.Add(category);
        //    _db.SaveChanges();

        //    return Ok(category);
        //   // return Created("GetAllCategories",null);
        //}

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id <= 0)
            
                return BadRequest("Please correct category Id");

                Category cat = _db.Categories.Find(id);
                if (cat != null)
                {
                    return Ok(cat);
                }
                return NotFound($"{id} category does not exists");
         }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type =typeof(Category))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]

        public IActionResult Create(CategoryModel category)
        {
           if(ModelState.IsValid)
            {
                try
                {
                    Category cat = new Category()
                    {
                        Name = category.Name,
                        IsActive = category.IsActive
                    };
                    _db.Categories.Add(cat);
                    _db.SaveChanges();
                    return Created("Create",cat);
                }
                catch(Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, 
                        ex.Message);
                }
            }
           return BadRequest("Please Check Input Data");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult Update(int id ,CategoryModel category)
        {
            if (id != category.Id)
                return BadRequest("Please correct category Id");
            if (ModelState.IsValid)
            {
                try
                {

                    Category cat = new Category()
                    {
                        Id = id,
                        Name = category.Name,
                        IsActive = category.IsActive
                    };
                    _db.Categories.Update(cat);
                    _db.SaveChanges();
                    return Ok(cat);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult Delete(int id)
        {

            if (id <=0)
                return BadRequest("Please Correct Category Id");

            if (ModelState.IsValid)
            {
                try
                {
                     Category cat = _db.Categories.Find(id);
                    _db.Categories.Remove(cat);
                    _db.SaveChanges();
                    return Ok(cat);
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
