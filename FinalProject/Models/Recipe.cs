using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Author")]
        public string Author { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recipe Content")]
        public string RecipeContent { get; set; }


        [DataType(DataType.ImageUrl)]
        [Display(Name = "Picute")]
        public string Picute { get; set; }


        [DataType(DataType.Url)]
        [Display(Name = "Video")]
        public string Video { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}