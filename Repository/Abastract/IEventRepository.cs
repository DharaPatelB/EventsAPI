using EventsAPI.Models.Domain;

namespace EventsAPI.Repository.Abastract
{
    public interface IEventRepository
    {
        bool Add(Event model);
    }
}

