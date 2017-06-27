using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class ResultListViewModel<T>
    {
        private readonly IEnumerable<T> results;
        private readonly int pagesNumber;
        private readonly int currentPage;

        public ResultListViewModel(IEnumerable<T> results, int pagesNumber, int currentPage)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            this.results = results;
            this.pagesNumber = pagesNumber;
            this.currentPage = currentPage;
        }

        public IEnumerable<T> Results => results;
        public int PagesNumber => pagesNumber;
        public int CurrentPage => currentPage;
    }
}