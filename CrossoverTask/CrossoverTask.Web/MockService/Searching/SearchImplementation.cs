using CrossoverTask.Data;
using CrossoverTask.Data.Entities;
using CrossoverTask.Service.Emailing;
using CrossoverTask.Service.RequestValidation;
using CrossoverTask.Service.Utility;
using MockService.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockService.SearchImplementation
{
    public class SearchImplementation
    {
        private const int MaxHumanAge = 1000;
        private IRequestValidator Validator { get; set; }
        private ResultsMailer mailer { get; set; }
        public SearchImplementation()
        {
            //Idealy this would be injected
            Validator = new RequestValidator();
            mailer = new ResultsMailer();
        }

        /// <summary>
        /// Executes an async search in the db, and mails the results.
        /// A boolean responce of the request validity is imidiatly returned
        /// </summary>
        /// <param name="searchParams">The search criteria, apikey and recipient email</param>
        /// <returns></returns>
        public bool Search(SearchParams searchParams)
        {

            if (!Validator.IsAuthorized(searchParams))
            {
                return false;
            }

            if (!Validator.IsSearchCriteriaValid(searchParams))
            {
                return false;
            }
        
            Task.Run(() =>
            {
                ExecuteSearchAndMail(searchParams);
            });        

            return true;
        }

        private void ExecuteSearchAndMail(SearchParams searchParams)
        {
            var filteredResults = FilterData(searchParams);
            mailer.SendSearchResults(filteredResults, searchParams.RecieverEmail);
        }

        private List<Convict> FilterData(SearchParams searchParams)
        {
            
             using (var dataContext = new CrosoverTaskDataContex())
            {
                IQueryable<Convict> filteredQueriable = dataContext.Convicts.Include("Crimes");

                if (searchParams.AgeStart.HasValue)
                {
                    DateTime end = DateTime.Now.AddYears(-1 * searchParams.AgeStart.Value);
                    filteredQueriable = filteredQueriable.Where(x => x.DateOfBirth < end);
                }

                if (searchParams.AgeEnd.HasValue)
                {
                    DateTime start = DateTime.Now.AddYears(-1 * searchParams.AgeEnd.Value);
                    filteredQueriable = filteredQueriable.Where(x => x.DateOfBirth > start);
                }

                if (searchParams.HeightStart.HasValue)
                {
                    float start = searchParams.HeightStart.Value;
                    filteredQueriable = filteredQueriable.Where(x => x.Height >= start);
                }

                if (searchParams.HeightEnd.HasValue)
                {
                    float end = searchParams.HeightEnd.Value;
                    filteredQueriable = filteredQueriable.Where(x => x.Height <= end);
                }

                if (searchParams.WeightStart.HasValue)
                {
                    float start = searchParams.WeightStart.Value;
                    filteredQueriable = filteredQueriable.Where(x => x.Weight >= start);
                }

                if (searchParams.WeightEnd.HasValue)
                {
                    float end = searchParams.WeightEnd.Value;
                    filteredQueriable = filteredQueriable.Where(x => x.Weight <= end);
                }

                if (searchParams.Name != null)
                {
                    filteredQueriable = filteredQueriable.Where(x => x.FirstName.ToLower().Contains(searchParams.Name.ToLower()));
                }

                if (searchParams.Surname != null)
                {
                    filteredQueriable = filteredQueriable.Where(x => x.LastName.ToLower().Contains(searchParams.Surname.ToLower()));
                }

                if (searchParams.Gender.HasValue)
                {
                    filteredQueriable = filteredQueriable.Where(x => x.Gender == searchParams.Gender);
                }

                return filteredQueriable.Take(searchParams.MaxNumberOfResults).ToList();
            }
        }
    }
}
