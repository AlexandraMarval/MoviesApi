using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Entidades;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return await context.Categories.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Category category)
        {
            context.Add(category);
            await context.SaveChangesAsync();
            return Ok(category);
        }
    }
}
