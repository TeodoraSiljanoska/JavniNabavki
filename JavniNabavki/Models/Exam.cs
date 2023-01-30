using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace JavniNabavki.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Tip { get; set; }
        public string Mesec { get; set; }
        public DateTime Pocetok { get; set; }
        public DateTime Kraj { get; set; }
        public DateTime Datum { get; set; }
        public DateTime Ispit { get; set; }
        public DateTime PopravenIspit { get; set; }

    }
}