using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ShortUrl.Databases
{
    public class ShortURLContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }
        public DbSet<User> Users { get; set; }

        // The following configures EF to create a Sqlite database file as `C:\blogging.db`.
        // For Mac or Linux, change this to `/tmp/blogging.db` or any other absolute path.
        // public ShortURLContext():base()
        // {
        // }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseSqlite($@"Data Source=./ShortURL.db");
        }
    }

    public class Url
    {
        [Key]
        [MaxLength(50)]
        public string Hash { get; set; }
        [MaxLength(512)]
        public string OriginalURL { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
    }

    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}