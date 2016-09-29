using System;
using System.ComponentModel.DataAnnotations;

namespace TaeyeonFanManagerSite.ViewModels
{
    public class JoinedDateDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? JoinedDateDate { get; set; }

        public int FanCount { get; set; }
    }
}