namespace CrossoverTask.Data.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CrossoverTask.Data.CrosoverTaskDataContex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CrossoverTask.Data.CrosoverTaskDataContex context)
        {
            var convicts = new List<Convict>();
            for (int i = 0; i < 1000; i++)
            {
                convicts.Add(new Convict
                {
                    FirstName = "FirstName" + i,
                    LastName = "LastName" + i,
                    DateOfBirth = DateTime.Now.AddYears((-1 * (i % 80)) - 20),
                    Gender = i % 2 == 0 ? Gender.Male : Gender.Female,
                    Height = 160 + i % 40,
                    Weight = 60 + i % 60,
                    Crimes = new List<Crime> {
                         new Crime {
                             CrimeName="Something terible",
                             DateCommited = DateTime.Now.AddYears((-1*(i%80))-10),
                             Description ="Stole "+i+" stuf",
                             Severity = 1/(i+1)
                         },
                         new Crime {
                             CrimeName="Something not so terible",
                             DateCommited = DateTime.Now.AddYears((-1*(i%80))-10),
                             Description ="Stole "+i+" flowers",
                             Severity = 1/(i+1)
                         },
                         new Crime {
                             CrimeName="Something not so terible",
                             DateCommited = DateTime.Now.AddYears((-1*(i%80))-10),
                             Description ="Hacked "+i+" pcs",
                             Severity = 1/(i+1)
                         },
                       }
                });
            }
            context.SaveChanges();
        }
    }
}
