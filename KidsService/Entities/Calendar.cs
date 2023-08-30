using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsService
{
    public class Calendar
    {
        [Key]
        public int Id { get; set; }
        public Kid Kid { get; set; }
        public DateTime Date { get; set; }
        public string Hours { get; set; }
        public double Price { get; set; }
        public Boolean IsDefinitive { get; set; }
    }
}
