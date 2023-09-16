using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Entidades;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/rental")]
    public class RentalsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public RentalsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("int:id")]
        public async Task<ActionResult> Get(int id)
        {
           var rental = context.Rentals.FirstOrDefaultAsync(rental => rental.Id == id);
           return Ok(rental);
        }

        [HttpGet("listado")]
        public async Task<ActionResult<List<Rental>>> Get()
        {
            return await context.Rentals.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Rental rental)
        {
            context.Add(rental);
            await context.SaveChangesAsync();
            return Ok(rental);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Rental rental)
        {
            context.Add(rental);
            await context.SaveChangesAsync();
            return Ok(rental);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Rental rental)
        {
            context.Remove(rental);
            await context.SaveChangesAsync();
            return Ok(rental);
        }
    }
}
