using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ArcadiaFansub.Domain.Models
{
    public partial class ArcadiaFansubContext : DbContext
    {
        public ArcadiaFansubContext()
        {
        }

        public ArcadiaFansubContext(DbContextOptions<ArcadiaFansubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anime> Animes { get; set; } = null!;
        public virtual DbSet<Episode> Episodes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<AdminTicket> AdminTickets { get; set; } = null!;
        public virtual DbSet<UserTicket> UserTickets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-12NGJ7T;Initial Catalog=ArcadiaFansub;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //relationship
            modelBuilder.Entity<Episode>()
            .HasOne(e => e.Anime)
            .WithMany(a => a.Episodes)
            .HasForeignKey(e => e.AnimeId);
            //relationship

            modelBuilder.Entity<AdminTicket>(entity =>
            {
                entity.HasKey(e => e.ResponseId);

                entity.Property(e => e.TicketReply)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.TicketReplyDate).HasColumnType("datetime");

                entity.Property(e => e.TicketAdminName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<UserTicket>(entity =>
            {
               entity.HasKey(e=> e.TicketId);

                entity.Property(e => e.TicketTitle)
                    .HasMaxLength(48)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.TicketMessage)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.SenderName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.TicketReason)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.TicketStatus)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.SenderToken)
                    .HasMaxLength(208)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.TicketDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Anime>(entity =>
            {
                entity.Property(e => e.AnimeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AnimeEpisodeAmount)
                .IsUnicode(false);

                entity.Property(e => e.AnimeImage);

                entity.Property(e => e.Editor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

                entity.Property(e => e.Translator)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Episode>(entity =>
            {
                entity.Property(e => e.AnimeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EpisodeNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.EpisodeLinks)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.EpisodeLikes)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EpisodeUploadDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FavoritedAnimes)
        .HasColumnType("nvarchar(max)");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserToken)
                    .HasMaxLength(208)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.UserPermission)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
