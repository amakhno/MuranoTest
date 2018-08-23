using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MuranoTest.Web.ViewModels
{
    public class SearchConnectorSearchViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Use { get; set; } = true;
    }
}