namespace Scheduler.MVC5.Model.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ColoredEvent
    {
        public int id { get; set; }

        [Column(TypeName = "text")]
        public string text { get; set; }

        public DateTime? start_date { get; set; }

        public DateTime? end_date { get; set; }

        [StringLength(18)]
        public string color { get; set; }
       
        public int? room_id { get; set; }
    }
}
