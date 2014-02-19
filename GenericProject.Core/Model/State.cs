using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericProject.Core.Model
{
    public class State : AuditedModel
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
