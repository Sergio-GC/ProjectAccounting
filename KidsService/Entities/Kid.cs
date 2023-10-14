using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsService
{
    public class Kid
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Name { get; set;}
        public string LastName { get; set;}
        public DateTime Birthdate { get; set;}
        public virtual List<Kid>? Siblings { get; set;}
    }
}
