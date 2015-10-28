
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrossoverTask.Data;
using CrossoverTask.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CrossoverTask.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        
        protected CrosoverTaskDataContex DataContext { get; private set; }
        protected ApplicationDbContext ApplicationDbContext { get; private set; }
        protected UserManager<ApplicationUser> UserManager { get; private set; }
        public BaseController()
        {
            DataContext = new CrosoverTaskDataContex();
            ApplicationDbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }


        protected override void Dispose(bool disposing)
        {
            if (DataContext != null)
            {
                DataContext.Dispose();
            }

            if (ApplicationDbContext != null)
            {
                ApplicationDbContext.Dispose();
            }

            if (UserManager != null)
            {
                UserManager.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
