using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericProject.Core.Model
{
    public class Peep : ModelBase
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
    }
}
