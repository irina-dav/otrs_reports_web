using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OtrsReports.Models;

namespace OtrsReports.Repositories
{
    public class EFRepository : IRepository
    {
        private AppDbContext context;

        public EFRepository(AppDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Ticket> Tickets => context.Tickets.Include(t => t.Articles);
        public IQueryable<Article> Articles => context.Articles.Include(t => t.Ticket).Include(t => t.User);
        public IQueryable<User> Users => context.Users;

        public Ticket GetTicket(int id)
        {
            return context.Tickets.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Ticket> GetClosedTickets(ReportFilter filter)
        {
            var tickets = Articles
                    .Where(a => a.Subject == "Закрыть" && a.CreateDateTime >= filter.ClosedDateFrom && a.CreateDateTime <= filter.ClosedDateTo)
                    .Select(t => t.Ticket);            

            return tickets;
        }

        public IEnumerable<Article> GetdArticlesClosure(ReportFilter filter)
        {
            var articlesSuccessClosed = Articles
                .Where(a => a.Subject.EndsWith("успешно закрыта") && a.CreateDateTime >= filter.ClosedDateFrom && a.CreateDateTime <= filter.ClosedDateTo);
            List<Article> articles = new List<Article>();
            foreach (var grpTicket in articlesSuccessClosed.GroupBy(a => a.Ticket))
            {
                var lastArticleClose = grpTicket.Key.Articles.LastOrDefault(a => a.Subject == "Закрыть");
                if (lastArticleClose != null)
                    articles.Add(lastArticleClose);
                else
                    articles.Add(grpTicket.LastOrDefault(a => a.Subject.EndsWith("успешно закрыта")));
            }
           
            if (filter.ClosedByUsers != null)
            {
                articles = articles.Where(a => filter.ClosedByUsers.Contains(a.create_by)).ToList();
            }
            return articles;

        }
    }
}
