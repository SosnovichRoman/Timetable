using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Timetable.Models;

namespace Timetable.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Timetable.Models.Group> Group { get; set; }
        public DbSet<Timetable.Models.Day> Day { get; set; }
        public DbSet<Timetable.Models.Lesson> Lesson { get; set; }
    }
}
