
using System.Collections.Generic;
using DeltaXProject.Model;
using DeltaXProject.ClientModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeltaXProject.Interface
{
   public interface IMovie
    {
        Task<IActionResult> GetMovies();
        bool CreateMovie(Movie obj);
        IActionResult EditMovie(Movie obj);
        IActionResult DeleteMovie(int Id);

        
    }
}
