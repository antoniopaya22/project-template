using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCore_API_REST_Template.Model
{
    public class Combinado
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public User[] Users { get; set; }
    }
}
