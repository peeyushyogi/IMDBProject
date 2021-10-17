using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXProject.ClientModel
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plot { get; set; }
        public string DateOfRelease { get; set; }
        public Producer ProducerData { get; set; }
        public List<Actor> ActorData { get; set; }
    }
}
