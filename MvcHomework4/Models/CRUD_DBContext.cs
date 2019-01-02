using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MvcHomework4.Models
{
    public partial class CRUD_DBContext : DbContext
    {
        public CRUD_DBContext()
        {
        }

        public CRUD_DBContext(DbContextOptions<CRUD_DBContext> options)
            : base(options)
        { }

        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<UserBlogMatch> UserBlogMatch { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CRUD_DB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.Property(e => e.Url).IsRequired();
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.BlogId);
            });

            modelBuilder.Entity<UserBlogMatch>(entity =>
            {
                entity.HasKey(e => e.RecordId);

                entity.Property(e => e.RecordId).HasColumnName("recordId");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.UserBlogMatch)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_blogId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBlogMatch)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_userId");
            });
        }
    }
}
