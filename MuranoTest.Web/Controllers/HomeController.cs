using AutoMapper;
using Interfaces.Services;
using Models;
using MuranoTest.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuranoTest.Web.Controllers
{
    public class HomeController : Controller
    {
        public readonly ISearchService _searchService;

        public HomeController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult Index()
        {
            var model = new SearchResultViewModel();
            model.Connectors = Mapper.Map<List<SearchConnectorSearchViewModel>>(_searchService.GetSearchConnectors());
            return View(model);
        }        

        [HttpPost]
        public ActionResult Index(SearchResultViewModel searchRequest)
        {
            searchRequest.SystemIds = searchRequest.Connectors.Where(x => x.Use == true).Select(x=>x.Id).ToArray();
            SearchResultViewModel result = new SearchResultViewModel(_searchService.GetResults(searchRequest));
            result.Connectors = searchRequest.Connectors;

            return PartialView("_Index", result);
        }
    }
}