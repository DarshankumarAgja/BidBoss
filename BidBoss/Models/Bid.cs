using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidBoss.Models
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AuctionId { get; set; }

        [ForeignKey("AuctionId")]
        public Auction Auction { get; set; } = default!;

        [Required]
        public string BidderId { get; set; } = default!;  // ✅ This is a foreign key (not an entity)

        [ForeignKey("BidderId")]
        public ApplicationUser Bidder { get; set; } = default!;  // ✅ Navigation property

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime BidTime { get; set; }
    }
}
