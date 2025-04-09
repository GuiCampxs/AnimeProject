using AnimeProject.Application.Dtos;
using AnimeProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeProject.Application.Services
{
    public interface IAnimeService
    {
        Task<AnimeDto> GetByIdAsync(int id);
        Task<IEnumerable<AnimeDto>> GetAllAsync();
        Task<AnimeDto> AddAsync(AnimeRequest animeDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, AnimeDto animeDto);
        Task<IEnumerable<Anime>> SearchByTitleAsync(string title);
    }
}
