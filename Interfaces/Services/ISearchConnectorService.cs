using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface ISearchConnectorService
    {
        void Create(SearchConnector searchConnector);
        void Delete(int id);
        void Edit(SearchConnector searchConnector);
        SearchConnector Get(int id);
        IEnumerable<SearchConnector> GetAll();
    }
}
