using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TaeyeonFanManagerSite.Models;

namespace TaeyeonFanManagerSite.DAL
{
    public class TeamContext:DbContext
    {
        public TeamContext()
            : base("TeamContext")
        {

        }

        public DbSet<Fan> Fans { get; set; }
        public DbSet<JoinedDate> JoinedDates { get; set; }
        public DbSet<Offline> Offlines { get; set; }
        public DbSet<Idol> Idols { get; set; }
        public DbSet<Concept> Concepts { get; set; }
        public DbSet<FanSign> FanSigns { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Offline>()
             .HasMany(c => c.Idols).WithMany(i => i.Offlines)
             .Map(t => t.MapLeftKey("OfflineId")
                 .MapRightKey("IdolID")
                 .ToTable("OfflineIdol"));

            modelBuilder.Entity<Idol>()
                .HasOptional(p => p.FanSign).WithRequired(p => p.Idol);

            modelBuilder.Entity<Concept>().MapToStoredProcedures();

            modelBuilder.Entity<Concept>().Property(p => p.RowVersion).IsConcurrencyToken();
        }
    }
}