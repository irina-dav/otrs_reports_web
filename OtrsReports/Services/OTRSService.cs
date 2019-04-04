using Microsoft.Extensions.Logging;
using OtrsReports.Repositories;
using OtrsReports.Models;
using Microsoft.Extensions.Configuration;

namespace OtrsReports.Services
{
    public class OTRSService
    {
        private IConfiguration configuration;
        private readonly ILogger logger;
        private readonly IRepository repository;
        private readonly AppDbContext context;
        private readonly string urlTicketTemplate;
        private readonly int reportLengthLimit;

        public OTRSService(IConfiguration config, ILogger<OTRSService> logger, IRepository repository, AppDbContext context)
        {
            this.configuration = config;
            this.logger = logger;
            this.repository = repository;
            this.context = context;
            urlTicketTemplate = configuration.GetSection("AppSettings")["OTRSTicketUrlTemplate"].ToString();
            int.TryParse(configuration.GetSection("AppSettings")["ReportLengthLimit"].ToString(), out int limit);
            reportLengthLimit = limit;
        }

        public string GetTicketUrl(int ticketId)
        {
            return string.Format(urlTicketTemplate, ticketId);
        }


        public string GetClosedArticleBody(Article article)
        {
            string report = "";
            if (article.Subject.EndsWith("успешно закрыта"))
            {
                string body = article.Body.Replace("\n\n", " ");
                string template = "переведена в статус \"Закрыта успешно\". Решение: ";
                int firstPos = body.IndexOf(template);
                firstPos = (firstPos == -1 || firstPos > template.Length) ? 0 : firstPos + template.Length;
                int secondPos = System.Math.Min(body.IndexOf("Служба ИТ-"), body.IndexOf("Тема заявки: "));
                if (secondPos == -1)
                    secondPos = body.Length;
                report = body.Substring(firstPos, secondPos - firstPos);
                if (report.Length > reportLengthLimit)
                    report = report.Substring(0, reportLengthLimit) + "...";
            }
            else
            {
                report = article.Body;
            }

            return report;
        }
    }
}
