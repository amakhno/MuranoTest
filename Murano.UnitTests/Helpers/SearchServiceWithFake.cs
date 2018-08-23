using Interfaces.Repositories;
using Interfaces.Services;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murano.UnitTests.Helpers
{
    class SearchServiceWithFake : SearchService
    {
        private SearchResult _searchResult;

        public bool isCalled { get; private set; }

        public SearchServiceWithFake(ISearchConnectorRepository searchConnectorRepository, 
            ILoggerService loggerService, 
            ISearchResultPositionRepository searchResultPositionRepository) 
            : base(searchConnectorRepository, loggerService, searchResultPositionRepository)
        {
        }

        public void SetResult(SearchResult searchResult)
        {
            _searchResult = searchResult;
        }

        protected override void GetResultPosition(SearchConnector connector, string query, out SearchResult searchResult)
        {
            searchResult = this._searchResult;
            isCalled = true;
        }
    }
}
