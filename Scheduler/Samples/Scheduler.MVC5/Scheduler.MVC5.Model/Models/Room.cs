using System.ComponentModel.DataAnnotations.Schema;

namespace Scheduler.MVC5.Model.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Room
    {
        [Key]
        [Column("room_id")]
        public int key { get; set; }

        [StringLength(128)]
        [Column("title")]
        public string label { get; set; }
        
    }
}
