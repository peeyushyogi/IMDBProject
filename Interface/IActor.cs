using DeltaXProject.Model;
using DeltaXProject.ClientModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeltaXProject.Interface
{
    public interface IActor
    {
        Task<IActionResult> GetActors();
        Task<IActionResult> GetActors(int? Id);
        IActionResult CreateActor(tblActors tblActor);
        IActionResult EditActor(Actor actor);
    }
}
