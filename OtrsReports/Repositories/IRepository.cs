using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtrsReports.Models;

namespace OtrsReports.Repositories
{
    public interface IRepository
    {
        IEnumerable<Ticket> Tickets { get; }
        IEnumerable<Article> Articles { get; }
        IEnumerable<User> Users { get; }

        Ticket GetTicket(int id);

        IEnumerable<Ticket> GetClosedTickets(ReportFilter filters);

        IEnumerable<Article> GetdArticlesClosure(ReportFilter filter);
    }
}
