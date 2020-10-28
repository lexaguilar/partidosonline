using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineFutbol.Models
{
    public partial class FutbolContext : DbContext
    {
        public FutbolContext()
        {
        }

        public FutbolContext(DbContextOptions<FutbolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Matches> Matches { get; set; }
        public virtual DbSet<MatchesViewers> MatchesViewers { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<Viewers> Viewers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LEX-PC\\PCLEX;Database=FutbolOnline;User Id=sa;Password=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matches>(entity =>
            {
                entity.HasIndex(e => e.EventDate)
                    .HasName("IX_Matches");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("URL")
                    .HasMaxLength(2000);

                entity.HasOne(d => d.TeamAway)
                    .WithMany(p => p.MatchesTeamAway)
                    .HasForeignKey(d => d.TeamAwayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matches_Teams1");

                entity.HasOne(d => d.TeamHome)
                    .WithMany(p => p.MatchesTeamHome)
                    .HasForeignKey(d => d.TeamHomeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matches_Teams");
            });

            modelBuilder.Entity<MatchesViewers>(entity =>
            {
                entity.HasKey(e => e.MatchId);

                entity.Property(e => e.MatchId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Viewers>(entity =>
            {
                entity.HasIndex(e => e.MatchId);

                entity.Property(e => e.Added).HasColumnType("datetime");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
