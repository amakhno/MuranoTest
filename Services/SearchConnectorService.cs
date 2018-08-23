using Interfaces.Repositories;
using Interfaces.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SearchConnectorService : ISearchConnectorService
    {
        private readonly ISearchConnectorRepository _searchConnectorRepository;

        public SearchConnectorService(ISearchConnectorRepository searchConnectorRepository)
        {
            _searchConnectorRepository = searchConnectorRepository;
        }

        public IEnumerable<SearchConnector> GetAll()
        {
            return _searchConnectorRepository.GetAll().ToList();
        }

        public SearchConnector Get(int id)
        {
            if (id == 0)
            {
                throw new Exception("Id can't be 0");
            }
            return _searchConnectorRepository.Get(id).GetAwaiter().GetResult();
        }

        public void Create(SearchConnector searchConnector)
        {
            int result = _searchConnectorRepository.Insert(searchConnector).GetAwaiter().GetResult();
        }

        public void Edit(SearchConnector searchConnector)
        {
            if (searchConnector.Id == 0)
            {
                throw new Exception("Id can't be 0");
            }
            int result = _searchConnectorRepository.Update(searchConnector).GetAwaiter().GetResult();
        }

        public void Delete(int id)
        {
            if (id == 0)
            {
                throw new Exception("Id can't be 0");
            }                
            int result = _searchConnectorRepository.Remove(new SearchConnector { Id = id }).GetAwaiter().GetResult();
        }
    }
}
