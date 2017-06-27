using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class ResultViewModel<T>
    {
        private readonly IEnumerable<T> data;
        private readonly int pagesNumber;
        private readonly int currentPage;

        public ResultViewModel(IEnumerable<T> results, int pagesNumber, int currentPage)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            data = results;
            this.pagesNumber = pagesNumber;
            this.currentPage = currentPage;
        }

        public IEnumerable<T> Results => data;
        public int PagesNumber => pagesNumber;
        public int CurrentPage => currentPage;
    }
}