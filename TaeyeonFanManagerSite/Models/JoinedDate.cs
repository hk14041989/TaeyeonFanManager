using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TaeyeonFanManagerSite.Models
{
    public enum Rank
    {
        S, A, B, C, D
    }

    public class JoinedDate
    {
        public int JoinedDateID { get; set; }
        public int OfflineID { get; set; }
        public int FanID { get; set; }

        [DisplayFormat(NullDisplayText = "No grade")]
        public Rank? Rank { get; set; }

        public virtual Offline Offline { get; set; }
        public virtual Fan Fan { get; set; }
    }
}