﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaveShelf.Business.Dtos
{
    public class FavoriteSongDto
    {
        public int UserId { get; set; } // bir de böyle deneyelim
        public int SongId { get; set; }
    }
}
