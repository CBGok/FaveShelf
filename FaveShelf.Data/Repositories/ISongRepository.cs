using FaveShelf.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaveShelf.Data.Repositories
{
    public interface ISongRepository
    {
        Task AddSong(SongEntity songEntity);
        Task<List<SongEntity>> GetAllSongs();
        Task<SongEntity> GetSongById(int id);
    }
}
