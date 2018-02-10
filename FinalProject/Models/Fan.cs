using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Fan
    {
        public int FanID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Seniority")]
        public double Seniority { get; set; }
    }
}