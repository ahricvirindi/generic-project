using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericProject.Core.Model
{
    public class Peep : AuditedModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public string Note { get; set; }
        public int HatsOwned { get; set; }
        public DateTime? Birthday { get; set; }

        public long? TitleId { get; set; }
        [ForeignKey("TitleId")]
        public Title Title { get; set; }

        public ICollection<Relation> Relations { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
