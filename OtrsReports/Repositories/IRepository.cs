using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtrsReports.Models;

namespace OtrsReports.Repositories
{
    public interface IRepository
    {
        IQueryable<Ticket> Tickets { get; }
        IQueryable<Article> Articles { get; }
        IQueryable<User> Users { get; }

        Ticket GetTicket(int id);

        IQueryable<Ticket> GetClosedTickets(ReportFilter filters);

        IEnumerable<Article> GetdArticlesClosure(ReportFilter filter);
    }
}
