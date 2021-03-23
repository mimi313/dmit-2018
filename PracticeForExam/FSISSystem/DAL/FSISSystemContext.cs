using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

#region Additional Namespaces
using FSISSystem.Entities;
#endregion

namespace FSISSystem.DAL
{
    internal partial class FSISSystemContext : DbContext
    {
        public FSISSystemContext()
            : base("name=FSISDB")
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Guardian> Guardians { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerStat> PlayerStats { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(e => e.PlayerStats)
                .WithRequired(e => e.Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Guardian>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Guardian>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Guardian>()
                .Property(e => e.EmergencyPhoneNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Guardian>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Guardian>()
                .HasMany(e => e.Players)
                .WithRequired(e => e.Guardian)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.AlbertaHealthCareNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.MedicalAlertDetails)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.PlayerStats)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.TeamName)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.Coach)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.AssistantCoach)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Games)
                .WithRequired(e => e.Team)
                .HasForeignKey(e => e.HomeTeamID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Games1)
                .WithRequired(e => e.Team1)
                .HasForeignKey(e => e.VisitingTeamID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Players)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);
        }
    }
}
