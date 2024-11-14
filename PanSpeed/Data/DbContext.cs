using Microsoft.EntityFrameworkCore;
using PanSpeed.Models;
using System.Collections.Generic;

namespace PanSpeed.Data
{
    public class PanSpeedContext : DbContext
    {
        public PanSpeedContext(DbContextOptions<PanSpeedContext> options) : base(options) { }
        public DbSet<Bookmark> Bookmarks { get; set; }
    }

}
