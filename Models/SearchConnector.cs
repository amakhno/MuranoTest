using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SearchConnector : BaseEntity
    {
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Шаблон запроса (%query% - запрос)")]
        public string QueryPattern { get; set; }

        [Display(Name = "Шаблон результата")]
        public string ResultPattern { get; set; }

        [Required]
        [Display(Name = "Шаблон заголовка")]
        public string ResultNamePattern { get; set; }

        [Display(Name = "Шаблон сcылки")]
        public string ResultLinkPattern { get; set; }

        [Required]
        [Display(Name = "Шаблон описания")]
        public string ResultBodyPattern { get; set; }

        [Display(Name = "Прямая ссылка на поисковик")]
        public string DirectLink { get; set; }
    }
}
