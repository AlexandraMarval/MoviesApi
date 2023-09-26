using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Entidades;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/category")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CategoriesController(
            ApplicationDbContext context, 
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet(Name = "obtenerListadoDeCategorias")]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategoryList()
        {
            var category = await context.Categories.ToListAsync();
            return Ok(category);
        }

        [HttpPost(Name = "crearCategoria")]
        public async Task<ActionResult> CreateACategory(CategoryCreacionDTO categoryCreacionDTO)
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

        [HttpPut(Name = "ActualizarCategoria")]
        public async Task<ActionResult> UpdateCategory(CategoryCreacionDTO categoryCreacionDTO, int id)
        {
            var existetheMovie = await context.Categories.AnyAsync(category => category.Id == id);

            if (!existetheMovie)
            {
                return NotFound("No se puede actualizar");
            }
            var category = mapper.Map<Category>(categoryCreacionDTO);
            category.Id = id;

            context.Update(category);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var deleteMovie = await context.Categories.AnyAsync(category => category.Id == id);
            if (!deleteMovie)
            {
                return NotFound("No se puede eleminar la movie");
            }
            context.Remove(new Category() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
