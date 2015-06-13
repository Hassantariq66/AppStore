using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FreeAppsDownload.Models
{
    public partial class UserMataData
    {
        public int Id { get; set; }

        [Required]
        public string username { get; set; }
        [Required (ErrorMessage="Write First name")]
        
        public string fname { get; set; }
        [Required(ErrorMessage = "Write Last name")]
        public string lname { get; set; }


        [Required(ErrorMessage = "Write Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required(ErrorMessage = "Write Password")]
        [StringLength(8)]
        public string password { get; set; }
        public string date { get; set; }
    }
}