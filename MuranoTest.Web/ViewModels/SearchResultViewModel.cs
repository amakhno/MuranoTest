using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuranoTest.Web.ViewModels
{
    public class SearchResultViewModel : SearchResult
    {
        public List<SearchConnectorSearchViewModel> Connectors { get; set; }
        
        public SearchResultViewModel()
        {

        }

        public SearchResultViewModel(SearchRequest searchRequest)
        {
            Query = searchRequest.Query;
            SystemIds = searchRequest.SystemIds;
        }

        public SearchResultViewModel(SearchResult searchRequest) : this((SearchRequest)searchRequest)
        {
            SearchSystem = searchRequest.SearchSystem;
            SearchPositions = searchRequest.SearchPositions;
        }
    }
}