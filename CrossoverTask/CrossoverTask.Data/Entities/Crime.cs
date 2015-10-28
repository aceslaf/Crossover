using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrossoverTask.Data.Entities
{
    public class Crime
    {
        public int Id { get; set; }
        public int ConvictId { get; set; }
        public DateTime DateCommited { get; set; }
        public string Description { get; set; }
        public float Severity { get; set; }
        public string CrimeName { get; set; }
        public virtual Convict Convict { get; set; }
    }
}