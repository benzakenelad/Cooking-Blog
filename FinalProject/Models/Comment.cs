using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int RecipeID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Author")]
        public string Author { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Comment Content")]
        public string CommentContent { get; set; }

        public virtual Recipe Recipe { get; set; }

    }
}