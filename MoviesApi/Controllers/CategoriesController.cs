using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Entidades;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext context) 
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categories>>> GetCategories()
        {
           return await context.Categories.ToListAsync();       
        }

        [HttpPost]
        public async Task<ActionResult> PostCategories(Categories categories) 
        {
            context.Add(categories);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
