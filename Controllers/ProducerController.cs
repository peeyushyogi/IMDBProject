using DeltaXProject.ClientModel;
using DeltaXProject.Interface;
using DeltaXProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase, IProducers
    {
        IDBRepository dBRepository;
        public ProducerController(IDBRepository DbRepo)
        {
            dBRepository = DbRepo;
        }
        [HttpPost]
        public tblProducers CreateProducers(tblProducers tblProducer)
        {
            dBRepository.tblProducers.Add(tblProducer);
            dBRepository.SaveChanges();
            return tblProducer;
        }

        public IActionResult EditProducer(Producer producer)
        {

            tblProducers tblProducer = new tblProducers()
            {
                 Id = producer.Id,
                  Bio = producer.Bio,
                   Company = producer.Company,
                    DOB = producer.DOB,
                     Gender = producer.Gender,
                      Name = producer.Name
            };
            dBRepository.tblProducers.Update(tblProducer);
            dBRepository.SaveChanges();
            return Ok("Edit Producer Successfully");
        }

        public async Task<IActionResult> GetProducers(int? Id)
        {
            return Ok(await dBRepository.tblProducers.Where(x=>x.Id==Id).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetProducers()
        {
            return Ok(await dBRepository.tblProducers.ToListAsync());
        }
    }
}
