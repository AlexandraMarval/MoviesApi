using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Entidades;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return await context.Categories.ToListAsync();
        }

        [HttpPost(Name = "crearCategoria")]
        public async Task<ActionResult> Post(CategoryCreacionDTO categoryCreacionDTO)
        {
            var existeYaEsaCategorria = await context.Categories.AnyAsync(category => category.Name == categoryCreacionDTO.Name);

            if (existeYaEsaCategorria)
            {
                return BadRequest($"Ya esta categoria existe{categoryCreacionDTO.Name}");
            }

            var category = mapper.Map<Category>(categoryCreacionDTO);

            context.Add(category);
            await context.SaveChangesAsync();
            return Ok(category);
        }
    }
}
