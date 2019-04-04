using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OtrsReports.Models
{
    [Table("ticket")]
    public class Ticket
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("tn")]
        public string Tn { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("customer_user_id")]
        public string CustomerUserId { get; set; }
        [Column("create_time")]
        public DateTime CreatedDateTime { get; set; }
        [Column("ticket_state_id")]
        public int StateId { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

    }

}