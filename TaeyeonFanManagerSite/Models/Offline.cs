using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaeyeonFanManagerSite.Models
{
    public class Offline
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int OfflineID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 500000)]
        public int TicketPrices { get; set; }

        public int ConceptID { get; set; }

        public virtual Concept Concept { get; set; }
        public virtual ICollection<Idol> Idols { get; set; }
        public virtual ICollection<JoinedDate> JoinedDates { get; set; }
    }
}