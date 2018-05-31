using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EMS.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EMS.Domain.Concrete
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
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Bracket> Brackets { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<PromotionalCode> PromoCodes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            base.OnModelCreating(builder);
        }
    }
}
