using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalProject.DAL
{
    public class BlogContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}