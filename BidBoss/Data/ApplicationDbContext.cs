using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BidBoss.Models;

namespace BidBoss.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ✅ Ensure AspNetUsers → Auctions (SellerId) does NOT cascade delete
            builder.Entity<Auction>()
                .HasOne(a => a.Seller)
                .WithMany()
                .HasForeignKey(a => a.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            // ✅ Corrected Foreign Key Mapping for BidderId
            builder.Entity<Bid>()
                .HasOne(b => b.Bidder)  // ✅ Reference the navigation property, not the string field
                .WithMany()
                .HasForeignKey(b => b.BidderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
