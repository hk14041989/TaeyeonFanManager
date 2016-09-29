using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaeyeonFanManagerSite.Models
{
    public class Idol:Person
    {
        //public int ID { get; set; }

        //[Required]
        //[Display(Name = "Last Name")]
        //[StringLength(50)]
        //public string LastName { get; set; }

        //[Required]
        //[Column("FirstName")]
        //[Display(Name = "First Name")]
        //[StringLength(50)]
        //public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Meet Date")]
        public DateTime MeetDate { get; set; }

        //[Display(Name = "Full Name")]
        //public string FullName
        //{
        //    get { return LastName + ", " + FirstMidName; }
        //}

        public virtual ICollection<Offline> Offlines { get; set; }
        public virtual FanSign FanSign { get; set; }
    }
}