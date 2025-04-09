using AnimeProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeProject.Domain.Repositories
{
    public interface IAnimeRepository
    {
        Task<Anime> GetByIdAsync(int id);
        Task<IEnumerable<Anime>> GetAllAsync();
        Task AddAsync(Anime anime);
        Task UpdateAsync(Anime anime);
        Task DeleteAsync(int id);
        Task<IEnumerable<Anime>> SearchByTitleAsync(string title);
    }
}
