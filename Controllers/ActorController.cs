using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DeltaXProject.Model;
using DeltaXProject.ClientModel;
using System.Collections.Generic;
using DeltaXProject.Interface;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeltaXProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase, IActor
    {
        IDBRepository dBRepository;
        public ActorController(IDBRepository dBRepository)
        {
            this.dBRepository = dBRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetActors()
        {
            return Ok(await this.dBRepository.tblActors.ToListAsync());
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetActors(int? Id)
        {
            return Ok( await this.dBRepository.tblActors.Where(x=>x.Id==Id).ToListAsync());
        }
        [HttpPost]
        public IActionResult CreateActor(tblActors tblActor)
        {
            
            this.dBRepository.tblActors.Add(tblActor);
            var context = dBRepository.SaveChanges();
            return Created(uri: "", tblActor);
        }

        public IActionResult EditActor(Actor actor)
        {
            
            tblActors DbTblObj = new tblActors() 
            {
                 Id = actor.Id,
                  Bio = actor.Bio,
                   DOB = actor.DOB,
                    Gender = actor.Gender,
                     Name = actor.Name
            };
            dBRepository.tblActors.Update(DbTblObj);
           dBRepository.SaveChanges();
           

            return Ok("Edited Successfully");
                
        }
    }
}
