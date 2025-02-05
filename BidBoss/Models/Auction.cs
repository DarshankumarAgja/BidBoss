using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidBoss.Models
{
    public class Auction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public string Description { get; set; }  // ✅ Add missing property

        [Required]
        public decimal ReservePrice { get; set; }  // ✅ Add missing property

        [Required]
        public decimal MinimumBidIncrement { get; set; }  // ✅ Add missing property

        [Required]
        public DateTime StartDate { get; set; }  // ✅ Add missing property

        [Required]
        public DateTime EndDate { get; set; }  // ✅ Add missing property

        [Required]
        public string SellerId { get; set; }

        [ForeignKey("SellerId")]
        public ApplicationUser Seller { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }
}
