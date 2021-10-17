using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace DeltaXProject.Model
{
    public interface IDBRepository
    {
       public  DbSet<tblActors> tblActors { get; set; }
        public DbSet<tblProducers> tblProducers { get; set; }
        public DbSet<tblMovie> tblMovie { get; set; }
        public DbSet<tblMovieActorMap> tblMovieActorMap { get; set; }
        int SaveChanges();
        //Task SaveChangesAsync();
        

    }
}