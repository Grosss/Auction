using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using ORM.Entities;

namespace ORM
{
    public partial class AuctionContext : DbContext
    {
        public AuctionContext()
             : base("name=NewAuction")
        {
            //Database.SetInitializer<AuctionContext>(new DropCreateDatabaseIfModelChanges<AuctionContext>());
            //Database.SetInitializer<AuctionContext>(new AuctionDBInitializer());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { set; get; }
        public virtual DbSet<Lot> Lots { set; get; }
        public virtual DbSet<Bid> Bids { set; get; }
        public virtual DbSet<Category> Categories { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany<Lot>(c => c.Lots)
                .WithRequired(l => l.Category)
                .HasForeignKey(l => l.CategoryId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Lot>()
                .HasMany<Bid>(l => l.Bids)
                .WithRequired(b => b.Lot)
                .HasForeignKey(b => b.LotId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .HasMany<Lot>(u => u.Lots)
                .WithRequired(l => l.User)
                .HasForeignKey(l => l.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany<Bid>(u => u.Bids)
                .WithRequired(b => b.User)
                .HasForeignKey(b => b.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .HasMany<Role>(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(ur =>
                {
                    ur.MapLeftKey("UserId");
                    ur.MapRightKey("RoleId");
                    ur.ToTable("UserRoles");
                });
        }        
    }

    public class AuctionDBInitializer : DropCreateDatabaseIfModelChanges<AuctionContext>
    {
        protected override void Seed(AuctionContext context)
        {
            var defaultRoles = new List<Role>();

            defaultRoles.Add(new Role() { RoleId = 1, Name = "admin" });
            defaultRoles.Add(new Role() { RoleId = 2, Name = "moderator" });
            defaultRoles.Add(new Role() { RoleId = 3, Name = "user" });

            foreach (var role in defaultRoles)
                context.Roles.Add(role);

            var defaultCategories = new List<Category>();

            defaultCategories.Add(new Category() { CategoryId = 1, Name = "Other" });
            defaultCategories.Add(new Category() { CategoryId = 2, Name = "Sport" });
            defaultCategories.Add(new Category() { CategoryId = 3, Name = "Clothes" });
            defaultCategories.Add(new Category() { CategoryId = 4, Name = "Phones" });
            defaultCategories.Add(new Category() { CategoryId = 5, Name = "Computers" });

            foreach (var category in defaultCategories)
                context.Categories.Add(category);

            base.Seed(context);
        }
    }
}
