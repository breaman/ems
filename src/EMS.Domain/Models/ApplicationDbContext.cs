using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Domain.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<CompetitionLevel> CompetitionLevels { get; set; }
        public DbSet<TournamentParameter> TournamentParameters { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
        public DbSet<Bracket> Brackets { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<PromotionalCode> PromotionalCodes { get; set; }
        public DbSet<EventInfo> EventInfo { get; set; }
        public DbSet<TeamInfo> TeamInfo { get; set; }
        public DbSet<ParticipantInfo> ParticipantInfo { get; set; }

        private IHttpContextAccessor HttpContextAccessor { get; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EventInfo>().HasData(
                new EventInfo {Id = 1, Name = "See3Slam", Year = 2018},
                new EventInfo {Id = 2, Name = "SpikeAndDig", Year = 2018}
            );

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            AddFingerPrinting();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            AddFingerPrinting();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddFingerPrinting()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            var added = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);

            foreach (var entry in added)
            {
                var fingerPrintEntry = entry.Entity as FingerPrintingEntityBase;
                if (fingerPrintEntry != null)
                {
                    fingerPrintEntry.CreatedOn = DateTimeOffset.UtcNow;
                    fingerPrintEntry.CreatedBy = Convert.ToInt32(HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier));
                }
            }

            foreach (var entry in modified)
            {
                var fingerPrintEntry = entry.Entity as FingerPrintingEntityBase;
                if (fingerPrintEntry != null)
                {
                    fingerPrintEntry.ModifiedOn = DateTimeOffset.UtcNow;
                    fingerPrintEntry.ModifiedBy = Convert.ToInt32(HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier));
                }
            }
        }
    }
}