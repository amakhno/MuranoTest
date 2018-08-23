using Interfaces.Repositories;
using Interfaces.Services;
using Models;
using Murano.UnitTests.Helpers;
using NSubstitute;
using NUnit.Framework;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Murano.UnitTests
{
    [TestFixture]
    public class SearchServiceUnitTest
    {
        [Test]
        public void GetResults_EmptyQuery_ReturnEmptyResult()
        {
            var logger = Substitute.For<ILoggerService>();
            var searchConnector = Substitute.For<ISearchConnectorRepository>();
            var searchResultPositionRepository = Substitute.For<ISearchResultPositionRepository>();
            SearchService searchService = new SearchService(searchConnector,
                logger, searchResultPositionRepository);
            var result = searchService.GetResults(new SearchRequest() { SystemIds = new int[] { 1 } });
            Assert.AreEqual(result.ToString(), (new SearchResult()).ToString());
        }

        [Test]
        public void GetResults_EmptySystemIDs_ReturnEmptyResult()
        {
            var logger = Substitute.For<ILoggerService>();
            var searchConnector = Substitute.For<ISearchConnectorRepository>();
            var searchResultPositionRepository = Substitute.For<ISearchResultPositionRepository>();
            SearchService searchService = new SearchService(searchConnector,
                logger, searchResultPositionRepository);
            var result = searchService.GetResults(new SearchRequest() { Query = "query" });
            Assert.AreEqual(result.ToString(), (new SearchResult()).ToString());
        }

        [Test]
        public void GetResults_HaveCahced_ReturnCached()
        {
            var logger = Substitute.For<ILoggerService>();
            var searchConnector = Substitute.For<ISearchConnectorRepository>();
            var searchResultPositionRepository = Substitute.For<ISearchResultPositionRepository>();

            var cachedResult = new List<SearchResultPosition>();
            cachedResult.Add(new SearchResultPosition()
            {
                Date = DateTime.UtcNow,
                Description = "Hi",
                Id = 1,
                Label = "Label",
                Link = "http://",
                Query = "Query",
                SearchConnectorId = 1
            });

            searchResultPositionRepository.Get(Arg.Any<Func<SearchResultPosition, bool>>()).Returns(
                cachedResult.AsQueryable()
                );

            SearchService searchService = new SearchService(searchConnector, logger, searchResultPositionRepository);

            var result = searchService.GetResults(new SearchRequest() { Query = "query", SystemIds = new int[] { 1 } });

            Assert.AreEqual(result.SearchPositions.First().ToString(), cachedResult.First().ToString());
        }

        [Test]
        public void GetResults_NoCahced_CallGetPositions()
        {
            var logger = Substitute.For<ILoggerService>();
            var searchConnector = Substitute.For<ISearchConnectorRepository>();
            var searchResultPositionRepository = Substitute.For<ISearchResultPositionRepository>();

            var cachedResult = new List<SearchResultPosition>();

            searchResultPositionRepository.Get(Arg.Any<Func<SearchResultPosition, bool>>()).Returns(
                cachedResult.AsQueryable()
                );

            var connectors = new List<SearchConnector>();
            connectors.Add(new SearchConnector() {
                Id = 1,
                Name = "Google",
                QueryPattern = "https://www.google.ru/search?q=%query%",
                ResultNamePattern = ".r a",
                ResultBodyPattern = ".s .st",
                ResultLinkPattern = ".r a",
                ResultPattern = ".g",
                DirectLink = "https://www.google.ru"
            });

            searchConnector.Get(Arg.Any<Func<SearchConnector, bool>>()).Returns(connectors.AsQueryable());

            SearchServiceWithFake searchService = new SearchServiceWithFake(searchConnector, logger, searchResultPositionRepository);
            searchService.SetResult(new SearchResult());

            var result = searchService.GetResults(new SearchRequest() { Query = "query", SystemIds = new int[] { 2 } });

            Assert.AreEqual(searchService.isCalled, true);
        }

        [Test]
        public void GetResults_BadQueryPattern_WriteLog()
        {
            var logger = Substitute.For<ILoggerService>();
            var searchConnector = Substitute.For<ISearchConnectorRepository>();
            var searchResultPositionRepository = Substitute.For<ISearchResultPositionRepository>();

            var cachedResult = new List<SearchResultPosition>();

            searchResultPositionRepository.Get(Arg.Any<Func<SearchResultPosition, bool>>()).Returns(
                cachedResult.AsQueryable()
                );

            var connectors = new List<SearchConnector>();
            connectors.Add(new SearchConnector()
            {
                Id = 1,
                Name = "Google",
                QueryPattern = "p://www.google.ru/search?q=%query%",
                ResultNamePattern = ".r a",
                ResultBodyPattern = ".s .st",
                ResultLinkPattern = ".r a",
                ResultPattern = ".g",
                DirectLink = "https://www.google.ru"
            });

            searchConnector.Get(Arg.Any<Func<SearchConnector, bool>>()).Returns(connectors.AsQueryable());

            SearchService searchService = new SearchService(searchConnector, logger, searchResultPositionRepository);   

            var result = searchService.GetResults(new SearchRequest() { Query = "query", SystemIds = new int[] { 1 } });

            logger.Received().Write(Arg.Any<string>());
        }

        [Test]
        public void GetResults_LoadedFromWeb_InsertInRepository()
        {
            var logger = Substitute.For<ILoggerService>();
            var searchConnector = Substitute.For<ISearchConnectorRepository>();
            var searchResultPositionRepository = Substitute.For<ISearchResultPositionRepository>();            

            var connectors = new List<SearchConnector>();
            connectors.Add(new SearchConnector()
            {
                Id = 1,
                Name = "Google",
                QueryPattern = "p://www.google.ru/search?q=%query%",
                ResultNamePattern = ".r a",
                ResultBodyPattern = ".s .st",
                ResultLinkPattern = ".r a",
                ResultPattern = ".g",
                DirectLink = "https://www.google.ru"
            });

            var searchResult = new SearchResult() { Query = "query", SystemIds = new int[] { 1 }, SearchSystem = 1 };
            searchResult.SearchPositions.Add(new SearchResultPosition()
            {
                Date = DateTime.UtcNow,
                Description = "Hi",
                Id = 1,
                Label = "Label",
                Link = "http://",
                Query = "Query",
                SearchConnectorId = 1
            });

            searchConnector.Get(Arg.Any<Func<SearchConnector, bool>>()).Returns(connectors.AsQueryable());
            searchResultPositionRepository.Get(Arg.Any<Func<SearchResultPosition, bool>>()).Returns(
                new List<SearchResultPosition>().AsQueryable()
                );

            SearchServiceWithFake searchService = new SearchServiceWithFake(searchConnector, logger, searchResultPositionRepository);
            searchService.SetResult(searchResult);    

            var result = searchService.GetResults(new SearchRequest() { Query = "query", SystemIds = new int[] { 1 } });

            searchResultPositionRepository.Received().Insert(Arg.Any<SearchResultPosition>());
        }
    }
}
