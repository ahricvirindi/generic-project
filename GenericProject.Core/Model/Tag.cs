using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericProject.Core.Model
{
    public class Tag : LookupModel
    {
        public ICollection<Peep> Peeps { get; set; }
    }
}
