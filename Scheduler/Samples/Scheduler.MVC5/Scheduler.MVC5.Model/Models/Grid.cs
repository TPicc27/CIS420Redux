namespace Scheduler.MVC5.Model.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Grid")]
    public partial class Grid
    {
        public int id { get; set; }

        [StringLength(150)]
        public string text { get; set; }

        [Column(TypeName = "text")]
        public string details { get; set; }

        public DateTime? start_date { get; set; }

        public DateTime? end_date { get; set; }
    }
}
