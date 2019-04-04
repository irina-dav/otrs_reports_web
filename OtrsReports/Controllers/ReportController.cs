using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtrsReports.Models;
using OtrsReports.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtrsReports.Services;

namespace OtrsReports.Controllers
{
    public class ReportController : Controller
    {
        private readonly IRepository repo;
        private readonly OTRSService otrsService;

        public ReportController(IRepository repository, OTRSService otrsService)
        {
            repo = repository;
            this.otrsService = otrsService;
        }

        public ViewResult Index()
        {
            ReportViewModel reportVM = new ReportViewModel()
            {
                Users = repo.Users
                    .Where(u => u.ValidId == 1)
                    .Select(u => new SelectListItem() { Value = u.Id.ToString(), Text = u.DisplayString })
                    .OrderBy(i => i.Text),
                ReportDateFrom = DateTime.Now.AddDays(-1).Date,
                ReportDateTo = DateTime.Now.AddDays(1).Date,
                GroupByCompletedUser = false
            };
            ViewBag.Title = "Закрытые заявки за период";
            return View(reportVM);
        }

        [HttpPost]
        public IActionResult ClosedTickets(ReportFilter filter)
        {
            List<Article> article = repo.GetdArticlesClosure(filter).ToList();
            List<TicketViewModel> ticketsVM = article.Select(a => new TicketViewModel(a) { Report = otrsService.GetClosedArticleBody(a) }).ToList();
            if (filter.GroupByCompletedUser)
            {
                ticketsVM.ForEach(t =>               
                    t.ClosedByUserInfo = $"{t.ClosedByUser.DisplayString} [{ticketsVM.Count(x => t.ClosedByUser.Id == x.ClosedByUser.Id)}]"                  
                );
            }                       
            return PartialView("_Tickets", new TicketsTableViewModel() { GroupByCompletedUser = filter.GroupByCompletedUser, TicketRows = ticketsVM });
        }        
    }
}
