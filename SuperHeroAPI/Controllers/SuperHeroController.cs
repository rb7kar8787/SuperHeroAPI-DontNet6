using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;


namespace SuperHeroAPI.Controllers
{

    [Route("api/[controller]")]

    [ApiController]

    public class SuperHeroController : ControllerBase
    {

    

        private static List<SuperHero> heroes = new List<SuperHero>
           {  new SuperHero
             {
               ID = 1, Name ="spider man",
                firstname="peter",
                lastname="parker",
                Place="New York City"
             },
           new SuperHero
           {
               ID=2, Name="IronMan",
               firstname="Tony",
               lastname="stark",
               Place="california"
           }
           };

        public DataContext Context { get; }

        public SuperHeroController(DataContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async  Task<ActionResult<List<SuperHero>>> Get()
        { 
              return Ok(await Context.SuperHeroes.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get( int id)
        {
            var hero = await Context.SuperHeroes.FindAsync(id);
            if (hero == null)
               return BadRequest("Hero Not Found");
            return Ok(hero);
        }
        [HttpPost]

        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
             Context.SuperHeroes.Add(hero
                 );
            await Context.SaveChangesAsync();
            return Ok(await Context.SuperHeroes.ToListAsync());
        }

        [HttpPut]

        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await Context.SuperHeroes.FindAsync(request.ID);
            if (hero == null)
                return BadRequest("Hero You enter is Not Found");
            else
            {
                hero.Name=request.Name;
                hero.firstname = request.firstname;
                hero.lastname=request.lastname;
                hero.Place=request.Place;
                await Context.SaveChangesAsync();

            }
            return Ok(await Context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await Context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero Not Found");
            Context.SuperHeroes.Remove(hero);
            await Context.SaveChangesAsync();
            return Ok(Context.SuperHeroes.ToListAsync());

        }
    }
}
