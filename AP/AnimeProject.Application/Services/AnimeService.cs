using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeProject.Application.Dtos;
using AnimeProject.Domain.Entities;
using AnimeProject.Domain.Repositories;

namespace AnimeProject.Application.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeService(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<AnimeDto> GetByIdAsync(int id)
        {
            var anime = await _animeRepository.GetByIdAsync(id);
            if (anime == null)
            {
                return null;
            }
            return new AnimeDto
            {
                Title = anime.Title,
                Description = anime.Description,
                Genre = anime.Genre
            };
        }

        public async Task<IEnumerable<AnimeDto>> GetAllAsync()
        {
            var animes = await _animeRepository.GetAllAsync();
            return animes.Select(a => new AnimeDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Genre = a.Genre
            });
        }

        public async Task<AnimeDto> AddAsync(AnimeRequest animeDto)
        {
            var anime = new Anime
            {
                Title = animeDto.Title,
                Description = animeDto.Description,
                Genre = animeDto.Genre
            };
            await _animeRepository.AddAsync(anime);

            return new AnimeDto
            {
                Id = anime.Id,
                Title = anime.Title,
                Description = anime.Description,
                Genre = anime.Genre
            };
        }

        public async Task DeleteAsync(int id)
        {
            await _animeRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(int id, AnimeDto animeDto)
        {
            var existingAnime = await _animeRepository.GetByIdAsync(id);
            if (existingAnime == null)
            {
                throw new KeyNotFoundException($"Anime with ID {id} not found.");
            }

            existingAnime.Title = animeDto.Title;
            existingAnime.Description = animeDto.Description;
            existingAnime.Genre = animeDto.Genre;

            await _animeRepository.UpdateAsync(existingAnime);
        }

        public async Task<IEnumerable<Anime>> SearchByTitleAsync(string title)
        {
            var animes = await _animeRepository.SearchByTitleAsync(title);
            return animes.Select(a => new Anime
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Genre = a.Genre
            });
        }
    }
}
