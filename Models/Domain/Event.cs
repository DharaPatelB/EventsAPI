using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventsAPI.Models.Domain
{
    public class Event
    {
        public  Guid Id { get; set; }            
        public string? Name { get; set; }
        public string Picture { get; set; }
        public string Tagline { get; set; }
        public DateTime Schedule { get; set; }
        public string Description { get; set; }
        public string Moderator { get; set; }
        public string Category { get; set; }
        public string Sub_Category { get; set; }
        public int Rigor_Rank { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
       
    }
}
