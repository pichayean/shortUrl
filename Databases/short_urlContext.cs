using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ShortUrl.Databases
{
    public partial class short_urlContext : DbContext
    {
        public short_urlContext()
        {
        }

        public short_urlContext(DbContextOptions<short_urlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Url> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_CI_AS");

            modelBuilder.Entity<Url>(entity =>
            {
                entity.ToTable("Url", "short_url");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("(NOW())");

                entity.Property(e => e.OriginalUrl)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("OriginalURL")
                    .IsUnicode(true)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
