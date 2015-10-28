using CrossoverTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverTask.Data
{
   public class CrosoverTaskDataContex : DbContext
    {
        public CrosoverTaskDataContex():base("name=DefaultConnection")
        {
        }
        public DbSet<Convict> Convicts { get; set; }
        public DbSet<Crime> Crimes { get; set; }
    }
}
