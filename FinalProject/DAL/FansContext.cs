using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalProject.DAL
{
    public class FanContext : DbContext
    {
        public DbSet<Fan> fans { get; set; }

    }
}