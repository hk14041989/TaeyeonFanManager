using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaeyeonFanManagerSite.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "UserName required", AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password required", AllowEmptyStrings = false)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Fullname required", AllowEmptyStrings = false)]
        public string FullName { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email ID not valid")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Gender required", AllowEmptyStrings = false)]
        public string Gender { get; set; }
    }
}