using FaveShelf.Data.Context;
using FaveShelf.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaveShelf.Data.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly FaveShelfContext _context;

        public SongRepository(FaveShelfContext context)
        {
            _context = context;
        }
        public async Task AddSong(SongEntity songEntity)
        {
            await _context.Songs.AddAsync(songEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SongEntity>> GetAllSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        public async Task<SongEntity> GetSongById(int id)
        {
            return await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
