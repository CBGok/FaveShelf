using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaveShelf.Data.Entities
{
    public class SongEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }
}
