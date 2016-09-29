using System.Collections.Generic;
using TaeyeonFanManagerSite.Models;

namespace TaeyeonFanManagerSite.ViewModels
{
    public class IdolIndexData
    {
        public IEnumerable<Idol> Idols { get; set; }
        public IEnumerable<Offline> Offlines { get; set; }
        public IEnumerable<JoinedDate> JoinedDates { get; set; }
    }
}