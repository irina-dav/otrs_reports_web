using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OtrsReports.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }


        [Column("login")]
        public string Login { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string Lastname { get; set; }

        [Column("valid_id")]
        public int ValidId { get; set; }

        [NotMapped]
        public string DisplayString => $"{Lastname} {FirstName}";
    }
}
