using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCodeCamp.Migrations;

namespace TheCodeCamp.Data
{
  public class CampContext : DbContext
  {
    public CampContext() : base("CodeCampConnectionString")
    {
      Database.SetInitializer(new MigrateDatabaseToLatestVersion<CampContext, Configuration>());
    }

    public DbSet<Camp> Camps { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<Talk> Talks { get; set; }

  }
}
