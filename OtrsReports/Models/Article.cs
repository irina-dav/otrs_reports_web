using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtrsReports.Models
{
    [Table("article")]
    public class Article
    {
        [Column("id")]
        public int Id { get; set; }

        public int ticket_id { get; set; }

        [ForeignKey("ticket_id")]
        public virtual Ticket Ticket { get; set; }

        public int create_by { get; set; }

        [ForeignKey("create_by")]
        public virtual User User { get; set; }

        [Column("a_subject")]
        public string Subject { get; set; }

        [Column("a_body")]
        public string Body { get; set; }

        [Column("create_time")]
        public DateTime CreateDateTime { get; set; }

    }
}
