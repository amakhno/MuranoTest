using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entity;
using Interfaces.Repositories;
using Interfaces.Services;
using Models;

namespace MuranoTest.Web.Controllers
{
    public class SearchConnectorsController : Controller
    {
        private readonly ISearchConnectorService _searchConnectorService;

        public SearchConnectorsController(ISearchConnectorService searchConnectorService)
        {
            _searchConnectorService = searchConnectorService;
        }

        public ActionResult Index()
        {
            return View(_searchConnectorService.GetAll());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchConnector searchConnector = _searchConnectorService.Get(id.Value);
            if (searchConnector == null)
            {
                return HttpNotFound();
            }
            return View(searchConnector);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,QueryPattern,ResultPattern,ResultNamePattern,ResultLinkPattern,ResultBodyPattern,DirectLink")]
            SearchConnector searchConnector)
        {
            if (ModelState.IsValid)
            {
                _searchConnectorService.Create(searchConnector);
                return RedirectToAction("Index");
            }

            return View(searchConnector);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchConnector searchConnector = _searchConnectorService.Get(id.Value);
            if (searchConnector == null)
            {
                return HttpNotFound();
            }
            return View(searchConnector);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,QueryPattern,ResultPattern,ResultNamePattern,ResultLinkPattern,ResultBodyPattern,DirectLink")]
            SearchConnector searchConnector)
        {
            if (ModelState.IsValid)
            {
                _searchConnectorService.Edit(searchConnector);
                return RedirectToAction("Index");
            }
            return View(searchConnector);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchConnector searchConnector = _searchConnectorService.Get(id.Value);
            if (searchConnector == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", searchConnector);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SearchConnector searchConnector = _searchConnectorService.Get(id);
            _searchConnectorService.Delete(searchConnector.Id);
            return RedirectToAction("Index");
        }
    }
}
