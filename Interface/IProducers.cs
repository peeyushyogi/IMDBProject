using DeltaXProject.Model;
using DeltaXProject.ClientModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaXProject.Interface
{
    public interface IProducers
    {
        Task<IActionResult> GetProducers();
        Task<IActionResult> GetProducers(int? Id);
        tblProducers CreateProducers(tblProducers tblProducer);
        IActionResult EditProducer(Producer producer);
    }
}
