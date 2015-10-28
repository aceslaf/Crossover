

namespace CrossoverTask.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class SearchService : ISearchService
    {
        /// <summary>
        /// Executes an async search in the db, and mails the results.
        /// A boolean responce of the request validity is imidiatly returned
        /// </summary>
        /// <param name="searchParams">The search criteria, apikey and recipient email</param>
        /// <returns></returns>
        public bool Search(SearchInput searchParams)
        {
            //var searchProxy = new SearchImplementation();
            //return proxy.Search(searchParams);
            return false;
        }

    }
}

