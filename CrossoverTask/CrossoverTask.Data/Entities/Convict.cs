using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrossoverTask.Data.Entities
{
    public enum Gender
    {
        Male=1,
        Female=2,
    }
    public class Convict
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }        
        public string FullName { get { return String.Format("{0} {1}", this.FirstName, this.LastName); } }        
        public virtual ICollection<Crime> Crimes { get; set; }
    }
}