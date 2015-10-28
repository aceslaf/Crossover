using MockService.Searching;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverTask.Service.RequestValidation
{
    public class RequestValidator: IRequestValidator
    {
        /// <summary>
        /// Checks if the search parameters are valid (ex. within range, max>min etc.. )
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public bool IsSearchCriteriaValid(SearchParams searchParams)
        {
            //check if there is at least one search criteria
            if (!HasNonNullCriteria(searchParams))
            {
                return false;
            }

            //Check if request params are ok
            var context = new ValidationContext(searchParams, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(searchParams, context, validationResults, true);
            if (validationResults.Any(eror => eror != null))
            {
                return false;
            }

            return true;
        }

        private bool HasNonNullCriteria(SearchParams searchParams)
        {

            List<object> criteriaProperties = new List<object>();
            criteriaProperties.Add(searchParams.AgeEnd);
            criteriaProperties.Add(searchParams.AgeStart);
            criteriaProperties.Add(searchParams.HeightEnd);
            criteriaProperties.Add(searchParams.HeightStart);
            criteriaProperties.Add(searchParams.WeightEnd);
            criteriaProperties.Add(searchParams.WeightStart);
            criteriaProperties.Add(searchParams.Name);
            criteriaProperties.Add(searchParams.Surname);
            criteriaProperties.Add(searchParams.Gender);
            if (!criteriaProperties.Any(o => o != null))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks the api key of the request.
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public bool IsAuthorized(SearchParams searchParams)
        {
            //Idealy this would be a function of the request identity or some form of authentication/authorization
            //This is just to ilustrate the concept
            //The web service is configured to use https so no plain api keys are sent over the net

            string SecretKey = "70291a7ce563f15dcdd84c3091a52cc3";
            if (SecretKey.Equals(searchParams.SecretKey))
            {
                return true;
            }

            return false;
        }
        
    }
}
