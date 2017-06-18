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
             : base("name=Auction")
        {
            Database.SetInitializer<AuctionContext>(new DropCreateDatabaseIfModelChanges<AuctionContext>());
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
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lot>()
                .HasMany<Bid>(l => l.Bids)
                .WithRequired(b => b.Lot)
                .HasForeignKey(b => b.LotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany<Lot>(u => u.Lots)
                .WithRequired(l => l.User)
                .HasForeignKey(l => l.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany<Bid>(u => u.Bids)
                .WithRequired(b => b.User)
                .HasForeignKey(b => b.UserId)
                .WillCascadeOnDelete(false);

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
}
