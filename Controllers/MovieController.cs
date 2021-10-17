using DeltaXProject.Interface;
using DeltaXProject.Model;
using DeltaXProject.ClientModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeltaXProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase, IMovie
    {
        DBRepository _dBRepository;
        public MovieController(DBRepository dBRepository)
        {
            _dBRepository = dBRepository;
        }
        [HttpPost]
        public bool CreateMovie(Movie obj)
        {
            bool IsMovieCreated = true;
            bool IsProducerIdExist = _dBRepository.tblProducers.Any(x => x.Id == obj.ProducerData.Id);
            if (IsProducerIdExist == true)
            {
                tblMovie MovieObject = new tblMovie()
                {
                    Name = obj.Name,
                    Plot = obj.Plot,
                    ProducerId = obj.ProducerData.Id,
                    DateOfRelease = obj.DateOfRelease,
                };
                Random random = new Random();
                int randomNumber = random.Next(1, 1000);
                MovieObject.ActorMapId = randomNumber;
                _dBRepository.tblMovie.Add(MovieObject);
                _dBRepository.SaveChanges();

                bool IsActorIdExist;
                foreach (Actor actor in obj.ActorData)
                {
                    IsActorIdExist = false;
                    IsActorIdExist = _dBRepository.tblActors.Any(x => x.Id == actor.Id);
                    if (IsActorIdExist == true)
                    {
                        tblMovieActorMap map = new tblMovieActorMap() { MovieId = MovieObject.Id, ActorId = actor.Id, MapId = MovieObject.ActorMapId };
                        _dBRepository.tblMovieActorMap.Add(map);
                        _dBRepository.SaveChanges();
                    }
                }
            }
            else
                IsMovieCreated = false;

            return IsMovieCreated;
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteMovie(int Id)
        {
            bool IsMovieExist = _dBRepository.tblMovie.Any(x => x.Id == Id);
            if (IsMovieExist == true)
            {
                tblMovie MovieObject = new tblMovie() { Id = Id };
                List<tblMovieActorMap> listMapMovieActor = _dBRepository.tblMovieActorMap.Where(x => x.MovieId == Id).ToList();
                foreach (tblMovieActorMap obj in listMapMovieActor)
                {
                    _dBRepository.tblMovieActorMap.Remove(obj);
                    _dBRepository.SaveChanges();
                }
                
                _dBRepository.tblMovie.Remove(MovieObject);
                _dBRepository.SaveChanges();
                
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public IActionResult EditMovie(Movie obj)
        {
            int ActorMappingId = _dBRepository.tblMovieActorMap.Where(k => k.MovieId == obj.Id).FirstOrDefault().MapId;

            tblMovie obj1 = new tblMovie() { 
             Id = obj.Id,
              DateOfRelease = obj.DateOfRelease,
               Name = obj.Name,
                Plot = obj.Plot,
                 ProducerId = obj.ProducerData.Id,
                 ActorMapId = ActorMappingId
            };
            
            List<tblMovieActorMap> RemovingObject = _dBRepository.tblMovieActorMap.Where(x => x.MapId == ActorMappingId).ToList();
            
              if (RemovingObject.Count > 0)
              {
                  foreach (tblMovieActorMap MappingObject in RemovingObject)
                  {
                      _dBRepository.tblMovieActorMap.Remove(MappingObject);
                      _dBRepository.SaveChanges();
                  }
              }
              
            foreach (Actor actor in obj.ActorData)
              {
                  tblMovieActorMap MapObject = new tblMovieActorMap() {
                   MapId = ActorMappingId,
                    ActorId = actor.Id,
                     MovieId = obj.Id
                  };
                  _dBRepository.tblMovieActorMap.Add(MapObject);
                  _dBRepository.SaveChanges();
              }
          
            _dBRepository.Entry(obj1).State = EntityState.Detached;
              _dBRepository.tblMovie.Update(obj1);
               _dBRepository.SaveChanges();
            return Ok(obj1);


        }
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            List<Movie> ListMovie = new List<Movie>();
           List<tblMovie> DbListMovies = await  _dBRepository.tblMovie.ToListAsync();
            foreach (tblMovie tblMovieobj in DbListMovies)
            {
                Movie movie = new Movie() 
                {
                     Name = tblMovieobj.Name,
                      DateOfRelease = tblMovieobj.DateOfRelease,
                       Plot = tblMovieobj.Plot,
                        Id = tblMovieobj.Id
                };
                tblProducers DbTableProducers = _dBRepository.tblProducers.Where(x => x.Id == tblMovieobj.ProducerId).FirstOrDefault();
                Producer producerData = new Producer()
                {
                    Id = DbTableProducers.Id,
                     Name = DbTableProducers.Name,
                      Bio = DbTableProducers.Bio,
                       Company = DbTableProducers.Company,
                        DOB = DbTableProducers.DOB,
                         Gender = DbTableProducers.Gender
                };
                var listMovieActorsMap = _dBRepository.tblMovieActorMap.Where(x => x.MapId == tblMovieobj.ActorMapId).ToList();
                List<Actor> ActorList = new List<Actor>();
                foreach (tblMovieActorMap obj in listMovieActorsMap)
                {
                    var ActorObj = _dBRepository.tblActors.Where(x => x.Id == obj.ActorId).FirstOrDefault();
                    Actor actor = new Actor()
                    {
                        Id      = ActorObj.Id,
                        Name    = ActorObj.Name,
                        Bio     = ActorObj.Bio,
                        DOB     = ActorObj.DOB,
                        Gender  = ActorObj.Gender
                    };
                    ActorList.Add(actor);
                }
                
                
                
                movie.ActorData = ActorList;
                movie.ProducerData = producerData;
                ListMovie.Add(movie);
               

            }
            return Ok(ListMovie);

        }
    }
}
