using EventsAPI.Models.Domain;
using EventsAPI.Models.DTO;
using EventsAPI.Repository.Abastract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;

namespace EventsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IEventRepository _eventRepo;
        private readonly DatabaseContext dbContext;

        public EventController(IFileService fs, IEventRepository eventRepo, DatabaseContext dbContext)
        {
            this._fileService = fs;
            this._eventRepo = eventRepo;
            this.dbContext = dbContext;

        }
        
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEvents([FromRoute] Guid id)
        {
            var @event = await dbContext.Event.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }
            return Ok(@event);
        }

        [HttpGet("EventsbyRecency")]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await dbContext.Event.OrderByDescending(Event => Event.Schedule).ToListAsync());

        }

        [HttpPost]
        public IActionResult Add([FromForm] Event model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }
            if (model.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 1)
                {
                    model.Picture = fileResult.Item2; 
                }
                var eventResult = _eventRepo.Add(model);
                if (eventResult)
                {
                    status.StatusCode = 1;
                    status.Message = "Added successfully";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on adding product";

                }
            }
            return Ok(status);

        }

        [HttpGet("{page}")]
        public async Task<IActionResult> GetEvents(int page)
        {
            var pageResults = 5f;
            var pageCount = Math.Ceiling(dbContext.Event.Count() / pageResults);

            var events = await dbContext.Event
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
            .ToListAsync();

            var responce = new EventPages
            {
                Events = events,
                CurrentPages = page,
                Pages = (int)pageCount
            };
            return Ok(responce);
        }      

       [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {
            var @event = await dbContext.Event.FindAsync(id);
            if (@event != null)
            {
                dbContext.Remove(@event);
                await dbContext.SaveChangesAsync();
                return Ok(@event);
            }
            return NotFound();
        }
          /* [HttpPut]
         [Route("{id:guid}")]
         public async Task<IActionResult> Update(Guid id, Event model)
         {
             var @event =await dbContext.Event.FindAsync(id);
             if (@event != null)
             {
                 @event.Name = model.Name; 
                 @event.Picture  = model.Picture;
                 @event.Tagline = model.Tagline;
                 @event.Schedule = model.Schedule;
                 @event.Description = model.Description;
                 @event.Moderator = model.Moderator;
                 @event.Category = model.Category;
                 @event.Sub_Category = model.Sub_Category;
                 @event.Rigor_Rank = model.Rigor_Rank;
                 @event.ImageFile = model.ImageFile;

                await dbContext.SaveChangesAsync();

             }
             return Ok();

         }       
          */    
    }
}

