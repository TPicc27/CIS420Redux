namespace Scheduler.MVC5.Model.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ValidEvent")]
    public partial class ValidEvent
    {
        public long id { get; set; }

        [StringLength(250)]
        public string text { get; set; }

        public DateTime? start_date { get; set; }

        public DateTime? end_date { get; set; }

        [StringLength(50)]
        public string email { get; set; }
    }
}
