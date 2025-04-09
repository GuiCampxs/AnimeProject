using Microsoft.AspNetCore.Mvc;
using AnimeProject.Application.Dtos;
using AnimeProject.Application.Services;

namespace AnimeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimesController : ControllerBase
    {
        private readonly IAnimeService _animeService;

        public AnimesController(IAnimeService animeService)
        {
            _animeService = animeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var anime = await _animeService.GetByIdAsync(id);
            if (anime == null)
            {
                return BadRequest("Anime not found or does not exist");
            } 

            return Ok(anime);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var animes = await _animeService.GetAllAsync();
            if (animes == null) 
            {
                return NotFound("There is no Anime registered");
            }
            
            return Ok(animes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AnimeRequest animeDto)
        {
            var createdAnime = await _animeService.AddAsync(animeDto);
            if(createdAnime == null)
            {
                return BadRequest("Anime could not be created");
            }

            return Ok(createdAnime);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AnimeDto animeDto)
        {
            var existingAnime = await _animeService.GetByIdAsync(id);
            if (existingAnime == null)
            {
                return NotFound("Anime not found");
            }

            await _animeService.UpdateAsync(id, animeDto);
            var updatedAnime = await _animeService.GetByIdAsync(id);
            return Ok(updatedAnime);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var anime = await _animeService.GetByIdAsync(id);
            if (anime == null) return NotFound();

            await _animeService.DeleteAsync(id);
            return Ok("Anime deleted sucessfully");
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByTitle([FromQuery] string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Title parameter is required.");
            }

            var animes = await _animeService.SearchByTitleAsync(title);
            return Ok(animes);
        }
    }
}
