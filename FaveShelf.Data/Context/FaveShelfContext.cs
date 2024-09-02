using FaveShelf.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaveShelf.Data.Context
{
    public class FaveShelfContext : DbContext
    {
        public FaveShelfContext(DbContextOptions<FaveShelfContext> options) : base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<SongEntity> Songs => Set<SongEntity>(); // Şarkılar için DbSet
    }
}
