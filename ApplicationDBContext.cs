using contactplatformweb.Entities;
using Microsoft.EntityFrameworkCore;

namespace contactplatformweb
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
        }
        //public DbSet<Contact> Contacts { get; set; }
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Schedule> Schedules { get; set; }

        public DbSet<Entities.Campaign> Campaigns { get; set; }

        public DbSet<Entities.Position> Positions { get; set; }

        public DbSet<Entities.Condition> Conditions { get; set; }

        public DbSet<Entities.State> States { get; set; }

        public DbSet<Entities.ReasonForDeparture> ReasonForDepartures { get; set; }

        public DbSet<Entities.Calendar> Calendars { get; set; }

        public DbSet<Entities.Week> Weeks { get; set; }

        public DbSet<Entities.SubCampaign> SubCampaigns { get; set; }

        public DbSet<Entities.Modality> Modalities { get; set; }

        public DbSet<Entities.Trainer> Trainers { get; set; }

        // supervisor
        public DbSet<Entities.Supervisor> Supervisors { get; set; }

        public DbSet<Entities.Capa> Capas { get; set; }

        public DbSet<Entities.Team> Teams { get; set; }

        public DbSet<Entities.Cese> Ceses { get; set; }

        // group trainer
        //public DbSet<Entities.GroupTrainer> GroupTrainers { get; set; }



        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Week && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Week)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Week)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the relationships
            modelBuilder.Entity<User>()
                .HasOne(u => u.Campaign)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CampaignId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Condition)
                .WithMany()
                .HasForeignKey(u => u.ConditionId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.State)
                .WithMany()
                .HasForeignKey(u => u.StateId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Position)
                .WithMany()
                .HasForeignKey(u => u.PositionId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.ReasonForDeparture)
                .WithMany()
                .HasForeignKey(u => u.ReasonForDepartureId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Trainer>()
               .HasOne(t => t.User)
               .WithMany()
               .HasForeignKey(t => t.UserId);

            // Configurar relación uno a muchos entre User y Capa, es decir, un usuario puede tener una sola capa y una capa puede tener muchos usuarios
            //modelBuilder.Entity<Capa>()
            //    .HasMany(c => c.Users)
            //    .WithOne(u => u.)
            //     .HasForeignKey(u => u.CapaId)
            //     .OnDelete(DeleteBehavior.SetNull);
     


            // Configurar relación uno a muchos entre Calendar y User
            modelBuilder.Entity<Calendar>()
                .HasMany(c => c.Users)
                .WithOne(u => u.Calendar)
                .HasForeignKey(u => u.CalendarId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configurar relación uno a muchos entre Teams y User
          
        }

    }
}
