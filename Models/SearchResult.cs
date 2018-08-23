using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SearchResult : SearchRequest
    {
        public int SearchSystem { get; set; }

        public List<SearchResultPosition> SearchPositions { get; set; } = new List<SearchResultPosition>();

        public override string ToString()
        {
            var result = base.ToString();
            result += $"\nSearchSystem: {SearchSystem}\n";
            if (SearchPositions != null)
            {

                foreach (var position in SearchPositions)
                {
                    result += $"SearchPosition: {position.Id}";
                }

            }
            return result;
        }
    }
}
