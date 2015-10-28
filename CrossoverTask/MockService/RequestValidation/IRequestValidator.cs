using MockService.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverTask.Service.RequestValidation
{
    public interface IRequestValidator
    {
        bool IsSearchCriteriaValid(SearchParams searchParams);
        bool IsAuthorized(SearchParams searchParams);
    }
}
