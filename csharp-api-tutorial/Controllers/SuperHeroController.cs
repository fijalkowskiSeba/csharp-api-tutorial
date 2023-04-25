using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace csharp_api_tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await this.context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero  = await this.context.SuperHeroes.FindAsync(id);
            if(hero == null)
            {
                return BadRequest("Hero not found.");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            this.context.SuperHeroes.Add(hero);
            await this.context.SaveChangesAsync();
            
            return Ok(await this.context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await this.context.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.SuperHeroes.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await this.context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }
            this.context.SuperHeroes.Remove(hero);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.SuperHeroes.ToListAsync());
        }
    }
}
