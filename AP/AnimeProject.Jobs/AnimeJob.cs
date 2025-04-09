using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeProject.Application.Dtos;
using AnimeProject.Application.Services;
using System.Net.Http.Json;

namespace AnimeProject.Jobs
{
    public class AnimeJob
    {
        private readonly IAnimeService _animeService;
        private readonly HttpClient _httpClient;

        public AnimeJob(IAnimeService animeService, HttpClient httpClient)
        {
            _animeService = animeService;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.jikan.moe/v4/");
        }

        public async Task Execute()
        {
            var response = await _httpClient.GetFromJsonAsync<JikanResponse>("anime");
            if (response?.Data != null)
            {
                foreach (var anime in response.Data)
                {
                    var animeDto = new AnimeRequest
                    {
                        Title = anime.Title,
                        Description = anime.Synopsis,
                        Genre = string.Join(", ", anime.Genres.Select(g => g.Name))
                    };
                    await _animeService.AddAsync(animeDto);
                }
            }
        }
    }

    public class JikanResponse
    {
        public List<JikanAnime>? Data { get; set; }
    }

    public class JikanAnime
    {
        public string? Title { get; set; }
        public string? Synopsis { get; set; }
        public List<JikanGenre>? Genres { get; set; }
    }

    public class JikanGenre
    {
        public string? Name { get; set; }
    }
}
