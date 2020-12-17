using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication5.Models;
using WebApplication5.Services;

namespace WebApplication5.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketServiceController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketServiceController(ITicketService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<int> GetCount()
        {
            return await Task.FromResult(_service.RemainingCount()).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<BookResult> BookTicket(Ticket ticket)
        {
            return await Task.FromResult(_service.Add(ticket)).ConfigureAwait(false);
        }
    }
}
