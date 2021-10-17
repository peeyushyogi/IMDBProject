using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaXProject.Model
{
    public class tblMovie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plot { get; set; }
        public string DateOfRelease { get; set; }
        public  int ProducerId { get; set; } 
        public int ActorMapId { get; set; }
        
        
    }
}
