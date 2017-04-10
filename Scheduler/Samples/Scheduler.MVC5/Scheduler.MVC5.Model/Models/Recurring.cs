namespace Scheduler.MVC5.Model.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Recurring")]
    public partial class Recurring
    {
        public int id { get; set; }

        [Column(TypeName = "text")]
        public string text { get; set; }

        public DateTime? start_date { get; set; }

        public DateTime? end_date { get; set; }

        public int? room_id { get; set; }

        public long? event_length { get; set; }

        [StringLength(50)]
        public string rec_type { get; set; }

        public int? event_pid { get; set; }
    }
}
