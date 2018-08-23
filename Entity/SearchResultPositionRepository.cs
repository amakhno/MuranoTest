using Interfaces.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class SearchResultPositionRepository : GenericRepository<SearchResultPosition, ApplicationContext>,
        ISearchResultPositionRepository
    {
        public SearchResultPositionRepository(ApplicationContext applicationContext) : base(applicationContext) { }

    }
}
