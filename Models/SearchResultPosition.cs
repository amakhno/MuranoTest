using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class SearchResultPosition : BaseEntity
    {
        public string Label { get; set; }

        [Required]
        public string Link { get; set; }
        
        public string Description { get; set; }

        [Required]
        public string Query { get; set; }

        [Required]
        public int SearchConnectorId { get; set; }

        public virtual SearchConnector SearchConnector { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}