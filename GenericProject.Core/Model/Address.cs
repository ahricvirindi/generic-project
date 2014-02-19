using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericProject.Core.Model
{
    public class Address : AuditedModel
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }

        public string City { get; set; }
        public string ZipCode { get; set; }

        public long AddressTypeId { get; set; }
        [ForeignKey("AddressTypeId")]
        public AddressType AddressType { get; set; }

        public long StateId { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }

        public long PeepId { get; set; }
        [ForeignKey("PeepId")]
        public Peep Peep { get; set; }
    }
}
