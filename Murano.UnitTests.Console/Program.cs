using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murano.UnitTests;

namespace Murano.UnitTests.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            HomeControllerUnitTests searchServiceUnitTest = new HomeControllerUnitTests();
            searchServiceUnitTest.Index_WithoutParams_ReturnPartialView();
        }
    }
}
