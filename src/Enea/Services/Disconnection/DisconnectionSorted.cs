using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enea.Services
{
    public class DisconnectionSorted
    {
        public IEnumerable<Disconnection> Today { get; set; }
        public IEnumerable<Disconnection> Tommorow { get; set; }
        public IEnumerable<Disconnection> Others { get; set; }


        public DisconnectionSorted()
        {
            Today = new List<Disconnection>();
            Tommorow = new List<Disconnection>();
            Others = new List<Disconnection>();
        }
    }
}
