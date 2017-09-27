namespace RBAC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyRBAC : DbContext
    {
        public MyRBAC()
            : base("name=MyRBAC")
        {
        }

        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>()
                .HasMany(e => e.Role)
                .WithMany(e => e.Module)
                .Map(m => m.ToTable("RoleModule").MapLeftKey("Moduleid").MapRightKey("Roleid"));

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Role)
                .Map(m => m.ToTable("UserRole").MapLeftKey("Roleid").MapRightKey("Usersid"));
        }
    }
}
