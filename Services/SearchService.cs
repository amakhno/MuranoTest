using Interfaces.Services;
using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

namespace Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchConnectorRepository _searchConnectorRepository;
        private readonly ILoggerService _loggerService;
        private readonly ISearchResultPositionRepository _searchResultPositionRepository;


        public SearchService(ISearchConnectorRepository searchConnectorRepository, ILoggerService loggerService,
            ISearchResultPositionRepository searchResultPositionRepository)
        {
            _searchConnectorRepository = searchConnectorRepository;
            _loggerService = loggerService;
            _searchResultPositionRepository = searchResultPositionRepository;
        }

        public SearchResult GetResults(SearchRequest searchRequest)
        {
            if (string.IsNullOrEmpty(searchRequest.Query))
            {
                return new SearchResult();
            }
            if (searchRequest.SystemIds == null || searchRequest.SystemIds.Count() == 0)
            {
                return new SearchResult();
            }

            var cachedResults = _searchResultPositionRepository.Get(
                x => searchRequest.SystemIds.Contains(x.SearchConnectorId)
                    && (x.Query == searchRequest.Query.ToLower()
                        || x.Description.ToLower().Contains(searchRequest.Query.ToLower()))
                ).OrderByDescending(x=>x.Date);

            if (cachedResults.Count() > 0)
            {
                var connectorId = cachedResults.First().SearchConnectorId;
                var positions = cachedResults.Where(x => x.SearchConnectorId == connectorId).Take(10);
                SearchResult searchResult = new SearchResult();
                searchResult.SearchPositions = positions.ToList();
                searchResult.SearchSystem = connectorId;
                return searchResult;
            }

            var connectors = _searchConnectorRepository.Get(x => searchRequest.SystemIds.Contains(x.Id)).ToList();
            int connectorsCount = connectors.Count;
            SearchResult[] results = new SearchResult[connectorsCount];
            SearchResult finalResult = new SearchResult();
            finalResult.SearchSystem = 1;

            List<Task> tasks = new List<Task>();

            for (int i = 0; i< connectorsCount; i++)
            {
                results[i] = new SearchResult();
                int currentIndex = i;
                var searchTask = Task.Run(()=>GetResultPosition(connectors[currentIndex], searchRequest.Query, out results[currentIndex]));
                tasks.Add(searchTask);
            }

            while (tasks.Count() > 0)
            {
                Task.WaitAny(tasks.ToArray(), 3000);
                if (results.Where(x=>x.SearchPositions.Count > 0).Count() > 0)
                {
                    finalResult = results.Where(x => x.SearchPositions.Count > 0).First();
                    break;
                }
                List<int> removeTasks = new List<int>();
                for (int i = 0; i< tasks.Count; i++)
                {
                    if (tasks[i].Status != TaskStatus.Running)
                    {
                        removeTasks.Add(i);
                    }                    
                }
                //Ни один таск не завершился => таймаут
                if (removeTasks.Count == 0)
                {
                    break;
                }
                removeTasks.Reverse();  //Если удалять таски то с конца, чтобы не нарушить очередность
                foreach (int number in removeTasks)
                {
                    tasks.Remove(tasks.ElementAt(number));
                }
            }

            if (finalResult.SearchPositions.Count > 0)
            {
                finalResult.SearchPositions = finalResult.SearchPositions.Take(10).ToList();
                foreach(var searchPosition in finalResult.SearchPositions)
                {
                    searchPosition.Query = searchRequest.Query.ToLower();
                    searchPosition.SearchConnectorId = finalResult.SearchSystem;
                    searchPosition.Date = DateTime.UtcNow;
                    _searchResultPositionRepository.Insert(searchPosition);
                } 
            }

            return finalResult;
        }

        protected virtual void GetResultPosition(SearchConnector connector, string query, out SearchResult searchResult)
        {
            SearchResult currentResult = new SearchResult();
            var url = connector.QueryPattern.Replace("%query%", query);
            var web = new HtmlWeb();
            HtmlDocument doc;
            try
            {
                if (url.IndexOf("http") != 0)
                {
                    throw new Exception("Bad QueryPattern");
                }
                doc = web.Load(url);
            }
            catch (Exception exc)
            {
                _loggerService.Write($"Error: {exc.ToString()}");

                    searchResult = new SearchResult { SearchSystem = connector.Id };
                return;
            }
            var document = doc.DocumentNode;
            if (!string.IsNullOrEmpty(connector.ResultPattern))
            {
                var resultNodes = document.QuerySelectorAll(connector.ResultPattern);
                foreach (var resultNode in resultNodes)
                {
                    string header = "", body = "", link = "";
                    if (!string.IsNullOrEmpty(connector.ResultNamePattern))
                    {
                        var node = resultNode.QuerySelector(connector.ResultNamePattern);
                        if (node != null)
                        {
                            header = node.InnerHtml;
                        }
                    }
                    if (!string.IsNullOrEmpty(connector.ResultBodyPattern))
                    {
                        var node = resultNode.QuerySelector(connector.ResultBodyPattern);
                        if (node != null)
                        {
                            body = node.InnerHtml;
                        }
                    }
                    if (!string.IsNullOrEmpty(connector.ResultLinkPattern))
                    {
                        var node = resultNode.QuerySelector(connector.ResultLinkPattern);
                        if (node != null)
                        {
                            link = node.GetAttributeValue("href", null);
                        }
                    }
                    if (!string.IsNullOrEmpty(link))
                    {
                        if (link.IndexOf("http") != 0)  //Если линк начинается не с http, то преобразуем его
                        {
                            link = connector.DirectLink + link;
                        }
                        
                        currentResult.SearchPositions.Add(new SearchResultPosition { Description = body.Replace("<br>", ""), Label = header, Link = link });
                    }
                }
            }

            currentResult.SearchSystem = connector.Id;

            searchResult = currentResult;

        }

        public IEnumerable<SearchConnector> GetSearchConnectors()
        {
            return _searchConnectorRepository.GetAll();
        }
    }
}
