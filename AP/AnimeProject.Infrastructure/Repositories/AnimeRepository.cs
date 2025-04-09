using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeProject.Domain.Entities;
using AnimeProject.Domain.Repositories;
using AnimeProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeProject.Infrastructure.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AppDbContext _context;

        public AnimeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Anime> GetByIdAsync(int id)
        {
            return await _context.Animes.FindAsync(id);
        }

        public async Task<IEnumerable<Anime>> GetAllAsync()
        {
            return await _context.Animes.ToListAsync();
        }

        public async Task AddAsync(Anime anime)
        {
            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Anime anime)
        {
            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var anime = await _context.Animes.FindAsync(id);
            if (anime != null)
            {
                _context.Animes.Remove(anime);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Anime>> SearchByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return new List<Anime>();
            }

            var name = title.Trim().ToLower();
            return await _context.Animes
                .Where(a => a.Title != null && a.Title.ToLower().Contains(name))
                .ToListAsync();
        }
    }
}