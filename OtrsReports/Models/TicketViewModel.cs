using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OtrsReports.Services;

namespace OtrsReports.Models
{

    public class TicketsTableViewModel
    {
        public bool GroupByCompletedUser { get; set; }
        public List<TicketViewModel> TicketRows = new List<TicketViewModel>();
    }

    public class TicketViewModel
    {
        public int Id { get; set; }

        public string Tn { get; set; }

        public string Title { get; set; }

        public string CustomerUserId { get; set; }

        public User ClosedByUser { get; set; }

        public string ClosedByUserInfo { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime? ClosedDateTime { get; set; }

        public string Report { get; set; }

        public TicketViewModel(Article article)
        {            
            Id = article.Ticket.Id;
            Tn = article.Ticket.Tn;
            Title = article.Ticket.Title;
            CustomerUserId = article.Ticket.CustomerUserId;
            CreatedDateTime = article.Ticket.CreatedDateTime;
            ClosedDateTime = article.CreateDateTime;            
            ClosedByUser = article.User;
        }

        public TicketViewModel(Ticket ticket)
        {
            Id = ticket.Id;
            Tn = ticket.Tn;
            Title = ticket.Title;
            CustomerUserId = ticket.CustomerUserId;
            CreatedDateTime = ticket.CreatedDateTime;
            Article closedArticle = ticket.Articles.LastOrDefault(t => t.Subject == "Закрыть");
            if (closedArticle != null)
            {
                ClosedDateTime = closedArticle.CreateDateTime;
                Report = closedArticle.Body;
                ClosedByUser = closedArticle.User;
            }
        }

    }
}
