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
        public string Pocetok { get; set; }
        public string Kraj { get; set; }
        public string Datum { get; set; }
        public string Ispit { get; set; }
        public string PopravenIspit { get; set; }

    }
}