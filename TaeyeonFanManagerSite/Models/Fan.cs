using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaeyeonFanManagerSite.Models
{
    public class Fan:Person
    {
        //public int ID { get; set; }

        //[Required]
        //[StringLength(50, MinimumLength = 1)]
        //[Display(Name = "Last Name")]
        //public string LastName { get; set;}

        //[Required]
        //[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        //[Column("FirstName")]
        //[Display(Name = "First Name")]
        //public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joined Date")]
        public DateTime JoinedDate { get; set;}

        //[Display(Name = "Full Name")]
        //public string FullName
        //{
        //    get
        //    {
        //        return LastName + ", " + FirstMidName;
        //    }
        //}

        public virtual ICollection<JoinedDate> JoinedDates { get; set; }
    }
}