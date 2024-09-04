using FaveShelf.Business.Services;
using FaveShelf.Data.Entities;
using FaveShelf.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaveShelf.Business.Managers
{
    public class SongManager : ISongService
    {
        private readonly ISongRepository _songRepository;

        public SongManager(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task AddSong(SongEntity song)
        {
            await _songRepository.AddSong(song);
        }

        public async Task<SongEntity> GetSongById(int songId)
        {
            return await _songRepository.GetSongById(songId);
        }

        public async Task<List<SongEntity>> GetTopSongs()
        {
            return await _songRepository.GetAllSongs(); // şarkıları veritabanındana al 
        }
    }
}
