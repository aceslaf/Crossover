using CrossoverTask.Web.Models;
using Microsoft.AspNet.Identity;
using MockService.SearchImplementation;
using System.Web.Mvc;
using MockService.Searching;
using System;
using System.Configuration;

namespace CrossoverTask.Web.Controllers
{
    public class SearchController : BaseController
    {
        [Authorize]
        public ActionResult Search()
        {
            return View(new SearchViewModel
            {
                Email = GetUserEmail()
            });
        }

        [HttpPost]
        [Authorize]
        public  ActionResult Search(SearchViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMsg = "Invalid search params ";
                return View("Search", viewModel);
            }

            if (!viewModel.HasAtLeastOneCriteria)
            {
                ViewBag.ErrorMsg = "At least one search criteria is needed";
                return View("Search", viewModel);
            }

            //The call should look something like this:
            //WcfServiceLibrary2.SearchService.ServiceClient client= new SearchService.ServiceClient();   
            //Due to problems with adding service references, I'll mock it with another project

            var proxy = new SearchImplementation();
            bool searchSuccessfullySubmited = proxy.Search(CreateSerchParams(viewModel));

            if (searchSuccessfullySubmited)
            {
                ViewBag.Msg = "Search results will be sent to email " + viewModel.Email;
                return View("Search", new SearchViewModel { Email = GetUserEmail() });
            }
            else
            {
                ViewBag.ErrorMsg = "Search query was not sent succesfully, please try again later ";
                return View("Search", viewModel);
            }
            
            
        }

        private SearchParams CreateSerchParams(SearchViewModel searchParams)
        {
            return new SearchParams
            {
                Gender = searchParams.Gender,
                Name = searchParams.FirstName,
                Surname = searchParams.LastName,
                AgeStart = searchParams.MinAge,
                AgeEnd = searchParams.MaxAge,
                WeightStart = searchParams.MinWeight,
                WeightEnd = searchParams.MaxWeight,
                HeightStart = searchParams.MinHeight,
                HeightEnd = searchParams.MaxHeight,
                MaxNumberOfResults = searchParams.MaxResults,
                RecieverEmail = searchParams.Email,
                SecretKey = ConfigurationSettings.AppSettings["SearchServiceKey"]

            };
        }

        private string GetUserEmail()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.Email;
            }

            return "";
        }
    }
}