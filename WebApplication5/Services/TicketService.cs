using System.Collections.Generic;
using WebApplication5.Models;

namespace WebApplication5.Services
{
    public interface ITicketService
    {
        BookResult Add(Ticket ticket);

        int RemainingCount();
    }

    public class TicketService : ITicketService
    {
        public static object KEY = new object();

        private static HashSet<Ticket> Tickets { get; set; } = new HashSet<Ticket>();

        private static readonly int MAX_COUNT = 100;

        public BookResult Add(Ticket ticket)
        {
            lock (KEY)
            {
                if (RemainingCount() > 0)
                {
                    Tickets.Add(ticket);

                    return new BookResult()
                    {
                        IsSuccess = true
                    };
                }

                return new BookResult();
            }
        }

        public int RemainingCount()
        {
            return MAX_COUNT - Tickets.Count;
        }
    }
}
