namespace Scheduler.MVC5.Model.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Event
    {
        public int id { get; set; }

        [Column(TypeName = "text")]
        public string text { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        public int? room_id { get; set; }

        public int? user_id { get; set; }
    }
}
