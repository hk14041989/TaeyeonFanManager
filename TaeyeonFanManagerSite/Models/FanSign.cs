using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaeyeonFanManagerSite.Models
{
    public class FanSign
    {
        [Key]
        [ForeignKey("Idol")]
        public int IdolID { get; set; }
        [StringLength(50)]
        [Display(Name = "FanSign Location")]
        public string Location { get; set; }

        public virtual Idol Idol { get; set; }
    }
}