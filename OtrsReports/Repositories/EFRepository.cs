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

        public IEnumerable<Ticket> Tickets => context.Tickets.Include(t => t.Articles);
        public IEnumerable<Article> Articles => context.Articles.Include(t => t.Ticket).Include(t => t.User);
        public IEnumerable<User> Users => context.Users;

        public Ticket GetTicket(int id)
        {
            return context.Tickets.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Ticket> GetClosedTickets(ReportFilter filter)
        {
            List<Ticket> tickets = Articles
                    .Where(a => a.Subject == "Закрыть" && a.CreateDateTime >= filter.ClosedDateFrom && a.CreateDateTime <= filter.ClosedDateTo)
                    .Select(t => t.Ticket).ToList();            

            return tickets;
        }

        public IEnumerable<Article> GetdArticlesClosure(ReportFilter filter)
        {
            /* List<Article> articles = Articles.Where(a => a.Subject == "Закрыть" && a.CreateDateTime >= filter.ClosedDateFrom && a.CreateDateTime <= filter.ClosedDateTo).ToList();
             List<Ticket> tickets_wrong_ended = Tickets.Where(t => t.StateId == 2 && !t.Articles.Any(a => a.Subject == "Закрыть")).ToList();
             List<Article> articles_wrong_ended = tickets_wrong_ended
                 .Select(t => t.Articles
                              .LastOrDefault(a => a.CreateDateTime >= filter.ClosedDateFrom && a.CreateDateTime <= filter.ClosedDateTo && a.Subject.EndsWith("успешно закрыта")))
                 .ToList();
             articles.AddRange(articles_wrong_ended);
             articles.RemoveAll(a => a == null);
             if (filter.ClosedByUsers != null)
             {
                 articles = articles.Where(a => filter.ClosedByUsers.Contains(a.create_by)).ToList();
             }
             return articles;*/
            List<Article> articlesSuccessClosed = Articles
                .Where(a => a.Subject.EndsWith("успешно закрыта") && a.CreateDateTime >= filter.ClosedDateFrom && a.CreateDateTime <= filter.ClosedDateTo)
                .ToList();
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
