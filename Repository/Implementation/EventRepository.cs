using EventsAPI.Models.Domain;
using EventsAPI.Repository.Abastract;

namespace EventsAPI.Repository.Implementation
{
    public class EventRepository : IEventRepository
    {
        private readonly DatabaseContext _context;
        public EventRepository(DatabaseContext context)
        {
            this._context = context;
        }
        public bool Add(Event model)
        {
            try
            {
                _context.Event.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
       

    }
}
