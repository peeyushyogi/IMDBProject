using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXProject.Model
{
    public class DBRepository : DbContext, IDBRepository
    {
        public DBRepository(DbContextOptions<DBRepository> options) : base(options)
        {
       
        }
        public DbSet<tblActors> tblActors { get; set; }
        public DbSet<tblProducers> tblProducers { get; set; }

        public DbSet<tblMovie> tblMovie { get; set; }

        public DbSet<tblMovieActorMap> tblMovieActorMap { get; set; }

        
        
    }
}
