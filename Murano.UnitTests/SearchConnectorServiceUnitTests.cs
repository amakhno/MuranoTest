using Interfaces.Repositories;
using Models;
using NSubstitute;
using NUnit.Framework;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murano.UnitTests
{
    [TestFixture]
    public class SearchConnectorServiceUnitTests
    {
        [Test]
        public void Get_NullID_Throw()
        {
            var searchConnectorRepository = Substitute.For<ISearchConnectorRepository>();
            SearchConnectorService searchConnectorService = new SearchConnectorService(searchConnectorRepository);
            var exception = Assert.Throws<Exception>(() => searchConnectorService.Get(0));
            StringAssert.Contains("can't be 0", exception.Message);
        }

        [Test]
        public void Edit_NullID_Throw()
        {
            var searchConnectorRepository = Substitute.For<ISearchConnectorRepository>();
            SearchConnectorService searchConnectorService = new SearchConnectorService(searchConnectorRepository);
            SearchConnector searchConnector = new SearchConnector() { Id = 0 };
            var exception = Assert.Throws<Exception>(() => searchConnectorService.Edit(searchConnector));
            StringAssert.Contains("can't be 0", exception.Message);
        }

        [Test]
        public void Delete_NullID_Throw()
        {
            var searchConnectorRepository = Substitute.For<ISearchConnectorRepository>();
            SearchConnectorService searchConnectorService = new SearchConnectorService(searchConnectorRepository);
            var exception = Assert.Throws<Exception>(() => searchConnectorService.Delete(0));
            StringAssert.Contains("can't be 0", exception.Message);
        }
    }
}
