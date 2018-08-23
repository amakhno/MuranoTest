using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SearchRequest
    {
        [Required(ErrorMessage = "Введите какой-нибудь запрос")]
        public string Query { get; set; }

        public int[] SystemIds { get; set; }

        public override string ToString()
        {
            var result = "";
            if (SystemIds != null)
            {
                for (int i = 0; i < SystemIds.Length; i++)
                {
                    result += SystemIds[i] + " ";
                }
            }            
            result += "\nQuery=" + Query;
            return result;
        }
    }
}
