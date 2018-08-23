using AutoMapper;
using Interfaces.Services;
using Models;
using MuranoTest.Web;
using MuranoTest.Web.Controllers;
using MuranoTest.Web.ViewModels;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murano.UnitTests
{
    [TestFixture]
    public class HomeControllerUnitTests
    {
        public void AutoMapperConfigure()
        {
            Mapper.Initialize(AutoMapperConfig.GetInitializer);
        }

        [TearDown]
        public void AutoMapperClear()
        {
            Mapper.Reset();
        }

        [Test]
        public void Index_WithoutParams_ReturnDefaultView()
        {
            AutoMapperConfigure();

            var searchService = Substitute.For<ISearchService>();
            var searchConnectors = new List<SearchConnector>();
            searchConnectors.Add(new SearchConnector()
            {
                Id = 1,
                Name = "Google",
                QueryPattern = "https://www.google.ru/search?q=%query%",
                ResultNamePattern = ".r a",
                ResultBodyPattern = ".s .st",
                ResultLinkPattern = ".r a",
                ResultPattern = ".g",
                DirectLink = "https://www.google.ru"
            });
            searchService.GetSearchConnectors().Returns(searchConnectors);
            HomeController homeController = new HomeController(searchService);

            var result = homeController.Index() as System.Web.Mvc.ViewResult;

            Assert.AreEqual("", result.ViewName);
        }

        [Test]
        public void Index_WithoutParams_ReturnPartialView()
        {
            AutoMapperConfigure();

            var searchService = Substitute.For<ISearchService>();
            var searchConnectors = new List<SearchConnector>();
            searchConnectors.Add(new SearchConnector()
            {
                Id = 1,
                Name = "Google",
                QueryPattern = "https://www.google.ru/search?q=%query%",
                ResultNamePattern = ".r a",
                ResultBodyPattern = ".s .st",
                ResultLinkPattern = ".r a",
                ResultPattern = ".g",
                DirectLink = "https://www.google.ru"
            });

            searchService.GetResults(Arg.Any<SearchRequest>()).Returns(new SearchResult());

            HomeController homeController = new HomeController(searchService);

            var request = new SearchResultViewModel();
            request.Connectors = Mapper.Map<List<SearchConnectorSearchViewModel>>(searchConnectors);

            var result = homeController.Index(request) as System.Web.Mvc.PartialViewResult;

            Assert.AreEqual("_Index", result.ViewName);
        }
    }
}
