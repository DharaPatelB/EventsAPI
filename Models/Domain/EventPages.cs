namespace EventsAPI.Models.Domain
{
    public class EventPages
    {       
            public List<Event> Events { get; set; } = new List<Event>();
            public int Pages { get; set; }
            public int CurrentPages { get; set; }
    }
}

