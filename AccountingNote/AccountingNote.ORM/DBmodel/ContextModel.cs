using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AccountingNote.ORM.DBmodel
{
    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=DefaultConnectionString")
        {
        }

        public virtual DbSet<Accounting> Accountings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounting>()
                .Property(e => e.CoverImage)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.PWD)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.MobilePhone)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.UserInfo)
                .WillCascadeOnDelete(false);
        }
    }
}
