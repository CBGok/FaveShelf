using FaveShelf.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaveShelf.Business.Services
{
    public interface ISongService
    {
        Task<SongEntity> GetSongById(int songId);
        Task<List<SongEntity>> GetTopSongs();
        Task AddSong(SongEntity song);
    }
}
